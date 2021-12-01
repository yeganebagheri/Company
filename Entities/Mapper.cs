using AutoMapper;
using DapperASPNetCore.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Entities
{
    public class Mapper : Profile
    {
        
            public Mapper()
            {
                CreateMap<CompanyForCreationDto, Company>();
                CreateMap<CompanyTypeForCreationDto, CompanyType>();
                CreateMap<LoginViewModel, Employee>();
                
            }
        
       
    }
}
