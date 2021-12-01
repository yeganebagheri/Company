using Dapper;
using DapperASPNetCore.Context;
using DapperASPNetCore.Contracts;
using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Repository
{
    public class CompanyTypeRepository : ICompanyTypeRepository
    {
        private readonly DapperContext _context;

        public CompanyTypeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyType>> GetCompanyTypes()
        {
            var query = "SELECT * FROM CompanyTypes";
           
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<CompanyType>(query);
                return companies.ToList();
            }
        }

        public async Task<CompanyType> GetCompanyType(int id)
        {
            var query = "SELECT * FROM CompanyTypes WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<CompanyType>(query, new { id });

                return company;
            }
        }

        public async Task<CompanyType> CreateCompanyType(CompanyType companyType)
        {
            var query = "INSERT INTO CompanyTypes (Title) VALUES (@Title)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Title", companyType.Title, DbType.String);
       
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdCompanyType = new CompanyType
                {
                    Id = id,
                    Title = companyType.Title,
                   
                };

                return createdCompanyType;
            }
        }

        public async Task UpdateCompanyType(int id, CompanyTypeForCreationDto companyType)
        {
            var query = "UPDATE Companies SET Title = @Title WHERE Id = @Id";

            var parameters = new DynamicParameters();
           
            parameters.Add("Title", companyType.Title, DbType.String);
            
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCompanyType(int id)
        {
            var query = "DELETE FROM CompanyTypes WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
        
    }
}
