﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace street.Models
{
    public class AuthToken
    {
        public string Id { get; set; }
        public long Expires { get; set; }
        public User User { get; set; }
    }
}
