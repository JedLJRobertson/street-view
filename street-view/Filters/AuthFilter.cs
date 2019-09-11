using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using street.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace street.Filters
{
    public class AuthFilter : IAuthorizationFilter
    {
        private readonly MyContext _context;

        public AuthFilter(MyContext context)
        {
            _context = context;

            if (_context.Tokens.Count() == 0)
            {
                _context.Add(new AuthToken { Id = "test" });
            }
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            StringValues token = "";

            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out token))
            {
                context.Result = new UnauthorizedResult();
            }

            if (token.Count() != 1)
            {
                context.Result = new UnauthorizedResult();
            }

            var tokenStr = token.Single();

            var authToken = _context.Tokens.Find(tokenStr);

            if (authToken == null)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
