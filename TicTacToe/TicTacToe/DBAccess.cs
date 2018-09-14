using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TicTacToe
{
    public class DBAccess
    {
        public SqlConnection DBconnect()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=.;Initial Catalog=TicTacToe;User ID=Sa;Password=test123!@#";
            return connection;
        }
        public string GetStatus()
        {
            int count = 0;
            int result1 = 0;
            int result2 = 0;
            int flag = 0;
            SqlConnection connection = DBconnect();
            SqlConnection connection2 = DBconnect();
            connection2.Open();
            connection.Open();
            string query2 = "select * from TTTPlayers where IsPlaying=1";
            SqlCommand sqlCommand2 = new SqlCommand(query2, connection);
            sqlCommand2.ExecuteNonQuery();
            using (SqlDataReader reader = sqlCommand2.ExecuteReader())
            {
                while(reader.Read())
                {
                    count += reader["NumberString"].ToString().Length-1;
                    if(flag == 0)
                    {
                        flag++;
                        result1 = check(reader["NumberString"].ToString());
                    }
                    else
                    {
                        result2 = check(reader["NumberString"].ToString());
                    }
                }
            }
            if(result1==1&&result2==1)
            {
                return "INVALID STATE";
            }
            if(result1==1)
            {
                return "PLAYER 1 WINS";
            }
            if (result2 == 1)
            {
                return "PLAYER 2 WINS";
            }
            if(count==9)
            {
                return "GAME OVER :TIE";
            }
            return "IN PROGRESS";
        }
        public int check(string NumberString)
        {
            if (NumberString.Contains("1") && NumberString.Contains("2") && NumberString.Contains("3"))
                return 1;
            else if (NumberString.Contains("4") && NumberString.Contains("5") && NumberString.Contains("6"))
                return 1;
            else if (NumberString.Contains("7") && NumberString.Contains("8") && NumberString.Contains("9"))
                return 1;
            else if (NumberString.Contains("1") && NumberString.Contains("4") && NumberString.Contains("7"))
                return 1;
            else if (NumberString.Contains("2") && NumberString.Contains("5") && NumberString.Contains("8"))
                return 1;
            else if (NumberString.Contains("3") && NumberString.Contains("6") && NumberString.Contains("9"))
                return 1;
            else if (NumberString.Contains("1") && NumberString.Contains("5") && NumberString.Contains("9"))
                return 1;
            else if (NumberString.Contains("3") && NumberString.Contains("5") && NumberString.Contains("7"))
                return 1;
            else
                return 0;
        }
        public void AddMove(Token move)
        {
            SqlConnection connection = DBconnect();
            connection.Open();
            string query2 = "update TTTPlayers SET NumberString += '"+ move.PlayerNumber + "' where AccessToken= '"+ move.AccessToken + "'";
            SqlCommand sqlCommand2 = new SqlCommand(query2, connection);
            sqlCommand2.ExecuteNonQuery();
        }
        public void StartGameWith(Token AccessToken)
        {
            SqlConnection connection = DBconnect();
            connection.Open();
            string query2 = "update TTTPlayers SET IsPlaying = 0 , NumberString = '0' where Id>=1";
            SqlCommand sqlCommand2= new SqlCommand(query2, connection);
            sqlCommand2.ExecuteNonQuery();
            SqlConnection connection2 = DBconnect();
            connection2.Open();
            string query = "update TTTPlayers SET IsPlaying = 1 where AccessToken='" + AccessToken.AccessToken.Substring(0,36) + "'";
            SqlCommand sqlCommand = new SqlCommand(query, connection2);
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            connection2.Close();
            if(AccessToken.PlayerNumber==2)
            {
                connection2 = DBconnect();
                connection2.Open();
                query = "update TTTPlayers SET IsPlaying = 1 where AccessToken='" + AccessToken.AccessToken.Substring(36, 36) + "'";
                sqlCommand = new SqlCommand(query, connection2);
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void AddToDb(TTTPlayersDB player)
        {
            try
            {
                using (SqlConnection connection = DBconnect())
                {
                    SqlConnection connection2 = DBconnect();
                    connection.Open();
                    connection2.Open();
                    string query = "select * from TTTPlayers where FirstName='" + player.FirstName + "' and LastName='" + player.LastName + "' and Email='" + player.Email + "'";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.ExecuteNonQuery();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            string guid = Guid.NewGuid().ToString();
                            string number = "0";
                            query = "Insert into TTTPlayers(FirstName , LastName , Email,AccessToken,NumberString,IsPlaying) values ( '" + player.FirstName + "' , '" + player.LastName + "' , '" + player.Email + "' , '" + guid + "' , '" + number + "' , 0 )";
                            SqlCommand sqlCommand2 = new SqlCommand(query, connection2);
                            sqlCommand2.ExecuteNonQuery();
                        }
                        //while (reader.Read())
                        //{
                        //    Console.WriteLine(reader["IsBooked"].ToString());
                        //    if (reader["IsBooked"].ToString() == "False")
                        //    {
                        //        Execute(x, id, 2);
                        //        Console.WriteLine("Booked Successfully!");
                        //        //return Convert.ToInt32(reader["Price"]);
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine("Product Already Booked , Check again later..");
                        //        Logging.Instance.Log("Exiting Book function , product already booked", "C:\\LogFile.txt");
                        //        Logging.Instance.Log("product already booked", "C:\\SaveFile.txt");
                        //    }
                        //}
                    }
                    //query = "Insert into Users(FirstName , LastName , UserName , AccessToken) values ( @firstName , @lastName , @userName , @accessToken)";
                    //SqlCommand sqlCommand = new SqlCommand(query, conn);
                    //sqlCommand.Parameters.Add(new SqlParameter("firstName", firstName));
                    //sqlCommand.Parameters.Add(new SqlParameter("lastName", lastName));
                    //sqlCommand.Parameters.Add(new SqlParameter("userName", userName));
                    //sqlCommand.Parameters.Add(new SqlParameter("accessToken", accessToken));
                    //sqlCommand.ExecuteNonQuery();
                    connection.Close();
                    connection2.Close();
                }
            }
            catch (Exception e)
            {
                 Console.WriteLine(e.Message);
            }
        }
    }
}
