using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BestBuy
{
    class Program
    {

        public static void ShowProductsTable()
        {
            var product = new ProductRepository();
            List<Product> Products = product.GetAllProducts();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("What do you want to do with this table?");
            Console.WriteLine("1) Create entry\n2) Update entry\n3) Delete entry");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void CreateTableEntry()
        {
            var product = new ProductRepository();

            Console.WriteLine("What is the name of the product?");
            string userResName = Console.ReadLine();

            Console.WriteLine("What is the price of the product?");
            string userResPrice = Console.ReadLine();
            decimal price = decimal.Parse(userResPrice);

            Console.WriteLine("What category is this item?");
            Console.WriteLine("1) Computers\n2) Appliances\n3) Cell Phones\n4) Headpones/Earbuds\n5) TVs and Accessories\n6) Printer's and Accessories");
            string userResCatID = Console.ReadLine();
            int catID = int.Parse(userResCatID);

            var prod1 = new Product()
            {
                Name = userResName,
                Price = price,
                CategoryId = catID
            };

            Console.WriteLine("Product added.");
            product.AddProduct(userResName, price, catID);
            Console.ReadLine();

        }

        public static void UpdateTableEntry()
        {
            var product = new ProductRepository();

            Console.WriteLine("What is the name of the product to update?");
            string userResName = Console.ReadLine();

            Console.WriteLine("What is the new price of the product?");
            string userResPrice = Console.ReadLine();
            decimal price = decimal.Parse(userResPrice);

            Console.WriteLine("What category is this item?");
            Console.WriteLine("1) Computers\n2) Appliances\n3) Cell Phones\n4) Headpones/Earbuds\n5) TVs and Accessories\n6) Printer's and Accessories");
            string userResCatID = Console.ReadLine();
            int catID = int.Parse(userResCatID);

            Console.WriteLine("What is the Product ID of the item?");
            string userProdID = Console.ReadLine();
            int prodID = int.Parse(userProdID);

            var prod1 = new Product()
            {
                Name = userResName,
                Price = price,
                CategoryId = catID,
                ProductId = prodID
            };

            Console.WriteLine("Product updated.");
            product.UpdateProduct(prod1);
            Console.ReadLine();
        }

        public static void DeleteEntry()
        {
            var product = new ProductRepository();

            Console.WriteLine("What is the Product ID of the item?");
            string userProdID = Console.ReadLine();
            int prodID = int.Parse(userProdID);

            var prod1 = new Product()
            {
                ProductId = prodID
            };

            Console.WriteLine("Product deleted.");
            product.DeleteProduct(prodID);
            Console.ReadLine();
        }

        public static void Main(string[] args)
        {

            Console.WriteLine("Which table would you like to see?");
            Console.WriteLine("(Type the word)");
            Console.WriteLine("1) Products\n2) Sales\n3) Exit");

            string userResponse = Console.ReadLine();

            bool exitClause = true;
            bool updateTable = true;

            while (exitClause)
            {

                switch (userResponse.ToUpper())
                {
                    case "PRODUCTS":
                        Console.Clear();
                        exitClause = false;
                        break;

                    case "SALES":
                        Console.Clear();
                        Console.WriteLine("Sales table.");
                        userResponse = Console.ReadLine();
                        break;

                    default:
                        exitClause = false;
                        break;
                }

            }

            while (updateTable)
            {
                ShowProductsTable();
                userResponse = Console.ReadLine();
                switch (userResponse.ToUpper())
                {
                    case "CREATE ENTRY":
                        CreateTableEntry();
                        break;

                    case "UPDATE ENTRY":
                        UpdateTableEntry();
                        break;

                    case "DELETE ENTRY":
                        DeleteEntry();
                        break;

                    default:
                        updateTable = false;
                        break;
                }
            }

            Console.ReadLine();
        }
    }
}