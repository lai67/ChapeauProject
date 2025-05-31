namespace Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int MenuItem { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
    }
}