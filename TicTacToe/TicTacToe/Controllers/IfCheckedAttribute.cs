using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlClient;

namespace TicTacToe.Controllers
{
    internal class IfCheckedAttribute : ResultFilterAttribute, IActionFilter
    {
        DBAccess db = new DBAccess();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            SqlConnection connection2 = db.DBconnect();
            connection2.Open();
            string query = "Insert into Logging(RequestType , RequestStatus,Exception,Comment) values ( 'PUT' , 'SUCCESFUL' , 'NONE' , 'PLAYER SELECTED A BOX')";
            SqlCommand sqlCommand = new SqlCommand(query, connection2);
            sqlCommand.ExecuteNonQuery();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int count = 0;
            int PlayerNumber = Convert.ToInt32(context.HttpContext.Request.Headers["PlayerNumber"]);
            SqlConnection connection2 = db.DBconnect();
            connection2.Open();
            string query = "select * from TTTPlayers where IsPlaying=1";
            SqlCommand sqlCommand = new SqlCommand(query, connection2);
            sqlCommand.ExecuteNonQuery();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while(reader.Read())
                {
                    count++;
                    if(reader["NumberString"].ToString().Contains(PlayerNumber.ToString()))
                    {
                        SqlConnection connection3 = db.DBconnect();
                        connection3.Open();
                        string query2 = "Insert into Logging(RequestType , RequestStatus,Exception,Comment) values ( 'PUT' , 'UNSUCCESFUL' , 'DUBLICATE ENTRY' , 'BOX ALREADY TICKED')";
                        SqlCommand sqlCommand2 = new SqlCommand(query2, connection3);
                        sqlCommand2.ExecuteNonQuery();
                        throw new Exception();
                    }
                }
                if(count!=2)
                {
                    SqlConnection connection3 = db.DBconnect();
                    connection3.Open();
                    string query2 = "Insert into Logging(RequestType , RequestStatus,Exception,Comment) values ( 'PUT' , 'UNSUCCESFUL' , 'TWO PLAYERS NOT PLAYING' , 'NEED TWO PLAYERS FOR A GAME')";
                    SqlCommand sqlCommand2 = new SqlCommand(query2, connection3);
                    sqlCommand2.ExecuteNonQuery();
                    throw new Exception();
                }
            }
            connection2.Close();
        }
    }
}