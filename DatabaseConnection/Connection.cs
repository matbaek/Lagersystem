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

        public List<string> ShowCatalog()
        {
            List<string> productsList = new List<string>();
            string query = "SELECT ID, Category, Title, [Description], Genre, AuthorFirstName, AuthorLastName, Price FROM Product";
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
                            string category = reader["Category"].ToString();
                            string title = reader["Title"].ToString();
                            string description = reader["Description"].ToString();
                            string genre = reader["Genre"].ToString();
                            string authorFirstName = reader["AuthorFirstName"].ToString();
                            string authorLastName = reader["AuthorLastName"].ToString();
                            string price = reader["Price"].ToString();

                            productsList.Add(id + "|" + category + "|" + title + "|" + description + "|" + genre + "|" + authorFirstName + "|" + authorLastName + "|" + price);
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

        public bool AddProduct(string category, string title, string description, string genre, string author, float price)
        {
            bool added = false;
            string[] _author = author.Split(' ');
            string firstName = "";
            string lastName = "";
            if (author == "")
            {
                firstName = "Ukendt forfatter";
            }
            else
            {
                string[] authorSplit = author.Split(' ');
                for (int i = 1; i < authorSplit.Length; i++)
                {
                    lastName += authorSplit[i] + " ";
                }
            }
            string query = "INSERT INTO Product (Category, Title, Description, Genre, AuthorFirstName, AuthorLastName, Price) VALUES (@Category, @Title, @Description, @Genre, @AuthorFirstName, @AuthorLastName, @Price)";
            SqlConnection con = SqlConnection();
            using (SqlCommand command = SqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Genre", genre);
                    command.Parameters.AddWithValue("@AuthorFirstName", firstName);
                    command.Parameters.AddWithValue("@AuthorLastName", lastName);
                    command.Parameters.AddWithValue("@Price", price);

                    command.ExecuteNonQuery();
                    added = true;
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            return added;
        }

        public bool EditProduct(int id, string category, string title, string description, string genre, string author, float price)
        {
            bool added = false;
            string[] authorSplit = author.Split(' ');
            string lastName = "";
            for (int i = 1; i < authorSplit.Length; i++)
            {
                lastName += authorSplit[i] + " ";
            }
            string query = "UPDATE Product SET Category = @Category, Title = @Title, Description = @Description, Genre = @Genre, AuthorFirstName = @AuthorFirstName, AuthorLastName = @AuthorLastName, Price = @Price " +
                "WHERE ID = @ID";
            SqlConnection con = SqlConnection();
            using (SqlCommand command = SqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Genre", genre);
                    command.Parameters.AddWithValue("@AuthorFirstName", authorSplit[0]);
                    command.Parameters.AddWithValue("@AuthorLastName", lastName);
                    command.Parameters.AddWithValue("@Price", price);

                    command.ExecuteNonQuery();
                    added = true;
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            return added;
        }

        public bool DeleteProduct(int id)
        {
            bool added = false;
            string query = "DELETE FROM Product WHERE ID = " + id;
            SqlConnection con = SqlConnection();
            using (SqlCommand command = SqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    added = true;
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            return added;
        }
    }
}
