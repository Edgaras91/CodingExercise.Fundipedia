using System.ComponentModel.DataAnnotations;
using Fundipedia.TechnicalInterview.Api.Models.Validators;

namespace Fundipedia.TechnicalInterview.Api.Models.Requests
{
    public record SupplierRequest(Guid Id,
        string Title,
        string FirstName,
        string LastName,
        [DaysGreaterThanValidator(1)] DateTime ActivationDate,
        ICollection<Email> Emails,
        ICollection<Phone> Phones);

    public record Phone(Guid Id, [MaxLength(20)] [RegularExpression("\\d+")] string PhoneNumber, bool IsPreferred);

    public record Email(Guid Id, [RegularExpression(Email.EmailValidationRegex)] string EmailAddress, bool IsPreferred)
    {
        //Regex Source: https://stackoverflow.com/a/201378/8979358
        private const string EmailValidationRegex = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";
    }
}