namespace RepairShop.Infrastructure.Entities
{
    public class OrderStatusLookup
    {
        public int OrderStatusLookupId { get; set; }
        public string? OrderStatusName { get; set; }
        public virtual ICollection<OrderStatus>? OrderStatuses { get; set; }
    }
}