using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Entities
{
    public class LoginViewModel
    {
        
        public string Email { get; set; }

       
        public string Password { get; set; }
    }

    public class LoginValidator : AbstractValidator<Employee>
    {
        public LoginValidator()
        {
           
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
