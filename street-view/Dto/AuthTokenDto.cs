using street.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace street.Dto
{
    public class AuthTokenDto
    {
        public string Id { get; set; }

        public AuthTokenDto()
        {

        }

        public AuthTokenDto(AuthToken token)
        {
            Id = token.Id;
        }
    }
}
