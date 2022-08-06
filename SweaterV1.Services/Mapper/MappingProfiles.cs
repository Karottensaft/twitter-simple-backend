using AutoMapper;
using SweaterV1.Domain.Models;

namespace SweaterV1.Services.Mapper
{
    public class UserLoginProfile : Profile
    {
        public UserLoginProfile()
        {
            CreateMap<UserModel, UserModelLoginDto>();
        }
    }

    public class UserRegistrationProfile : Profile
    {
        public UserRegistrationProfile()
        {
            CreateMap< UserModelRegistrationDto, UserModel>();
        }
    }

    public class UserInformationProfile : Profile
    {
        public UserInformationProfile()
        {
            CreateMap<UserModel, UserModelInformationDto>();
        }
    }

    public class UserChangeProfile : Profile
    {
        public UserChangeProfile()
        {
            CreateMap<UserModelChangeDto, UserModel>();
        }
    }

    public class PostCreationProfile : Profile
    {
        public PostCreationProfile()
        {
            CreateMap<PostModelCreationDto, PostModel>();
        }
    }

    public class PostInformationProfile : Profile
    {
        public PostInformationProfile()
        {

            CreateMap<PostModel, PostModelInformationDto>();
        }
    }

    public class PostChangeProfile : Profile
    {
        public PostChangeProfile()
        {
            CreateMap<PostModelChangeDto, PostModel>();
        }
    }

    public class CommentInformationProfile : Profile
    {
        public CommentInformationProfile()
        {
            CreateMap<CommentModel, CommentModelInformationDto>();
        }
    }
    public class CommentCreateProfile : Profile
    {
        public CommentCreateProfile()
        {
            CreateMap<CommentModelCreationDto, CommentModel>();
        }
    }
    public class CommentChangeProfile : Profile
    {
        public CommentChangeProfile()
        {
            CreateMap<CommentModelChangeDto, CommentModel>();
        }
    }

    public class LikeInformationProfile : Profile
    {
        public LikeInformationProfile()
        {
            CreateMap<LikeModel, LikeModelInformationDto>();
        }
    }

    public class LikeCreateProfile : Profile
    {
        public LikeCreateProfile()
        {
            CreateMap<LikeModelCreationDto, LikeModel>();
        }
    }
}
