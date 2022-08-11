using AutoMapper;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;
using SweaterV1.Services.Extensions;

namespace SweaterV1.Services.Services;

public class UserService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public UserService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserModelLoginDto> ValidateLogIn(UserModelAuthDto data)
    {
        var user = await _unitOfWork.UserRepository.GetEntityByUsernameAsync(data.Username);
        if (user == null) throw new InvalidDataException("Wrong username or password");
        if (Pbkdf2HashMiddleware.VerifyPassword(data.Password, user.Password))
            return _mapper.Map<UserModelLoginDto>(user);
        throw new InvalidDataException("Wrong username or password");
    }

    public async Task<IEnumerable<UserModel>> GerListOfEntities()
    {
        return await _unitOfWork.UserRepository.GetEntityListAsync();
    }

    public async Task<UserModelInformationDto> GetEntity(int id)
    {
        var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id);
        return _mapper.Map<UserModelInformationDto>(user);
    }

    public async Task CreateEntity(UserModelRegistrationDto userMapped)
    {
        var userToValidate = await _unitOfWork.UserRepository.GetEntityByUsernameAsync(userMapped.Username);

        if (userToValidate == null)
        {
            userMapped.Password = Pbkdf2HashMiddleware.CreatePasswordHash(userMapped.Password);
            var user = _mapper.Map<UserModel>(userMapped);
            _unitOfWork.UserRepository.PostEntity(user);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new ArgumentException("User already exist.");
        }
    }

    public async Task UpdateEntity(UserModelChangeDto userDto, int id)
    {
        var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id);
        userDto.Password = Pbkdf2HashMiddleware.CreatePasswordHash(userDto.Password);
        _mapper.Map(userDto, user);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteEntity(int id)
    {
        _unitOfWork.UserRepository.DeleteEntity(id);
        await _unitOfWork.SaveAsync();
    }
}