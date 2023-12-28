﻿using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MambaManyToManyCrud.DTOs.MemberDtos
{
    public class MemberUpdateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string LinkUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        public List<int> ProfessionIds { get; set; }
    }
    public class MemberUpdateDtoValidator : AbstractValidator<MemberUpdateDto>
    {
        public MemberUpdateDtoValidator()
        {
          RuleFor(x => x.FullName).NotEmpty().WithMessage("Bos ola bilmez")
           .NotNull().WithMessage("Null ola bilmez")
           .MaximumLength(100).WithMessage("Max 100 ola biler")
           .MinimumLength(3).WithMessage("Min 3 ola biler");
          RuleFor(x => x.LinkUrl).NotNull().WithMessage("Null ola bilmez");

        }
    }
}
