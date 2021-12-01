using Dapper;
using DapperASPNetCore.Context;
using DapperASPNetCore.Contracts;
using DapperASPNetCore.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperASPNetCore.Repository
{
    public class UserRepository : IUserRepository
    {
        private IConfiguration _config;

       
        private readonly DapperContext _context;

        public UserRepository(DapperContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public Employee Authenticate(Employee login)
        {
            Employee user = null;

            if (login.Email == "yegane@gmail.com" && login.Password == "secret")
            {
                user = new Employee { Name = "yegane bagheri", Email = "yegane@gmail.com" };
            }
            return user;
        }

        public string BuildToken(Employee user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //public Employee ValidateUser(string Email, string Password)
        //{
        //    var query = "SELECT * FROM Employees  Email = @Email and Password = @Password";
        //    using (var connection = _context.CreateConnection())
        //    {

        //        return connection.Query<Employee>(query, new { Email = Email, Password = Password }, commandType: CommandType.Text).FirstOrDefault();
        //    }
        //}

        //public void InsertUser(string Email, string Password)
        //{
        //    var query = "INSERT INTO Employees (Password, Email) VALUES (@Password, @Email)" +
        //        "SELECT CAST(SCOPE_IDENTITY() as int)";
        //    using (var connection = _context.CreateConnection())
        //    {
        //        connection.Execute(query, new { Email = Email, Password = Password }, commandType: CommandType.Text);
        //    }

        //}
    }
}
