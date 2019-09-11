using Microsoft.AspNetCore.Mvc;
using street.Dto;
using street.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace street.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly MyContext _context;

        public UserController(MyContext context)
        {
            _context = context;

            if (_context.Reports.Count() == 0)
            {
                InitExampleDb();
            }
        }

        private void InitExampleDb()
        {
            _context.Users.Add(new User { Username = "jed", Password = "test" });
            _context.SaveChanges();
        }

        [HttpPost]
        public ActionResult<AuthTokenDto> Login(LoginDto login)
        {
            var user = _context.Users.First(u => u.Username.ToLower() == login.Username.ToLower());

            if (user == null)
            {
                return new NotFoundResult();
            }

            if (user.Password != login.Password)
            {
                return new NotFoundResult();
            }

            var token = new AuthToken
            {
                Id = Guid.NewGuid().ToString(),
                User = user
            };

            _context.Tokens.Add(token);
            _context.SaveChanges();

            return new AuthTokenDto(token);
        }

    }
}
