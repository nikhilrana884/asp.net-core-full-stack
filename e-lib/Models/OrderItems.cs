using System;

namespace e_lib.Models
{
    public class OrderItems
    {
        public int Id { get; set; }

        public int OrderID { get; set; }

        public int BookID { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
