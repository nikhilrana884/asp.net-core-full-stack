using e_lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace e_lib.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
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
        

        [HttpGet]
        [Route("userList")]
        public Response userList(Users users)
        {

            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ELibCS").ToString());
            Response response = dal.userList(users, connection);
            return response;
        }

    }
}
