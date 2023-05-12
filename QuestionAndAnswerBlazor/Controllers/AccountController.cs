using Microsoft.AspNetCore.Mvc;
using QuestionAndAnswerBlazor.DTOs;
using QuestionAndAnswerBlazor.Helpers;
using QuestionAndAnswerBlazor.Services;

namespace QuestionAndAnswerBlazor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        IAccountsService _Account;
        private IConfiguration _configuration;
        JwtHelper _JWT;

        public AccountController(IAccountsService account, IConfiguration configuration)
        {
            _Account = account;
            _configuration = configuration;
            _JWT = new JwtHelper(_configuration.GetSection("JWT:Key").Value ?? throw new InvalidOperationException("JWT 'Key' not found.")); ;
        }

        [HttpPost("Register")]
        public IActionResult Register(NewAccountDTO newAccount)
        {
            bool Result = _Account.AddNew(newAccount.Username, newAccount.Password, newAccount.Email);
            return Ok(Result);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginAccountDTO loginAccount)
        {
            var LoginResult = _Account.LoginIn(loginAccount.Username, loginAccount.Password); // 0 = Invalid Other = Account ID
            if (LoginResult == 0)
            {
                return Ok("Invalid Username Or Password");
            }

            string JwtKey = _JWT.GenerateToken(LoginResult.ToString());

            return Ok(JwtKey);
        }
    }
}
