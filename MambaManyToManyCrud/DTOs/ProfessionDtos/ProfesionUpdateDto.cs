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
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bos ola bilmez")
         .NotNull().WithMessage("Null ola bilmez")
         .MaximumLength(50).WithMessage("Max 50 ola biler")
         .MinimumLength(3).WithMessage("Min 3 ola biler");
      

        }
    }
}
