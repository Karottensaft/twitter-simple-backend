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
    public async Task<IEnumerable<PostModel>> GetListOfPosts()
    {
        var post = await _postService.GerListOfPosts();
        return post;
    }

    [HttpGet("user/{username}/posts")]
    public async Task<IEnumerable<PostModelInformationDto>> GetListOfPostsByUser(string username)
    {
        var post = await _postService.GerListOfPostsByUsername(username);
        return post;
    }

    [HttpGet("post/{postId}")]
    public async Task<PostModelInformationDto> GetPost(int postId)
    {
        var user = await _postService.GetPost(postId);
        return user;
    }

    [Authorize]
    [HttpPost("user/post-create")]
    public async Task<PostModelCreationDto> PostPost(PostModelCreationDto post)
    {
        await _postService.CreatePost(post);
        return post;
    }

    [Authorize]
    [HttpPut("post/post-change")]
    public async Task<PostModelChangeDto> PutPost(PostModelChangeDto post, int postId)
    {
        await _postService.UpdatePost(post, postId);
        return post;
    }

    [Authorize]
    [HttpDelete("post/delete")]
    public async Task DeletePost(int postId)
    {
        await _postService.DeletePost(postId);
    }
}