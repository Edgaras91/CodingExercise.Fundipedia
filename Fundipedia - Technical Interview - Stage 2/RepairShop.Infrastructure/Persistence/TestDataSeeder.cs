using RepairShop.Infrastructure.Entities;

namespace RepairShop.Infrastructure.Persistence
{
    public static class TestDataSeeder
    {
        public static void SeedData(OrdersDbContext orderBdContext)
        {
            var orderTypeLookups = new[]
            {
                new OrderTypeLookup { OrderTypeId = 1, OrderTypeName = "Repair" },
                new OrderTypeLookup { OrderTypeId = 2, OrderTypeName = "Hire" }
            };

            var orderStatusLookups = new[]
            {
                new OrderStatusLookup { OrderStatusLookupId = 1, OrderStatusName = "Confirmed" },
                new OrderStatusLookup { OrderStatusLookupId = 2, OrderStatusName = "Closed" },
                new OrderStatusLookup { OrderStatusLookupId = 3, OrderStatusName = "AuthorisationRequired" }
            };

            var orderStatuses = new[]
            {
                //1 Large repair orders for new customers should be closed
                new OrderStatus
                {
                    IsLargeOrder = true,
                    IsNewCustomer = true,
                    OrderTypeLookupId = orderTypeLookups.First(x => x.OrderTypeName == "Repair").OrderTypeId,
                    OrderStatusLookupId =
                        orderStatusLookups.First(x => x.OrderStatusName == "Closed").OrderStatusLookupId,
                    Priority = 1
                },
                //2 Large rush hire orders should always be closed
                new OrderStatus
                {
                    IsLargeOrder = true,
                    IsRushOrder = true,
                    OrderTypeLookupId = orderTypeLookups.First(x => x.OrderTypeName == "Hire").OrderTypeId,
                    OrderStatusLookupId =
                        orderStatusLookups.First(x => x.OrderStatusName == "Closed").OrderStatusLookupId,
                    Priority = 2
                },
                //3 Large repair orders always require authorisation
                new OrderStatus
                {
                    IsLargeOrder = true,
                    OrderTypeLookupId = orderTypeLookups.First(x => x.OrderTypeName == "Repair").OrderTypeId,
                    OrderStatusLookupId = orderStatusLookups.First(x => x.OrderStatusName == "AuthorisationRequired")
                        .OrderStatusLookupId,
                    Priority = 3
                },
                //4 All rush orders for new customers always require authorisation
                new OrderStatus
                {
                    IsRushOrder = true,
                    IsNewCustomer = true,
                    OrderStatusLookupId = orderStatusLookups.First(x => x.OrderStatusName == "AuthorisationRequired")
                        .OrderStatusLookupId,
                    Priority = 4
                },
                //5 All other orders should be confirmed
                //If above entry doesn't match, we will return status "Confirmed" in service code.
            };


            orderBdContext.OrderTypeLookups.AddRange(orderTypeLookups);
            orderBdContext.OrderStatusLookups.AddRange(orderStatusLookups);
            orderBdContext.OrderStatuses.AddRange(orderStatuses);

            orderBdContext.SaveChanges();
        }
    }
}