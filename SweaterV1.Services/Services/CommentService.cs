using AutoMapper;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;
using SweaterV1.Services.Middlewares;

namespace SweaterV1.Services.Services;

public class CommentService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;
    private readonly IUserProviderMiddleware _userProvider;

    public CommentService(UnitOfWork unitOfWork, IMapper mapper, IUserProviderMiddleware userProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<IEnumerable<CommentModelInformationDto>> GerListOfCommentsByPostId(int postId)
    {
        var commentsToMap = await _unitOfWork.CommentRepository.GetEntityListByPostIdAsync(postId);
        var commentsMapped = _mapper.Map<IEnumerable<CommentModelInformationDto>>(commentsToMap);
        return commentsMapped.ToList();
    }

    public async Task CreateComment(CommentModelCreationDto commentToMap, int postId)
    {
        var commentMapped = _mapper.Map<CommentModel>(commentToMap);
        commentMapped.UserId = _userProvider.GetUserId();
        commentMapped.PostId = postId;
        _unitOfWork.CommentRepository.PostEntity(commentMapped);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateComment(CommentModelChangeDto commentToMap, int commentId)
    {
        var userId = _userProvider.GetUserId();
        var comment = await _unitOfWork.CommentRepository.GetEntityByIdAsync(commentId);
        if (userId == comment.UserId)
        {
            _mapper.Map(commentToMap, comment);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new AccessViolationException("Current user doesn't match with comment owner");
        }
    }

    public async Task DeleteComment(int commentId)
    {
        var userId = _userProvider.GetUserId();
        var commentToValidate = await _unitOfWork.CommentRepository.GetEntityByIdAsync(commentId);
        if (userId == commentToValidate.UserId)
        {
            _unitOfWork.CommentRepository.DeleteEntity(commentId);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new AccessViolationException("Current user doesn't match with comment owner");
        }
    }

    public async Task DeleteAllComments(int postId)
    {
        _unitOfWork.CommentRepository.DeleteAllEntitiesByPostId(postId);
        await _unitOfWork.SaveAsync();
    }
}