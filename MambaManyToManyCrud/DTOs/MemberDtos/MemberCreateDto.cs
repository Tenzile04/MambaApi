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
            RuleFor(x => x.FullName).NotEmpty().NotNull().MaximumLength(100).MinimumLength(3);
            RuleFor(x=>x.LinkUrl).NotEmpty().NotNull().MaximumLength(100).MinimumLength(3);
        

        }
    }
}
