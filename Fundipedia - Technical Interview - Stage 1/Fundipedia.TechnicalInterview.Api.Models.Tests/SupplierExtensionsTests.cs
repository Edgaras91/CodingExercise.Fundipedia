using Fundipedia.TechnicalInterview.Model.Extensions;
using Fundipedia.TechnicalInterview.Model.Supplier;

namespace Fundipedia.TechnicalInterview.Api.Models.Tests
{
    public class SupplierExtensionsTests
    {
        [SetUp]
        public void IsActiveWithValidActivationDateShouldReturnTrue()
        {
            var supplier = new Supplier { ActivationDate = DateTime.UtcNow.AddDays(2) };
            Assert.IsTrue(supplier.IsActive());
        }

        [Test]
        public void IsActiveWithInValidActivationDateShouldReturnTrue()
        {
            var supplier = new Supplier { ActivationDate = DateTime.MinValue };
            Assert.IsFalse(supplier.IsActive());
        }
    }
}