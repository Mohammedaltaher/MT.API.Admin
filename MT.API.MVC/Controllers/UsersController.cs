using AutoMapper;
using MT.API.MVC.App_Start;
using MT.API.MVC.Models;
using MT.API.MVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.UI;

namespace MT.API.MVC.Controllers
{
    public class UsersController : ApiController
    {
        Context context = new Context();
        private readonly IMapper mapper;
        public UsersController()
        {
            var config = MappingProfile.config();
             mapper = config.CreateMapper();
        }

        public IHttpActionResult Get()
        {
            var user = context.User.FirstOrDefault();
            var data = mapper.Map<User ,UserDto>(user);
             return Ok(data);
        }
        // POST api/values
        public IHttpActionResult Post([FromBody] UserRequestDto req)
        {
            try
            {
                if (context.User.Where(x => x.Username == req.Username  || x.Email == req.Email).Count() > 0)
                {
                    return BadRequest("username is already exits");
                }
                // var data =  Mapper.Map<CameraRequestDto, Camera>(req);
                User user = new User()
                {
                    CreateDate = DateTime.Now,
                    Email = req.Email,
                    FullName = req.FullName,
                    Password = req.Password,
                    IsDeleted = "N",
                    Username = req.Username,
                    PhoneNumber = req.PhoneNumber,
                    UserTypeId = 2 // citizen
                };
                
                context.User.Add(user);
                context.SaveChanges();
                return Ok("Done");
            }
            catch (Exception ex)
            {
                throw ex;
            }
      
        }
        [Route("api/Login")]
        [HttpPost]
        public IHttpActionResult Login(UserLoginRequestDto req)
        {
            UserLoginResponseDto response;
            var user = context.User.Where(x => (x.Email == req.Email || x.Username == req.Email) && x.Password == req.Password).FirstOrDefault();
            if(user != null)
            {
                response = new UserLoginResponseDto()
                {
                    Data = mapper.Map<User, UserLoginDto>(user) ,
                    ErrorMessage = null 
                };
            }
            else
            {
                response = new UserLoginResponseDto()
                {
                    Data = null ,
                    ErrorMessage = "incorrect password or email "
                };
            }
            return Json(response);
        }

    }
}
