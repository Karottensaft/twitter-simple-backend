﻿using System.Diagnostics.Eventing.Reader;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;
using AutoMapper;
using SweaterV1.Services.Services;
using SweaterV1.Services.HelpingServices;

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

    public async Task<UserModelLoginDto> LoginAsync(string login, string password)
    {
        //password = PBKDF2HashHelper.CreatePasswordHash(password);
        var user = await _unitOfWork.UserRepository.LoginAsync(login);
        
        if (PBKDF2HashHelper.VerifyPassword(password, user.Password))
        {
            return _mapper.Map<UserModelLoginDto>(user);
        }
        else
        {
            return null;
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
        userMapped.Password =  PBKDF2HashHelper.CreatePasswordHash(userMapped.Password);
        var user = _mapper.Map<UserModel>(userMapped);
        
        _unitOfWork.UserRepository.PostEntity(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateEntity(UserModelChangeDto userDto, int id)
    {
        var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id);
        _mapper.Map<UserModelChangeDto, UserModel>(userDto, user);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteEntity(int id)
    {
        _unitOfWork.UserRepository.DeleteEntity(id);
        await _unitOfWork.SaveAsync();
    }
}