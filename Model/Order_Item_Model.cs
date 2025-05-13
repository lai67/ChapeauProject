namespace Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
    }
}