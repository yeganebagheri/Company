using DapperASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Contracts
{
    public interface IRabbitMqRepository
    {
         void Producer(Company company);
    }
}
