using FluentValidation;

namespace MambaManyToManyCrud.DTOs.MemberDtos
{
    public class MemberGetDto
    {
        public string FullName { get; set; }
        public string LinkUrl { get; set; }
        

    }
    public class MemberGetDtoValidator : AbstractValidator<MemberGetDto>
    {
        public MemberGetDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Bos ola bilmez")
                .NotNull().WithMessage("Null ola bilmez")
                .MaximumLength(100).WithMessage("Max 100 ola biler")
                .MinimumLength(3).WithMessage("Min 3 ola biler");
            RuleFor(x => x.LinkUrl).NotNull().WithMessage("Null ola bilmez");
        }
    }
}
