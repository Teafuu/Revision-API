using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Revision_API.Data;
using Revision_API.Models;
using Revision_API.Models.Dto.Request;
using Revision_API.Models.Dto.Response;


namespace Revision_API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly Revision_APIContext _context;

        public UserController(Revision_APIContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "CreateUser")]
        public CreateAccountResponse CreateUser(CreateAccountRequest request)
        {
            if (!request.Email.Contains("@"))
                return new CreateAccountResponse
                {
                    Error = "Invalid Email",
                    Success = false
                };

            if (_context.Users.Any(user => user.Email == request.Email))
                return new CreateAccountResponse
                {
                    Success = false,
                    Error = "Email already exists"
                };
            

            User Hallo = new User
            {
                Email = request.Email,
                Password = request.Password
            };

            _context.Users.Add(Hallo);
            _context.SaveChanges();

            return new CreateAccountResponse
            {
                Success = true
            };
        }

        [HttpPost(Name = "ValidateUser")]
        public ValidateAccountResponse ValidateUser(ValidateAccountRequest request)
        {
            var users = _context.Users.Where(user => user.Email == request.Email);

            if (!users.Any())
                return new ValidateAccountResponse
                {
                    Error = "Email doesn't exist",
                    Success = false
                };

            if (!users.Any(x => x.Password == request.Password))
                return new ValidateAccountResponse
                {
                    Error = "Invalid password",
                    Success = false
                };

            return new ValidateAccountResponse
            {
                Success = true
            };
        }
    }
}
