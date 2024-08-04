using System.Linq;
using Fundipedia.TechnicalInterview.Api.Models.Requests;
using Fundipedia.TechnicalInterview.Api.Models.Response;
using Fundipedia.TechnicalInterview.Model.Supplier;

namespace Fundipedia.TechnicalInterview.Mappers
{
    public static class SupplierMapper
    {
        public static SupplierResponse ToResponseDto(this Supplier supplier)
        {
            return new SupplierResponse(supplier.Id, supplier.Title, supplier.FirstName, supplier.LastName, supplier.ActivationDate);
        }

        public static Supplier ToDomainDto(this SupplierRequest supplierRequest)
        {
            return new Supplier
            {
                Id = supplierRequest.Id,
                Title = supplierRequest.Title,
                FirstName = supplierRequest.FirstName,
                LastName = supplierRequest.LastName,
                ActivationDate = supplierRequest.ActivationDate,
                Emails = supplierRequest.Emails.Select(email => new Fundipedia.TechnicalInterview.Model.Supplier.Email
                {
                    Id = email.Id,
                    EmailAddress = email.EmailAddress,
                    IsPreferred = email.IsPreferred
                }).ToList(),
                Phones = supplierRequest.Phones.Select(phone => new Fundipedia.TechnicalInterview.Model.Supplier.Phone
                {
                    Id = phone.Id,
                    PhoneNumber = phone.PhoneNumber,
                    IsPreferred = phone.IsPreferred
                }).ToList()
            };
        }
    }
}