using DapperASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Contracts
{
    public interface IUserRepository
    {
        //Employee ValidateUser(string Name, string Password);
        Employee Authenticate(Employee login);
        string BuildToken(Employee user);
        //void InsertUser(string Name, string Password);
    }
}
