using System.Net;
using Fundipedia.TechnicalInterview.Api.Models.Requests;
using Fundipedia.TechnicalInterview.Api.Models.Response;
using Fundipedia.TechnicalInterview.Domain;
using Fundipedia.TechnicalInterview.Mappers;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Net.Http;

namespace Fundipedia.TechnicalInterview.IntegrationTests.Controllers
{
    public class SupplierControllerTests
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory;

        public SupplierControllerTests()
        {
            _webApplicationFactory = new WebApplicationFactory<Program>();
        }

        [Test]
        public async Task PostSupplierShouldCreate()
        {
            var request = CreateSupplierRequest();

            var result = await _webApplicationFactory.CreateClient()
                .PostAsync("/api/Suppliers/", JsonContent.Create(request));

            ClassicAssert.True(result.IsSuccessStatusCode);
        }

        [Test]
        public async Task GetSuppliersShouldReturnListOfSuppliers()
        {
            var supplierSeed1 = CreateSupplierRequest().ToDomainDto();
            var supplierSeed2 = CreateSupplierRequest().ToDomainDto();

            var scope = _webApplicationFactory.Services.CreateScope();
            await scope.ServiceProvider.GetRequiredService<SupplierService>().InsertSupplier(supplierSeed1);
            await scope.ServiceProvider.GetRequiredService<SupplierService>().InsertSupplier(supplierSeed2);

            var result = await _webApplicationFactory.CreateClient().GetAsync("/api/Suppliers/");
            var stringContent = await result.Content.ReadAsStringAsync();
            var resultModels = JsonConvert.DeserializeObject<SupplierResponse[]>(stringContent);

            ClassicAssert.NotNull(resultModels);
            ClassicAssert.AreEqual(resultModels!.Length, 2);
            Assert.That(resultModels[0].Id, Is.EqualTo(supplierSeed1.Id));
            Assert.That(resultModels[1].Id, Is.EqualTo(supplierSeed2.Id));
        }

        [Test]
        public async Task GetSupplierByIdShouldReturnSupplier()
        {
            var supplierSeed = CreateSupplierRequest().ToDomainDto();

            var scope = _webApplicationFactory.Services.CreateScope();
            await scope.ServiceProvider.GetRequiredService<SupplierService>().InsertSupplier(supplierSeed);
            var result = await _webApplicationFactory.CreateClient().GetAsync($"/api/Suppliers/{supplierSeed.Id}");
            ClassicAssert.True(result.IsSuccessStatusCode);

            var resultModel = JsonConvert.DeserializeObject<SupplierResponse>(await result.Content.ReadAsStringAsync());

            //TODO: Assert every property programatically, with fewer lines of code. FluentAssertions could be used, maybe NUnit 4 can do it too?
            ClassicAssert.NotNull(resultModel);
            ClassicAssert.AreEqual(resultModel.Id, supplierSeed.Id);
            ClassicAssert.AreEqual(resultModel.FirstName, supplierSeed.FirstName);
            ClassicAssert.AreEqual(resultModel.LastName, supplierSeed.LastName);
            ClassicAssert.AreEqual(resultModel.Title, supplierSeed.Title);
            ClassicAssert.AreEqual(resultModel.ActivationDate, supplierSeed.ActivationDate);
        }

        [Test]
        public async Task GetSupplierWhenDoesNotExistShouldReturnNotFound()
        {
            var result = await _webApplicationFactory.CreateClient().GetAsync($"/api/Suppliers/{Guid.NewGuid()}");
            ClassicAssert.True(result.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public async Task DeleteSupplierShouldDeleteInactiveSupplier()
        {
            var scope = _webApplicationFactory.Services.CreateScope();
            var supplierService = scope.ServiceProvider.GetRequiredService<SupplierService>();

            var supplierSeed = CreateSupplierRequest().ToDomainDto();
            supplierSeed.ActivationDate = DateTime.MinValue; //Bug

            await supplierService.InsertSupplier(supplierSeed);

            var result = await _webApplicationFactory.CreateClient().DeleteAsync($"/api/Suppliers/{supplierSeed.Id}");
            ClassicAssert.True(result.IsSuccessStatusCode);
            var allSuppliers = await supplierService.GetSuppliers();
            ClassicAssert.AreEqual(allSuppliers.Count, 0);
        }

        private static SupplierRequest CreateSupplierRequest()
        {
            return new SupplierRequest
            (
                Guid.NewGuid(),
                "Queen",
                "Khaleesi",
                "Targaryen",
                DateTime.UtcNow.AddDays(2),
                new List<Email> { new(Guid.NewGuid(), "spongebob@square.pants", true) },
                new List<Phone> { new(Guid.NewGuid(), "123456789", true) }
            );
        }
    }
}