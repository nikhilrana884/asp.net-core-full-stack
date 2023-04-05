using System;

namespace e_lib.Models
{
    public class Books
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public int Quantity { get; set; }

        public DateTime PublishDate { get; set; }

        public string ImageUrl { get; set; }

        public int Status { get; set; }

    }
}
