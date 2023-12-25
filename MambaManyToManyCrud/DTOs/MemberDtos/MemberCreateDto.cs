using FluentValidation;
using MambaManyToManyCrud.DTOs.ProfessionDtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace MambaManyToManyCrud.DTOs.MemberDtos
{
    public class MemberCreateDto
    {
        public string FullName {  get; set; }
        public string LinkUrl { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public  List<int> ProfessionIds { get; set; }
        
    }
    public class MemberCreateDtoValidator : AbstractValidator<MemberCreateDto>
    {
        public MemberCreateDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Bos ola bilmez")
                .NotNull().WithMessage("Null ola bilmez")
                .MaximumLength(100).WithMessage("Max 100 ola biler")
                .MinimumLength(3).WithMessage("Min 3 ola biler");
            RuleFor(x=>x.LinkUrl).NotNull().WithMessage("Null ola bilmez");
        

        }
    }
}
