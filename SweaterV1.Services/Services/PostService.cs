using AutoMapper;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;
using SweaterV1.Services.Middlewares;

namespace SweaterV1.Services.Services;

public class PostService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;
    private readonly IUserProviderMiddleware _userProvider;

    public PostService(UnitOfWork unitOfWork, IMapper mapper, IUserProviderMiddleware userProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<IEnumerable<PostModel>> GerListOfPosts()
    {
        return await _unitOfWork.PostRepository.GetEntityListAsync();
    }

    public async Task<IEnumerable<PostModelInformationDto>> GerListOfPostsByUsername(string username)
    {
        var postToMap = await _unitOfWork.PostRepository.GetEntityListAsyncByUserId(username);
        return _mapper.Map<IEnumerable<PostModelInformationDto>>(postToMap);
    }

    public async Task<PostModelInformationDto> GetPost(int postId)
    {
        var postToMap = await _unitOfWork.PostRepository.GetEntityByIdAsync(postId);
        return _mapper.Map<PostModelInformationDto>(postToMap);
    }

    public async Task CreatePost(PostModelCreationDto postToMap)
    {
        var postMapped = _mapper.Map<PostModel>(postToMap);
        postMapped.CreationDate = DateTime.UtcNow;
        postMapped.UserId = _userProvider.GetUserId();
        postMapped.Username =  _userProvider.GetUsername();
        _unitOfWork.PostRepository.PostEntity(postMapped);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdatePost(PostModelChangeDto postChanged, int postId)
    {
        var userId = _userProvider.GetUserId();
        var post = await _unitOfWork.PostRepository.GetEntityByIdAsync(postId);
        if (userId == post.UserId)
        {
            _mapper.Map(postChanged, post);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new AccessViolationException("Current user doesn't match with post owner");
        }
    }

    public async Task DeletePost(int postId)
    {
        var userId = _userProvider.GetUserId();
        var postToValidate = await _unitOfWork.PostRepository.GetEntityByIdAsync(postId);

        if (userId == postToValidate.UserId)
        {
            _unitOfWork.CommentRepository.DeleteAllEntitiesByPostId(postId);
            _unitOfWork.LikeRepository.DeleteAllEntitiesByPostId(postId);
            _unitOfWork.PostRepository.DeleteEntity(postId);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new AccessViolationException("Current user doesn't match with post owner");
        }
    }
}