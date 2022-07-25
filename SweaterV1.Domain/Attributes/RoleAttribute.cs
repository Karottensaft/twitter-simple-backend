//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SweaterV1.Domain.Attributes
//{
//    internal class RoleAttribute : ValidationAttribute
//    {
//        public override bool IsValid(object? value)
//        {
//            if (value is string role)
//            {
//                if (role != "admin")
//                    return true;
//                else
//                    ErrorMessage = "Некорректное имя: admin";
//            }
//            return false;
//        }
//    }
//}
