using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;
using AutoMapper;
using SweaterV1.Services.Extensions;

namespace SweaterV1.Services.Services;
public class UserService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserModelLoginDto> ValidateLogIn(UserModelAutentificationDto data)
    {
        var user = await _unitOfWork.UserRepository.GetEntityByUsernameAsync(data.Username);
        
        if (PBKDF2HashMiddleware.VerifyPassword(data.Password, user.Password))
        {
            return _mapper.Map<UserModelLoginDto>(user);
        }
        else
        {
            throw new Exception("Wrong username or password");
        }
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

        if (userToValidate != null)
        {
            throw new Exception("User already exist");
        }

        userMapped.Password = PBKDF2HashMiddleware.CreatePasswordHash(userMapped.Password);
        var user = _mapper.Map<UserModel>(userMapped);
        _unitOfWork.UserRepository.PostEntity(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateEntity(UserModelChangeDto userDto, int id)
    {
        var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id);
        userDto.Password = PBKDF2HashMiddleware.CreatePasswordHash(userDto.Password);
        _mapper.Map(userDto, user);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteEntity(int id)
    {
        _unitOfWork.UserRepository.DeleteEntity(id);
        await _unitOfWork.SaveAsync();
    }
}