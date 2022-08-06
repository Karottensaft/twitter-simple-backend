using System.Collections;
using AutoMapper;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;


namespace SweaterV1.Services.Services
{
    public class LikeService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LikeService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LikeModelInformationDto>> GerListOfEntitiesByPost(int postId)
        {
            IEnumerable<LikeModel> likesToMap =  await _unitOfWork.LikeRepository.GetEntityListAsyncByPostId(postId);
            var likes = _mapper.Map<IEnumerable<LikeModelInformationDto>>(likesToMap);
            return likes.ToList();

        }

        public async Task CreateEntity(LikeModelCreationDto likeMapped)
        {
            var like = _mapper.Map<LikeModel>(likeMapped);
            _unitOfWork.LikeRepository.PostEntity(like);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteEntity(int id)
        {
            _unitOfWork.LikeRepository.DeleteEntity(id);
            await _unitOfWork.SaveAsync();
        }
    }
}