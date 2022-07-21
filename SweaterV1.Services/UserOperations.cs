//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SweaterV1.Domain.Models;
//using SweaterV1.Infrastructure.Repositories;
//using SweaterV1.Infrastructure.Data;

//namespace SweaterV1.Services
//{

    
//    public class UserOperations : IUserOperations
//    {
//        private SweaterDBContext _db;
//        private UserRepository _userRepository;
//        public UserOperations(SweaterDBContext context, UserRepository userRepository)
//        {
//            _db = context;
//            _userRepository = userRepository;
//        }
//        public async Task<IEnumerable<UserModel>> GetAllEntities()
//        {
//            return await _userRepository.GetEntityListAsync();
//        }

//    }
//}
