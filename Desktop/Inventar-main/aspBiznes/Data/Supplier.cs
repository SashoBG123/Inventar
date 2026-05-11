namespace aspBiznes.Data
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int EIK { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTimeOffset DateRegister { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
