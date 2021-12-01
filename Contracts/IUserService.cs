using DapperASPNetCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DapperASPNetCore.Contracts
{
   // public interface IUserService
    //{
    //    List<Employee> GetUserList();

    //    Employee GetToken(string username, string password);

    //    void InsertUser(string username, string password);

    //}

    //public class UserService : IUserService
    //{
    //    private readonly IUserRepository _userRepository;

    //    public UserService(IUserRepository userRepository)
    //    {
    //        _userRepository = userRepository;
    //    }

        //public List<Employee> GetUserList()
        //{
        //    var obj = new List<Employee>();
        //    return obj;
        //}

        //public Employee GetToken(string Name, string password)
        //{
        //    var passwordHash = Utils.HashUtil.GetSha256FromString(password);

        //    var ret = _userRepository.ValidateUser(Name, passwordHash);

        //    if (ret != null)
        //    {
        //        //ret.Token = Utils.JwtManager.GenerateToken(username).Value;
        //    }
        //    return ret;
        //}

        //public void InsertUser(string username, string password)
        //{
        //    var passwordHash = Utils.HashUtil.GetSha256FromString(password);

        //    _userRepository.InsertUser(username, passwordHash);
        //}




    }
