namespace TrackOrdersApp.Models
{
    public enum OrderStatus { Pending, Processing, Shipped, Cancelled, Paid }

    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
