using e_lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace e_lib.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BooksController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("addToCart")]
        public Response addToCart(Cart cart)
        {

            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ELibCS").ToString());
            Response response = dal.addToCart(cart, connection);
            return response;
        }



        [HttpPost]
        [Route("placeOrder")]
        public Response placeOrder(Users users)
        {

            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ELibCS").ToString());
            Response response = dal.placeOrder(users, connection);
            return response;
        }


        [HttpPost]
        [Route("addUpdateMedicine")]
        public Response addUpdateMedicine(Books books)
        {

            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ELibCS").ToString());
            Response response = dal.addUpdateMedicine(books, connection);
            return response;
        }



    }
}
