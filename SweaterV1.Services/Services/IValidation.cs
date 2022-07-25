using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweaterV1.Services.Services
{
    public interface IValidation
    {
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
    }
}
//protected bool ValidateUser(UserModel user)
//{
//    if (user.Login.Trim().Length == 0)
//        _validaton.AddError("Login", "Login is required.");
//    if (user.Password.Trim().Length <= 7)
//        _validaton.AddError("Password", "Symbols in the password cannot be less than seven.");
//    if (user.Mail.Trim().Length == 0)
//        _validaton.AddError("Mail", "Mail is required.");
//    if (user.FirstName.Trim().Length <= 7)
//        _validaton.AddError("FirstName", "FirstName is required.");
//    if (user.LastName.Trim().Length == 0)
//        _validaton.AddError("LastName", "LastName is required.");
//        return _validaton.IsValid;
//}
//if (!ValidateUser(user))
//    return;

// Database logic