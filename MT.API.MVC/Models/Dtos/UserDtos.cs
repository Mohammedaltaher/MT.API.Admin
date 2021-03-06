﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MT.API.MVC.Models.Dtos
{
    public class UserRequestDto
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class UserDto
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class UserLoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class UserLoginDto
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }

    }

    public class UserLoginResponseDto
    {
        public UserLoginDto Data { get; set; }
        public string ErrorMessage {get;set;}
    }

}