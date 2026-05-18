namespace aspBiznes.Models
{
    public class ReportsViewModel
    {
        public int ItemsCount { get; set; }

        public int CategoriesCount { get; set; }

        public int SuppliersCount { get; set; }

        public int OrdersCount { get; set; }

        public int TotalStock { get; set; }

        public int LowStockItems { get; set; }

        public int OutOfStockItems { get; set; }
    }
}
