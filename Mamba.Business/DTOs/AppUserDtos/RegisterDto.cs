using FluentValidation;
using MambaManyToManyCrud.DTOs.MemberDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.DTOs.AppUserDtos
{
    public class RegisterDto
    {   
        public string UserName {  get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  

    }
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Fullname).NotEmpty().WithMessage("Bos ola bilmez")
                .NotNull().WithMessage("Null ola bilmez")
                .MaximumLength(50).WithMessage("Max 50 ola biler")
                .MinimumLength(3).WithMessage("Min 3 ola biler");
         RuleFor(x=>x.Password).NotEmpty().WithMessage("Bos ola bilmez")
                .NotNull().WithMessage("Null ola bilmez")
                .MaximumLength(30).WithMessage("Max 30 ola biler")
                .MinimumLength(8).WithMessage("Min 8 ola biler");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Bos ola bilmez")
             .NotNull().WithMessage("Null ola bilmez")
             .MaximumLength(50).WithMessage("Max 50 ola biler")
             .MinimumLength(3).WithMessage("Min 3 ola biler");


        }
    }
}
