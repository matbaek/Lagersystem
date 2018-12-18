using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnection;

namespace ProductLibrary
{
    public class Product
    {
        private List<Product> products = new List<Product>();
        private int id;
        private string category;
        private string title;
        private string description;
        private string genre;
        private string author;
        private float price;

        public int ID
        {
            get { return id; }
            private set { this.id = value; }
        }
        public string Category
        {
            get { return category; }
            private set { this.category = value; }
        }
        public string Title
        {
            get { return title; }
            private set { this.title = value; }
        }
        public string Description
        {
            get { return description; }
            private set { this.description = value; }
        }
        public string Genre
        {
            get { return genre; }
            private set { this.genre = value; }
        }
        public string Author
        {
            get { return author; }
            private set { this.author = value; }
        }
        public float Price
        {
            get { return price; }
            private set { this.price = value; }
        }

        public Product(int id, string category, string title, string description, string genre, string author, float price)
        {
            this.id = id;
            this.category = category;
            this.title = title;
            this.description = description;
            this.genre = genre;
            this.author = author;
            this.price = price;
        }

        public Product() { }

        public List<Product> StringToProduct()
        {
            Connection connnection = new Connection();
            List<string> products = connnection.ShowCatalog();

            for (int i = 0; i < products.Count; i++)
            {
                string[] splitList = products[i].Split('|');
                Product product = new Product(int.Parse(splitList[0]), splitList[1], splitList[2], splitList[3], splitList[4], splitList[5] + " " + splitList[6], float.Parse(splitList[7]));
                this.products.Add(product);
            }
            return this.products;
        }

        public void ShowProduct(int id)
        {
            StringToProduct();
            Product product = new Product();
            for (int i = 0; i < this.products.Count; i++)
            {
                if (this.products[i].id == id)
                {
                    product = this.products[i];
                }
            }
            
            Console.WriteLine("Title: " + product.title);
            Console.WriteLine("af " + product.author);
            Console.WriteLine("Kategori: " + product.category);
            Console.WriteLine("Genre: " + product.genre + Environment.NewLine);
            Console.WriteLine(product.description + Environment.NewLine);
            Console.WriteLine("Pris: " + product.price + "kr");

        }

        public void AddProduct(string category, string title, string description, string genre, string author, float price)
        {
            Connection connnection = new Connection();

            bool checkIfProductIsAdded = connnection.AddProduct(category, title, description, genre, author, price);
            if (checkIfProductIsAdded == true)
            {
                Console.WriteLine("Produktet er blevet tilføjet!");
            }
            else
            {
                Console.WriteLine("Produktet blev ikke tilføjet, prøv igen!");
            }
            Console.ReadLine();
        }

        public void EditProduct(int id, string category, string title, string description, string genre, string author, float price)
        {
            Connection connnection = new Connection();
            StringToProduct();
            Product product = new Product();
            for (int i = 0; i < this.products.Count; i++)
            {
                if (this.products[i].id == id)
                {
                    product = this.products[i];
                }
            }
            if (category == "") { category = product.category; }
            if (title == "") { title = product.title; }
            if (description == "") { description = product.description; }
            if (genre == "") { genre = product.genre; }
            if (author == "") { author = product.author; }
            if (price == 0) { price = product.price; }

            bool checkIfProductHasBeenEdit = connnection.EditProduct(id, category, title, description, genre, author, price);
            if (checkIfProductHasBeenEdit == true)
            {
                Console.WriteLine("Produktet er blevet opdateret!");
            }
            else
            {
                Console.WriteLine("Produktet blev ikke opdateret, prøv igen!");
            }
            Console.ReadLine();
        }

        public void DeleteProduct(int id)
        {
            Connection connnection = new Connection();

            bool checkIfProductIsDeleted = connnection.DeleteProduct(id);
            if (checkIfProductIsDeleted == true)
            {
                Console.WriteLine("Produktet er blevet slettet!");
            }
            else
            {
                Console.WriteLine("Produktet blev ikke slettet, prøv igen!");
            }
            Console.ReadLine();
        }

        public override string ToString()
        {
            string firstLineFirstPart = this.id + ". " + this.title + " - af " + this.author;
            string firstLineSpaces = new String(' ', Console.BufferWidth - firstLineFirstPart.Length - this.price.ToString().Length - 3);
            string firstLine = firstLineFirstPart + firstLineSpaces + this.price + "kr";
            string secondLine = "";
            if(this.description.Length > 100)
            {
                secondLine = this.description.Substring(0, 60);
            } else {
                secondLine = this.description;
            }

            return firstLine + Environment.NewLine + secondLine + Environment.NewLine;
        }
    }
}
