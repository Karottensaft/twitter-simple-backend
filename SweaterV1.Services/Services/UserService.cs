using AutoMapper;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;
using SweaterV1.Services.Middlewares;

namespace SweaterV1.Services.Services;

public class UserService
{
    private readonly IMapper _mapper;
    private readonly TokenMiddleware _tokenMiddleware;
    private readonly UnitOfWork _unitOfWork;
    private readonly IUserProviderMiddleware _userProvider;

    public UserService(UnitOfWork unitOfWork, IMapper mapper, IUserProviderMiddleware userProvider,
        TokenMiddleware tokenMiddleware)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userProvider = userProvider;
        _tokenMiddleware = tokenMiddleware;
    }

    public async Task<TokenModel> GetToken(UserModelAuthDto userToValidate)
    {
        UserModel userToMap = await _unitOfWork.UserRepository.GetEntityByNameAsync(userToValidate.Username);
        if (userToMap == null) throw new InvalidDataException("Wrong username or password");
        if (HashPasswordMiddleware.VerifyPassword(userToValidate.Password, userToMap.Password))
        {
            var user = _mapper.Map<UserModelLoginDto>(userToMap);
            user.UserId = userToMap.UserId;
            return _tokenMiddleware.GetToken(user);
            
        }
            
        throw new InvalidDataException("Wrong username or password");
    }

    public async Task<IEnumerable<UserModel>> GerListOfUsers()
    {
        return await _unitOfWork.UserRepository.GetEntityListAsync();
    }

    public async Task<UserModelInformationDto> GetCurrentUser()
    {
        var userId = _userProvider.GetUserId();
        var usersToMap = await _unitOfWork.UserRepository.GetEntityByIdAsync(userId);
        return _mapper.Map<UserModelInformationDto>(usersToMap);
    }

    public async Task<UserModelInformationDto> GetUserByUsername(string username)
    {
        var userToMap = await _unitOfWork.UserRepository.GetEntityByNameAsync(username);
        return _mapper.Map<UserModelInformationDto>(userToMap);
    }


    public async Task CreateUser(UserModelRegistrationDto userToMap)
    {
        var userToValidate = await _unitOfWork.UserRepository.GetEntityByNameAsync(userToMap.Username);

        if (userToValidate == null)
        {
            userToMap.Password = HashPasswordMiddleware.CreatePasswordHash(userToMap.Password);
            var user = _mapper.Map<UserModel>(userToMap);
            _unitOfWork.UserRepository.PostEntity(user);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new ArgumentException("User already exist.");
        }
    }

    public async Task UpdateUser(UserModelChangeDto userChanged)
    {
        var userId = _userProvider.GetUserId();
        var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(userId);
        userChanged.Password = HashPasswordMiddleware.CreatePasswordHash(userChanged.Password);
        _mapper.Map(userChanged, user);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteUser(int userId)
    {
        _unitOfWork.UserRepository.DeleteEntity(userId);
        await _unitOfWork.SaveAsync();
    }
}