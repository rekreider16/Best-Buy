using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;

namespace BestBuy
{
    public class ProductRepository
    {
        public ProductRepository()
        {
#if DEBUG
            string jsonText = File.ReadAllText("appsettings.development.json");
#else
            string jsonText = File.ReadAllText("appsettings.development.json");
#endif
            string connStr = JObject.Parse(jsonText)["ConnectionStrings"]["DefaultConnection"].ToString();

            this.connStr = connStr;
        }

        private string connStr;

        public List<Product> GetAllProducts()
        {
            MySqlConnection conn = new MySqlConnection(connStr);

            List<Product> products = new List<Product>();

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ProductID, Name, Price, CategoryID FROM products;";

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = (int)dataReader["ProductID"],
                        Name = dataReader["Name"].ToString(),
                        Price = (decimal)dataReader["Price"],
                        CategoryId = (int)dataReader["CategoryID"]
                    };

                    products.Add(product);
                    Console.WriteLine(product.ProductId + "....." + product.Name + "....." + product.Price);
                }

                return products;
            }
        }

        public Product GetProductsByName(string Name)
        {
            MySqlConnection conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ProductID, Name, Price, CategoryID " +
                                  "FROM products " +
                                  "WHERE Name @xyz " +
                                  "ORDER BY ProductID";
                cmd.Parameters.AddWithValue("xyz", $"%{Name}%");

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    Product product = new Product()
                    {
                        Name = dataReader["Name"].ToString(),
                        ProductId = (int)dataReader["ProductID"],
                        Price = (decimal)dataReader["Price"],
                        CategoryId = (int)dataReader["CateogryID"]
                    };

                    return product;
                }
                return null;

            }
        }

        public Product GetProduct(int id)
        {
            MySqlConnection conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ProductID, Name, Price, CategoryID " +
                                  "FROM products " +
                                  "WHERE ProductID=" + id;

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    Product product = new Product()
                    {
                        Name = dataReader["Name"].ToString(),
                        ProductId = (int)dataReader["ProductID"],
                        Price = (decimal)dataReader["Price"],
                        CategoryId = (int)dataReader["CategoryID"]
                    };

                    return product;
                }
                return null;

            }
        }

        public void AddProduct(string name, decimal price, int catID)
        {
            var conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();

                cmd.CommandText = "INSERT INTO products (Name, Price, CategoryID) Values (@n , @p , @cID);";
                cmd.Parameters.AddWithValue("n", name);
                cmd.Parameters.AddWithValue("p", price);
                cmd.Parameters.AddWithValue("cID", catID);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Product prod)
        {
            var conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE products SET Name = @n, Price = @p, CategoryId = @cID " +
                                  "WHERE ProductId = @pID;";
                cmd.Parameters.AddWithValue("n", prod.Name);
                cmd.Parameters.AddWithValue("p", prod.Price);
                cmd.Parameters.AddWithValue("cID", prod.CategoryId);
                cmd.Parameters.AddWithValue("pID", prod.ProductId);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int id)
        {
            MySqlConnection conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM Products " +
                                  "WHERE ProductID=" + id;

                cmd.ExecuteNonQuery();
            }
        }

    }
}
