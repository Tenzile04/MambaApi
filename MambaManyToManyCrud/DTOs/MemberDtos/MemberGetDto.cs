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
            RuleFor(x => x.FullName).NotEmpty().NotNull().MaximumLength(100).MinimumLength(3);
            RuleFor(x => x.LinkUrl).NotEmpty().NotNull().MaximumLength(100).MinimumLength(3);
        }
    }
}
