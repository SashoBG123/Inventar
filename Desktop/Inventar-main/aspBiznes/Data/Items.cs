namespace aspBiznes.Data
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public Category Categories { get; set; }

        public int SupplierId { get; set; }

        public Supplier Suppliers { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateOnly? ExpiryDate { get; set; }

        public DateTimeOffset DateRegister { get; set; }

        public ICollection<CartItem> CartItems { get; set; }


    }
}
