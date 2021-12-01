using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Contracts
{
    public interface ICompanyTypeRepository
    {
        public Task<IEnumerable<CompanyType>> GetCompanyTypes();
        public Task<CompanyType> GetCompanyType(int id);
        public Task<CompanyType> CreateCompanyType(CompanyType company);
        public Task UpdateCompanyType(int id, CompanyTypeForCreationDto company);
        public Task DeleteCompanyType(int id);
    }
}
