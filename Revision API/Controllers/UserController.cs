using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Revision_API.Data;
using Revision_API.Models;


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

        [HttpPost(Name = "PostUser")]
        public void CreateUser(string email, string password)
        {
            if (_context.User.Any(user => user.Email == email))
            {
                return;
            }

            User Hallo = new User();

            Hallo.Email = email;
            Hallo.Password = password;

            _context.User.Add(Hallo);
            _context.SaveChanges();
        }

        [HttpGet(Name = "Validate")]
        public bool ValidateCredentials(string email, string password)
        {
            return _context.User.Any(user => user.Email == email 
                                             && user.Password == password);
        }
    }
}
