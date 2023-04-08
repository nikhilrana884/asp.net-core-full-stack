using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace e_lib.Models
{
    public class DAL
    {
        public Response register(Users users, SqlConnection connection )
        {
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FIrstName", users.FirstName);

            cmd.Parameters.AddWithValue("@LastName", users.LastName);

            cmd.Parameters.AddWithValue("@Password", users.Password);

            cmd.Parameters.AddWithValue("@Email", users.Email);

            cmd.Parameters.AddWithValue("@Fund", 0);
            
            cmd.Parameters.AddWithValue("@Type", "Users");

            cmd.Parameters.AddWithValue("@Status", "Pending");

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage= "User registered successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User registration failed.";

            }

            return response;

        }

        public Response login(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_login", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dataTable= new DataTable();
            da.Fill(dataTable);
            Response response= new Response();
            Users user = new Users();
            if(dataTable.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dataTable.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dataTable.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dataTable.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dataTable.Rows[0]["Email"]);
                user.Type = Convert.ToString(dataTable.Rows[0]["Type"]);
                response.StatusCode = 200;
                response.StatusMessage = "User is valid";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is invalid";
                response.user = null;
            }
            return response;



        }

        public Response viewUser(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("p_viewUser", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);

            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            Response response = new Response();
            Users user= new Users();
            if (dataTable.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dataTable.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dataTable.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dataTable.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dataTable.Rows[0]["Email"]);
                user.Type = Convert.ToString(dataTable.Rows[0]["Type"]);
                user.Fund = Convert.ToDecimal(dataTable.Rows[0]["Fund"]);
                user.CreatedOn = Convert.ToDateTime(dataTable.Rows[0]["CreatedOn"]);
                user.Password = Convert.ToString(dataTable.Rows[0]["Password"]);

                response.StatusCode = 200;
                response.StatusMessage = "User exists";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User does not exist.";
                response.user = user;   
            }
            return response;



        }

        public Response updateProfile(Users users, SqlConnection connection)
        {

            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_updateProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

       
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Record updated successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Record not updated. Try again.";
            }
            return response;



        }

        public Response addToCart(Cart cart, SqlConnection connection)
        {

            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", cart.UserID);
            cmd.Parameters.AddWithValue("@BookID", cart.BookID);
            cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();


            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item added successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be added.";
            }
            return response;



        }

        public Response placeOrder(Users users, SqlConnection connection)
        {

            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_PlaceOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", users.ID);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();


            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order placed successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order could not be placed.";
            }
            return response;



        }

        public Response orderList(Users users, SqlConnection connection)
        {

            Response response = new Response();
            List<Orders> listOrder = new List<Orders>();
            SqlDataAdapter da = new SqlDataAdapter("sp_OrderList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);

            DataTable dt = new DataTable();
            da.Fill(dt);
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    order.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                    order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                    listOrder.Add(order);

                }


                if (listOrder.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order details fetched";
                    response.ListOrders= listOrder;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order could not be fetched.";
                    response.ListOrders = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order could not be fetched.";
                response.ListOrders = null;
            }
            return response;


        }

        public Response addUpdateMedicine(Books books, SqlConnection connection)
        {

            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddUpdateMedicine", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", books.Name);
            cmd.Parameters.AddWithValue("@Author", books.Author);
            cmd.Parameters.AddWithValue("@UnitPrice", books.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", books.Discount);
            cmd.Parameters.AddWithValue("@Quantity", books.Quantity);
            cmd.Parameters.AddWithValue("@PublishDate", books.PublishDate);
            cmd.Parameters.AddWithValue("@ImageUrl", books.ImageUrl);
            cmd.Parameters.AddWithValue("@Status", books.Status);
            cmd.Parameters.AddWithValue("@Type", books.Type);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();


            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Book added successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Book could not be added.";
            }
            return response;



        }

        public Response userList(Users users, SqlConnection connection)
        {

            Response response = new Response();
            List<Users> listUsers = new List<Users>();
            SqlDataAdapter da = new SqlDataAdapter("sp_UserList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Users user = new Users();
                    user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    user.Password = Convert.ToString(dt.Rows[i]["Password"]);
                    user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    user.Fund = Convert.ToDecimal(dt.Rows[i]["Fund"]);
                    user.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                    user.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);

                    listUsers.Add(user);

                }


                if (listUsers.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Users details fetched";
                    response.listUsers = listUsers;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Users could not be fetched.";
                    response.listUsers = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Users could not be fetched.";
                response.listUsers = null;
            }
            return response;


        }

    }
}
