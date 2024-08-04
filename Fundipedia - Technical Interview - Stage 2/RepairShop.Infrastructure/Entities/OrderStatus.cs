namespace RepairShop.Infrastructure.Entities
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public bool IsRushOrder { get; set; }
        public bool IsNewCustomer { get; set; }
        public bool IsLargeOrder { get; set; }
        public int? OrderTypeLookupId { get; set; }
        public int OrderStatusLookupId { get; set; }
        public int Priority { get; set; }
        public virtual OrderTypeLookup? OrderTypeLookup { get; set; }
        public virtual OrderStatusLookup? OrderStatusLookup { get; set; }
    }
}