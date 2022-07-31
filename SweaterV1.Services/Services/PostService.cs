using AutoMapper;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;


namespace SweaterV1.Services.Services
{
    public class PostService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostModel>> GerListOfEntities()
        {
            return await _unitOfWork.PostRepository.GetEntityListAsync();
        }

        public async Task<PostModelInformationDto> GetEntity(int id)
        {
            var user = await _unitOfWork.PostRepository.GetEntityByIdAsync(id);
            return _mapper.Map<PostModelInformationDto>(user);
        }

        public async Task CreateEntity(PostModelCreationDto postMapped)
        {
            var post = _mapper.Map<PostModel>(postMapped);
            _unitOfWork.PostRepository.PostEntity(post);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateEntity(PostModelChangeDto postDto, int id)
        {
            var post = await _unitOfWork.PostRepository.GetEntityByIdAsync(id);
            _mapper.Map<PostModelChangeDto, PostModel>(postDto, post);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteEntity(int id)
        {
            _unitOfWork.PostRepository.DeleteEntity(id);
            await _unitOfWork.SaveAsync();
        }
    }
}