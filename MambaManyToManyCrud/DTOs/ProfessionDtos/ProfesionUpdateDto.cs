using FluentValidation;

namespace MambaManyToManyCrud.DTOs.ProfessionDtos
{
    public class ProfesionUpdateDto
    {
        public string Name { get; set; }
    }
    public class ProfessionUpdateDtoValidator : AbstractValidator<ProfesionUpdateDto>
    {
        public ProfessionUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(50).MinimumLength(3);

        }
    }
}
