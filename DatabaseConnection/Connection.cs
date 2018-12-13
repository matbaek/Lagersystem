using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseConnection
{
    public class Connection
    {
        private static string connectionString =
            "Data Source = EALSQL1.Eal.Local;" +
            "Database = C_DB10_2018;" +
            "User ID = C_STUDENT10;" +
            "Password = C_OPENDB10;";

        private SqlConnection SqlConnection()
        {
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }

        private SqlCommand SqlCommand(string query, SqlConnection con)
        {
            SqlCommand command = new SqlCommand(query, con);
            return command;
        }

        public bool CheckLoginInformation(string username, string password)
        {
            string query = "SELECT ID, Username, Password FROM [User] WHERE Username = '" + username + "' AND Password = '" + password + "'";
            SqlConnection con = SqlConnection();
            using (SqlCommand command = SqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    if (command.ExecuteReader().HasRows) return true;
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            return false;
        }

        public List<string> ShowCatelog()
        {
            List<string> productsList = new List<string>();
            string query = "SELECT Product.ID, CategoryID, Title, [Description], Genre, ArthorFirstName, ArthorLastName, Price, Category.Type FROM Product JOIN Category ON Product.CategoryID = Category.ID";
            SqlConnection con = SqlConnection();
            using (SqlCommand command = SqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string id = reader["ID"].ToString();
                            string type = reader["Type"].ToString();
                            string title = reader["Title"].ToString();
                            string description = reader["Description"].ToString();
                            string genre = reader["Genre"].ToString();
                            string arthorFirstName = reader["ArthorFirstName"].ToString();
                            string arthorLastName = reader["ArthorLastName"].ToString();
                            string price = reader["Price"].ToString();

                            productsList.Add(id + "|" + type + "|" + title + "|" + description + "|" + genre + "|" + arthorFirstName + "|" + arthorLastName + "|" + price);
                        }
                    }

                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            return productsList;
        } 
    }
}
