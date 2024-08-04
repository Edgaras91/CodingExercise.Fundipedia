using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace RepairShop.Api.Tests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory = new();

        //1 Large repair orders for new customers should be closed
        [TestCase(false, "Repair", true, true, "Closed")]
        //2 Large rush hire orders should always be closed
        [TestCase(true, "Hire", false, true, "Closed")]
        //3 Large repair orders always require authorisation
        [TestCase(false, "Repair", false, true, "AuthorisationRequired")]
        //4 All rush orders for new customers always require authorisation
        [TestCase(true, null, true, false, "AuthorisationRequired")]
        //5 All other orders should be confirmed
        [TestCase(false, null, false, false, "Confirmed")]
        public async Task GetOrderStatusShouldMatchBusinessRequirements(bool isRushOrder, string? orderType,
            bool isNewCustomer, bool isLargeOrder,
            string expected)
        {
            // Arrange
            var query =
                $"/orders/status?IsRushOrder={isRushOrder}&IsNewCustomer={isNewCustomer}&IsLargeOrder={isLargeOrder}";
            if (orderType != null) query += $"&OrderType={orderType}";

            var client = _webApplicationFactory.CreateClient();

            // Act
            var response = await client.GetStringAsync(query);

            // Assert
            Assert.AreEqual(response, expected);
        }
    }
}