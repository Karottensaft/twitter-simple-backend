using AutoMapper;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;

namespace SweaterV1.Services.Services;

public class CommentService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public CommentService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CommentModel>> GerListOfEntities()
    {
        return await _unitOfWork.CommentRepository.GetEntityListAsync();
    }

    public async Task<CommentModelInformationDto> GetEntity(int id)
    {
        var comment = await _unitOfWork.CommentRepository.GetEntityByIdAsync(id);
        return _mapper.Map<CommentModelInformationDto>(comment);
    }

    public async Task CreateEntity(CommentModelCreationDto commentMapped)
    {
        var comment = _mapper.Map<CommentModel>(commentMapped);
        _unitOfWork.CommentRepository.PostEntity(comment);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateEntity(CommentModelChangeDto commentDto, int id)
    {
        var comment = await _unitOfWork.CommentRepository.GetEntityByIdAsync(id);
        _mapper.Map(commentDto, comment);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteEntity(int id)
    {
        _unitOfWork.CommentRepository.DeleteEntity(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAllEntities(int postId)
    {
        _unitOfWork.CommentRepository.DeleteAllEntitiesByPostId(postId);
        await _unitOfWork.SaveAsync();
    }
}