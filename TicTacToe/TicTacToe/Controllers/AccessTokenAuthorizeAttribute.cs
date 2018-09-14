using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlClient;

namespace TicTacToe.Controllers
{
    internal class AccessTokenAuthorizeAttribute : ResultFilterAttribute, IActionFilter
    {
        DBAccess db = new DBAccess();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            SqlConnection connection2 = db.DBconnect();
            connection2.Open();
            string query = "Insert into Logging(RequestType , RequestStatus,Exception,Comment) values ( 'PUT' , 'SUCCESFUL' , 'NONE' , 'GAME STARTED WITH PLAYER'S VALID ACCESS TOKEN')";
            SqlCommand sqlCommand = new SqlCommand(query, connection2);
            sqlCommand.ExecuteNonQuery();
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            int count = 0;
            string AccessToken = context.HttpContext.Request.Headers["AccessToken"].ToString();
            int PlayerNumber = Convert.ToInt32(context.HttpContext.Request.Headers["PlayerNumber"]);
            SqlConnection connection2 = db.DBconnect();
            connection2.Open();
            string query = "select * from TTTPlayers where AccessToken='" + AccessToken + "'";
            SqlCommand sqlCommand = new SqlCommand(query, connection2);
            sqlCommand.ExecuteNonQuery();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    SqlConnection connection3 = db.DBconnect();
                    connection3.Open();
                    string query2 = "Insert into Logging(RequestType , RequestStatus,Exception,Comment) values ( 'PUT' , 'UNSUCCESFUL' , 'ACESS DENIED' , 'NO PLAYER WITH SUCH ACCESS TOKEN')";
                    SqlCommand sqlCommand2 = new SqlCommand(query2, connection3);
                    sqlCommand2.ExecuteNonQuery();
                    throw new InvalidOperationException();
                }
            }
            connection2.Close();
            if (PlayerNumber==2)
            {
                connection2 = db.DBconnect();
                connection2.Open();
                query = "select * from TTTPlayers where IsPlaying=1";
                sqlCommand = new SqlCommand(query, connection2);
                sqlCommand.ExecuteNonQuery();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        count++;
                    }
                }
                connection2.Close();
                if (count != 1)
                {
                    SqlConnection connection3 = db.DBconnect();
                    connection3.Open();
                    string query2 = "Insert into Logging(RequestType , RequestStatus,Exception,Comment) values ( 'PUT' , 'UNSUCCESFUL' , 'NO FIRST PLAYER' , 'CANNOT ADD SECOND PLAYER WITHOUT FIRST PLAYER')";
                    SqlCommand sqlCommand2 = new SqlCommand(query2, connection3);
                    sqlCommand2.ExecuteNonQuery();
                    throw new Exception();
                }
            }
        }
    }
}