using FluentValidation;

namespace MambaManyToManyCrud.DTOs.ProfessionDtos
{
    public class ProfessionGetDto
    {
        public string Name {  get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
    public class ProfessionGetDtoValidator : AbstractValidator<ProfessionGetDto>
    {
        public ProfessionGetDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bos ola bilmez")
          .NotNull().WithMessage("Null ola bilmez")
          .MaximumLength(50).WithMessage("Max 50 ola biler")
          .MinimumLength(3).WithMessage("Min 3 ola biler");

        }
    }
}
