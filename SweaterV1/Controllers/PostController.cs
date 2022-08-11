using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Services;

namespace SweaterV1.WebAPI.Controllers;

[ApiController]
public class PostController : ControllerBase
{
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        _postService = postService;
    }

    [Authorize(Roles = "admin")]
    [HttpGet("post/all")]
    public async Task<IEnumerable<PostModel>> GetListOfPostsAsync()
    {
        var post = await _postService.GerListOfEntities();
        return post;
    }

    [HttpGet("user/{userId}/posts")]
    public async Task<IEnumerable<PostModelInformationDto>> GetListOfPostsAsyncByUser(int userId)
    {
        var post = await _postService.GerListOfEntitiesByUser(userId);
        return post;
    }

    [HttpGet("post/{postId}")]
    public async Task<PostModelInformationDto> GetPostAsync(int postId)
    {
        var user = await _postService.GetEntity(postId);
        return user;
    }
    [Authorize]
    [HttpPost("user/post-create")]
    public async Task<PostModelCreationDto> PostPost(PostModelCreationDto post)
    {
        await _postService.CreateEntity(post);
        return post;
    }
    [Authorize]
    [HttpPut("post/{postId}/post-change")]
    public async Task<PostModelChangeDto> PutPost(PostModelChangeDto post, int postId)
    {
        await _postService.UpdateEntity(post, postId);
        return post;
    }
    [Authorize]
    [HttpDelete("post/{postId}/delete")]
    public async Task DeletePost(int postId)
    {
        await _postService.DeleteEntity(postId);
    }
}