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
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bos ola bilmez")
              .NotNull().WithMessage("Null ola bilmez")
              .MaximumLength(50).WithMessage("Max 50 ola biler")
              .MinimumLength(3).WithMessage("Min 3 ola biler");

        }
    }
}
