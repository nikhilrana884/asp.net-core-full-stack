using System.Collections.Generic;

namespace e_lib.Models
{
    public class Response
    {
        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public List<Users> listUsers { get; set; }

        public Users user { get; set; }

        public List<Books> ListBooks { get; set; }

        public Books book { get; set; }

        public List<Cart> ListCart { get; set; }

        public List<Orders> ListOrders { get; set; }

        public Orders order { get; set; }

        public List<OrderItems> ListItems { get; set; }

        public OrderItems orderItem  { get; set; }




    }
}
 