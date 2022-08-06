using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Services;

namespace SweaterV1.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly CommentService _commentService;

    public CommentController(CommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<IEnumerable<CommentModel>> GetListAsync()
    {
        var comment = await _commentService.GerListOfEntities();
        return comment;
    }

    [HttpGet("{id}")]
    public async Task<CommentModelInformationDto> GetAsync(int id)
    {
        var comment = await _commentService.GetEntity(id);
        return comment!;
    }

    [HttpPost]
    public async Task<CommentModelCreationDto> Post(CommentModelCreationDto comment)
    {
        await _commentService.CreateEntity(comment!);
        return comment!;
    }

    [HttpPut("{id}")]
    public async Task<CommentModelChangeDto> Put(CommentModelChangeDto comment, int id)
    {
        await _commentService.UpdateEntity(comment, id);
        return comment;
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _commentService.DeleteEntity(id);
    }
}