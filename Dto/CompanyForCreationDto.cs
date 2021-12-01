using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Dto
{
    public class CompanyForCreationDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public int CompanyTypeId { get; set; }

    }

    public class CompanyDtoValidator : AbstractValidator<CompanyForCreationDto>
    {
        public CompanyDtoValidator()
        {
            RuleFor(x => x.CompanyTypeId).NotNull();
            RuleFor(x => x.Name).Length(6, 16);
            RuleFor(x => x.Address).Length(6, 46);
            RuleFor(x => x.Country).NotNull();
        }
    }
}
