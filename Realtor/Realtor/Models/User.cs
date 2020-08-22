﻿using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Realtor.Models
{
    public class User
    {
        public string CompanyName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
