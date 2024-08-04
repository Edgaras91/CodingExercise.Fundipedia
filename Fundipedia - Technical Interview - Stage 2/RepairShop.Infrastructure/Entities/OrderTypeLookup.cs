namespace RepairShop.Infrastructure.Entities
{
    public class OrderTypeLookup
    {
        public int OrderTypeId { get; set; }
        public string? OrderTypeName { get; set; }
        public virtual ICollection<OrderStatus>? OrderStatuses { get; set; }
    }
}