using System.ComponentModel.DataAnnotations;

namespace Fundipedia.TechnicalInterview.Api.Models.Validators
{
    public class DaysGreaterThanValidator : ValidationAttribute
    {
        public int Days { get; }

        public DaysGreaterThanValidator(int days)
        {
            Days = days;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateTimeValue && dateTimeValue > DateTime.UtcNow.AddDays(1))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? $"Make sure your date is > than {Days} days");
        }
    }
}