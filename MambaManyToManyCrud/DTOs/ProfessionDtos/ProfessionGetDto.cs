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
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(50).MinimumLength(3);

        }
    }
}
