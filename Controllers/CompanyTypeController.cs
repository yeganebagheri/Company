using AutoMapper;
using Dapper;
using DapperASPNetCore.Context;
using DapperASPNetCore.Contracts;
using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperASPNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyTypeController : ControllerBase
    {
        //private readonly ICacheService _cacheService;
        private readonly ICompanyTypeRepository _companyTypeRepo;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        private readonly DapperContext _context;

       
        public CompanyTypeController(DapperContext context, IMapper mapper,ICompanyTypeRepository companyTypeRepo , IDistributedCache distributedCache)
        {
            
            _companyTypeRepo = companyTypeRepo;
            _distributedCache = distributedCache;
            _context = context;
            _mapper = mapper;
        }
        //[HttpGet]
        //public async Task<IActionResult> GetCompanyTypes()
        //{
        //    try
        //    {
        //        var companyTypes = await _companyTypeRepo.GetCompanyTypes();
        //        return Ok(companyTypes);
        //    }
        //    catch (Exception ex)
        //    {
        //        //log error
        //        return StatusCode(500, ex.Message);
        //    }
        //}


        //[HttpGet("{id}", Name = "CompanyTypeById")]
        //public async Task<IActionResult> GetCompanyType(int id)
        //{
        //    try
        //    {
        //        var company = await _companyTypeRepo.GetCompanyType(id);
        //        if (company == null)
        //            return NotFound();

        //        return Ok(company);
        //    }
        //    catch (Exception ex)
        //    {
        //        //log error
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpGet("{key}") ]
        public async Task<IActionResult> GetCompanyType(string Key)
        {
            //var cacheKey = "CompanyType";
            CompanyType companyType = new CompanyType();
            var cachedCompanyType = await _distributedCache.GetAsync(Key);
            if (cachedCompanyType != null)
            {
                var serializedCompanyType = Encoding.UTF8.GetString(cachedCompanyType);
                 companyType = JsonConvert.DeserializeObject<CompanyType>(serializedCompanyType);
            }
            else
            {
               
                var query = "SELECT * FROM CompanyTypes WHERE Id = @Key";

                using (var connection = _context.CreateConnection())
                {
                     companyType = await connection.QuerySingleOrDefaultAsync<CompanyType>(query, new { Key });
                    
                    var serializedCompanyType = JsonConvert.SerializeObject(companyType);
                    cachedCompanyType = Encoding.UTF8.GetBytes(serializedCompanyType);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await _distributedCache.SetAsync(Key, cachedCompanyType, options);

                }

            }
            return Ok(companyType);
        }

        [HttpGet("allCached")]
        public async Task<IActionResult> GetAllCompanyTypeCache()
        {
            var cacheKey = "CompanyTypeList";
            List<CompanyType> companyTypeList = null;
            var cachedCompanyTypeList = await _distributedCache.GetAsync(cacheKey);
            if (cachedCompanyTypeList != null)
            {
                var serializedCustomerList = Encoding.UTF8.GetString(cachedCompanyTypeList);
                companyTypeList = JsonConvert.DeserializeObject<List<CompanyType>>(serializedCustomerList);
            }
            else
            {
                
                var query = "SELECT * FROM CompanyTypes";
                using (var connection = _context.CreateConnection())
                {
                    var companies = await connection.QueryAsync<CompanyType>(query);
                    companyTypeList = companies.ToList();
                    var serializedCustomerList = JsonConvert.SerializeObject(companyTypeList);
                    cachedCompanyTypeList = Encoding.UTF8.GetBytes(serializedCustomerList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await _distributedCache.SetAsync(cacheKey, cachedCompanyTypeList, options);
                }
                return Ok(companyTypeList);
            }
            return Ok(companyTypeList);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCompanyType(CompanyTypeForCreationDto companyType)
        {
            try
            {
                var Data = _mapper.Map<CompanyType>(companyType);
                var createdCompanyType = await _companyTypeRepo.CreateCompanyType(Data);
                return Ok(/*"CompanyTypeById", new { id = createdCompanyType.Id },*/ createdCompanyType);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateCompanyType(int id, CompanyTypeForCreationDto company)
        //{
        //    try
        //    {
        //        var dbCompany = await _companyTypeRepo.GetCompanyType(id);
        //        if (dbCompany == null)
        //            return NotFound();

        //        await _companyTypeRepo.UpdateCompanyType(id, company);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        //log error
        //        return StatusCode(500, ex.Message);
        //    }
        //}



    }
}
