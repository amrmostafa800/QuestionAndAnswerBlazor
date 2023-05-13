using QuestionAndAnswerBlazor.Helpers;
using QuestionAndAnswerBlazor.Models;
using System.Text.RegularExpressions;

namespace QuestionAndAnswerBlazor.Services
{
    public class AccountsService
    {
        private readonly Models.AppContext _context;

        public AccountsService(Models.AppContext context)
        {
            _context = context;
        }

        private bool _isValidUserName(ref string UserName)
        {
            return Regex.IsMatch(UserName, "^[a-zA-Z0-9_]{4,16}$");
        }

        private bool _isValidEmail(ref string Email)
        {
            return Regex.IsMatch(Email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
        }

        public bool AddNew(string User, string Password, string Email)
        {
            string PasswordHash = PasswordHelper.HashPassword(Password);

            if (_isValidUserName(ref User) && _isValidEmail(ref Email))
            {
                User user = new()
                {
                    Username = User,
                    Password = PasswordHash,
                    Email = Email
                };

                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public int LoginIn(string Username, string Password)
        {
            var result = (from U in _context.Users
                          where U.Username == Username
                          select new User
                          {
                              Username = U.Username,
                              Password = U.Password,
                              Id = U.Id
                          })
             .SingleOrDefault();

            //var Account = _context.Users.SingleOrDefault(U => U.Username == Username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                var isValidPassword = PasswordHelper.VerifyPassword(Password, result.Password);
                if (isValidPassword)
                {
                    return result.Id;
                }
                return 0; //Invlid Password
            }
        }
    }
}
