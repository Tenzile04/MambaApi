using FluentValidation;

namespace MambaManyToManyCrud.DTOs.ProfessionDtos
{
    public class ProfessionCreateDto
    {
        public string Name {  get; set; }
    }
    public class ProfessionCreateDtoValidator : AbstractValidator<ProfessionCreateDto>
    {
        public ProfessionCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(50).MinimumLength(3);

        }
    }
}
