using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlClient;

namespace TicTacToe.Controllers
{
    internal class AuthorizeAttribute : ResultFilterAttribute, IActionFilter
    {
        DBAccess db = new DBAccess();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            SqlConnection connection2 = db.DBconnect();
            connection2.Open();
            string query = "Insert into Logging(RequestType , RequestStatus,Exception,Comment) values ( 'POST' , 'SUCCESFUL' , 'NONE' , 'PLAYER ADDED TO DATABASE SUCCESSFULLY')";
            SqlCommand sqlCommand = new SqlCommand(query, connection2);
            sqlCommand.ExecuteNonQuery();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string firstName = context.HttpContext.Request.Headers["FirstName"].ToString();
            string lastName = context.HttpContext.Request.Headers["LastName"].ToString();
            string Email = context.HttpContext.Request.Headers["Email"].ToString();
            SqlConnection connection2 = db.DBconnect();
            connection2.Open();
            string query = "select * from TTTPlayers where FirstName='" + firstName + "' and LastName='" + lastName + "' and Email='" + Email + "'";
            SqlCommand sqlCommand = new SqlCommand(query, connection2);
            sqlCommand.ExecuteNonQuery();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    SqlConnection connection3 = db.DBconnect();
                    connection3.Open();
                    string query2 = "Insert into Logging(RequestType , RequestStatus,Exception,Comment) values ( 'POST' , 'UNSUCCESFUL' , 'DUBLICATE ENTRY' , 'PLAYER ALREADY IN DATABASE')";
                    SqlCommand sqlCommand2 = new SqlCommand(query2, connection3);
                    sqlCommand2.ExecuteNonQuery();
                    throw new InvalidOperationException();
                }
            }
        }
    }
}