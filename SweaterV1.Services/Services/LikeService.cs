using AutoMapper;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;
using SweaterV1.Services.Middlewares;

namespace SweaterV1.Services.Services;

public class LikeService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;
    private readonly IUserProviderMiddleware _userProvider;

    public LikeService(UnitOfWork unitOfWork, IMapper mapper, IUserProviderMiddleware userProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<IEnumerable<LikeModelInformationDto>> GerListOfLikesByPost(int postId)
    {
        var likesToMap = await _unitOfWork.LikeRepository.GetEntityListAsyncByPostId(postId);
        var likes = _mapper.Map<IEnumerable<LikeModelInformationDto>>(likesToMap);
        return likes.ToList();
    }

    public async Task CreateLike(LikeModelCreationDto likeToMap, int postId)
    {
        var like = _mapper.Map<LikeModel>(likeToMap);
        like.UserId = _userProvider.GetUserId();
        like.PostId = postId;
        var likeToValidate =
            await _unitOfWork.LikeRepository.GetEntityByUserIdAndPostIdAsync(like.UserId, like.PostId);

        if (likeToValidate == null)
        {
            var likeMapped = _mapper.Map<LikeModel>(likeToMap);
            likeMapped.UserId = _userProvider.GetUserId();
            likeMapped.PostId = postId;
            likeMapped.CreationDate = DateTime.UtcNow;
            _unitOfWork.LikeRepository.PostEntity(likeMapped);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new ArgumentException("Like already exist");
        }
    }

    public async Task DeleteLike(int likeId)
    {
        var userId = _userProvider.GetUserId();
        var likeToValidate = await _unitOfWork.PostRepository.GetEntityByIdAsync(likeId);

        if (userId == likeToValidate.UserId)
        {
            _unitOfWork.LikeRepository.DeleteEntity(likeId);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new AccessViolationException("Current user doesn't match with like owner");
        }
    }

    public async Task DeleteAllLikes(int postId)
    {
        _unitOfWork.LikeRepository.DeleteAllEntitiesByPostId(postId);
        await _unitOfWork.SaveAsync();
    }
}