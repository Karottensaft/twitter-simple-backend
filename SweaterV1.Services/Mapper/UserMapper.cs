using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SweaterV1.Domain.Models;
using SweaterV1.Infrastructure.Repositories;
using SweaterV1.Infrastructure.Data;
using AutoMapper;


namespace SweaterV1.Services.Mapper
{

    public class UserMapper
    {
        public static UserModelInformationDto ToUserModelInformationDtoMap(UserModel user)
        {
            return new UserModelInformationDto()
            {
                Login = user.Login,
                Email = user.Mail,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        //public static AddressDTO ToAddressDTOMap(Address address)
        //{
        //    return new AddressDTO()
        //    {
        //        ID = address.ID,
        //        City = address.City
        //    };
        //}
    }
}
