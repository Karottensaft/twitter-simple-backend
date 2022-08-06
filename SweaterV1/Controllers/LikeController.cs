using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Services;

namespace SweaterV1.Controllers;

[ApiController]
[Route("[controller]")]
public class LikeController : ControllerBase
{
    private readonly LikeService _likeService;

    public LikeController(LikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpGet("post/{postId}/likes")]
    public async Task<IEnumerable<LikeModelInformationDto>> GetListAsyncByPost(int postId)
    {
        var like = await _likeService.GerListOfEntitiesByPost(postId);
        return like;
    }


    [HttpPost("post/{id}/like-creator")]
    public async Task<LikeModelCreationDto> Post(LikeModelCreationDto like)
    {
        await _likeService.CreateEntity(like!);
        return like!;
    }

    [HttpDelete("like/{id}/deleter")]
    public async Task Delete(int id)
    {
        await _likeService.DeleteEntity(id);
    }
}