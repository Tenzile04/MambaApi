using FluentValidation;

namespace MambaManyToManyCrud.DTOs.MemberDtos
{
    public class MemberUpdateDto
    {
        public string FullName { get; set; }
        public string LinkUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        public List<int> ProfessionIds { get; set; }
    }
    public class MemberUpdateDtoValidator : AbstractValidator<MemberUpdateDto>
    {
        public MemberUpdateDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().NotNull().MaximumLength(100).MinimumLength(3);
            RuleFor(x => x.LinkUrl).NotEmpty().NotNull().MaximumLength(100).MinimumLength(3);

        }
    }
}
