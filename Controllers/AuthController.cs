using AutoMapper;
using DapperASPNetCore.Contracts;
using DapperASPNetCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Controllers
{

    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

       
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody] LoginViewModel login)
        {
            IActionResult response = Unauthorized();
            var Data = _mapper.Map<Employee>(login);
            var user = _userRepository.Authenticate(Data);

            if (user != null)
            {
                var tokenString = _userRepository.BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

      

       
    }
}
