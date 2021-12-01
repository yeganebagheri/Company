using AutoMapper;
using DapperASPNetCore.Contracts;
using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using DapperASPNetCore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {

        private readonly ICompanyRepository _companyRepo;
        private readonly CompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IRabbitMqRepository _Rabbit;

        public CompaniesController(ICompanyRepository companyRepo, CompanyService companyService , IRabbitMqRepository rabbitMq, IMapper mapper)
        {
            _companyRepo = companyRepo;
            _companyService = companyService;
            _mapper = mapper;
            _Rabbit = rabbitMq;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
               var currentUser = HttpContext.User;
                var companies = await _companyRepo.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

       

        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var company = await _companyRepo.GetCompany(id);
                if (company == null)
                    return NotFound();

                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyForCreationDto company )
        {
           
            try
            {
               var Data= _mapper.Map<Company>(company);
                var createdCompany = await _companyRepo.CreateCompany(Data);
                
                var createdLog = new MongoLog
                {
                    NameLog = company.Name
                };
                _companyService.Create(createdLog);
                _Rabbit.Producer(Data);
                return Ok(/*"CompanyById", new { id = createdCompany.Id }, */createdCompany);
                
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Logs")]
        
        public ActionResult<List<MongoLog>> GetAllLogs() =>
           _companyService.Get();

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyForUpdateDto company)
        {
            try
            {
                var dbCompany = await _companyRepo.GetCompany(id);
                if (dbCompany == null)
                    return NotFound();
                var Data = _mapper.Map<Company>(company);
                await _companyRepo.UpdateCompany(id, Data);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var dbCompany = await _companyRepo.GetCompany(id);
                if (dbCompany == null)
                    return NotFound();

                await _companyRepo.DeleteCompany(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpGet("ByEmployeeId/{id}")]
        //public async Task<IActionResult> GetCompanyForEmployee(int id)
        //{
        //    try
        //    {
        //        var company = await _companyRepo.GetCompanyByEmployeeId(id);
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

        //[HttpGet("{id}/MultipleResult")]
        //public async Task<IActionResult> GetCompanyEmployeesMultipleResult(int id)
        //{
        //    try
        //    {
        //        var company = await _companyRepo.GetCompanyEmployeesMultipleResults(id);
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
    }

}
