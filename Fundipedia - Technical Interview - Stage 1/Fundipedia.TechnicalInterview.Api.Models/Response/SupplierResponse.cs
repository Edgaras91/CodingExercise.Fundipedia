namespace Fundipedia.TechnicalInterview.Api.Models.Response
{
    public record SupplierResponse(Guid Id, string Title, string FirstName, string LastName, DateTime ActivationDate);
}