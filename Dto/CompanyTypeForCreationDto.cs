using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Dto
{
    public class CompanyTypeForCreationDto
    {
        public string Title { get; set; }
    }

    public class CompanyTypeDtoValidator : AbstractValidator<CompanyTypeForCreationDto>
    {
        public CompanyTypeDtoValidator()
        {
            RuleFor(x => x.Title).Length(6, 16);
        }
    }
}
