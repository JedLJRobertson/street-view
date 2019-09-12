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
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            StringValues token = "";

            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (token.Count() != 1)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var tokenStr = token.Single();

            var authToken = _context.Tokens.Find(tokenStr);

            if (authToken == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (authToken.Expires < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            {
                context.Result = new UnauthorizedResult();

                _context.Tokens.Remove(authToken);
                return;
            }
        }
    }
}
