using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Revision_API.API.Messaging;
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
        public ActionResult<CreateAccountRequest> CreateUser(CreateAccountRequest request)
        {
            try
            {
                if (!request.Email.Contains('@') || !request.Email.Contains('.'))
                    return BadRequest(new CreateAccountResponse
                    {
                        Error = "Invalid Email",
                        Success = false
                    });

                if (_context.Users.Any(user => user.Email == request.Email))
                    return BadRequest(new CreateAccountResponse
                    {
                        Error = "Email already exists",
                        Success = false
                    });


                User Hallo = new User
                {
                    Email = request.Email,
                    Name = request.Name,
                    Password = request.Password,
                    DeviceToken = request.DeviceToken
                };

                _context.Users.Add(Hallo);
                _context.SaveChanges();

                return Created("User/CreateUser",new CreateAccountResponse
                {
                    Success = true,
                });
            }catch (Exception ex)
            {
                return BadRequest(new CreateAccountResponse { Error= ex.Message, Success = false });
            }
            
        }

        [HttpPost(Name = "ValidateUser")]
        public ActionResult<ValidateAccountResponse> ValidateUser(ValidateAccountRequest request)
        {
            try
            {
                var users = _context.Users.Where(user => user.Email == request.Email);

                if (!users.Any())
                    return BadRequest(new ValidateAccountResponse
                    {
                        Error = "Email doesn't exist",
                        Success = false
                    });

                if (!users.Any(x => x.Password == request.Password))
                    return BadRequest(new ValidateAccountResponse
                    {
                        Error = "Invalid password",
                        Success = false
                    });

                var user = users.FirstOrDefault();

                return Ok(new ValidateAccountResponse
                {
                    Success = true,
                    UserId = user.Id.ToString(),
                    Name = user.Name
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ValidateAccountResponse { Success = false, Error = ex.Message});
            }
        }

        [HttpDelete(Name = "ResetDatabase")]
        public void ResetDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
