using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductLibrary;
using DatabaseConnection;

namespace Lagersystem
{
    public class Menu
    {
        public void ShowMenu()
        {
            bool menuRunning = true;
            while(menuRunning)
            {
                Console.Clear();
                MenuStructureTop();
                MenuStructure("1. Vis katalog");
                MenuStructure("2. Tilføj produkt");
                MenuStructure("0. Exit");
                MenuStructureBottom();
                Console.Write("Indtast menu punkt: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        menuRunning = false;
                        break;
                    case "1":
                        bool showCatalog = true;
                        while (showCatalog)
                        {
                            ShowCatalog();
                            MenuStructureTop();
                            MenuStructure("Vælg produkt (F.eks. 3)");
                            MenuStructure("0. Exit");
                            MenuStructureBottom();
                            Console.Write("Indtast: ");
                            string productPick = Console.ReadLine();
                            if(productPick != "0")
                            {
                                ShowProduct(int.Parse(productPick));
                                MenuStructureTop();
                                MenuStructure("1. Rediger produkt");
                                MenuStructure("2. Slet produkt");
                                MenuStructure("0. Exit");
                                MenuStructureBottom();
                                Console.Write("Indtast: ");
                                string productChoice = Console.ReadLine();
                                if(productChoice == "1")
                                {
                                    Console.Write("Indtast title: ");
                                    string editTitle = Console.ReadLine();
                                    Console.Write("Indtast forfatter: ");
                                    string editAuthor = Console.ReadLine();
                                    Console.Write("Indtast kategori: ");
                                    string editCategory = Console.ReadLine();
                                    Console.Write("Indtast genre: ");
                                    string editGenre = Console.ReadLine();
                                    Console.Write("Indtast beskrivelse: ");
                                    string editDescription = Console.ReadLine();
                                    Console.Write("Indtast pris: ");
                                    string editPrice = Console.ReadLine();
                                    float editPriceFloat = 0;
                                    if(editPrice != "")
                                    {
                                        editPriceFloat = float.Parse(editPrice);
                                    }

                                    EditProduct(int.Parse(productPick), editCategory, editTitle, editDescription, editGenre, editAuthor, editPriceFloat);
                                }
                                else if(productChoice == "2")
                                {
                                    MenuStructureTop();
                                    MenuStructure("Er du sikker på at du vil slette produktet? (J/N)");
                                    MenuStructureBottom();
                                    Console.Write("Indtast: ");
                                    string deleteProduct = Console.ReadLine();
                                    if(deleteProduct == "J")
                                    {
                                        DeleteProduct(int.Parse(productPick));
                                    }
                                }
                            } 
                            else
                            {
                                showCatalog = false;
                            }
                        }
                        break;
                    case "2":
                        Console.Write("Indtast title: ");
                        string title = Console.ReadLine();
                        Console.Write("Indtast forfatter: ");
                        string author = Console.ReadLine();
                        Console.Write("Indtast kategori: ");
                        string category = Console.ReadLine();
                        Console.Write("Indtast genre: ");
                        string genre = Console.ReadLine();
                        Console.Write("Indtast beskrivelse: ");
                        string description = Console.ReadLine();
                        Console.Write("Indtast pris: ");
                        float price = float.Parse(Console.ReadLine());

                        AddProduct(category, title, description, genre, author, price);
                        break;
                    default:
                        Console.WriteLine("Menupunktet findes ikke, prøv igen.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void MenuStructureTop()
        {
            Console.Write("╔");
            Console.Write(new String('═', Console.WindowWidth - 3));
            Console.WriteLine("╗");
        }
        
        private void MenuStructure(string menuPoint)
        {
            string text = "║  " + menuPoint;
            StringBuilder spaces = new StringBuilder();
            for (int i = 0; i < (Console.WindowWidth - text.Length - 2); i++)
            {
                spaces.Append(" ");
            }
            Console.WriteLine(text + spaces + "║");
        }

        private void MenuStructureBottom() { 
            Console.Write("╚");
            Console.Write(new String('═', Console.WindowWidth - 3));
            Console.WriteLine("╝");
        }

        private void ShowCatalog()
        {
            Console.Clear();
            Product product = new Product();
            List<Product> products = product.StringToProduct();
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine(products[i].ToString());
            }
        }

        private void ShowProduct(int id)
        {
            Console.Clear();
            Product product = new Product();

            product.ShowProduct(id);
        }

        private void AddProduct(string category, string title, string description, string genre, string author, float price)
        {
            Console.Clear();
            Product product = new Product();

            product.AddProduct(category, title, description, genre, author, price);
        }

        private void EditProduct(int id, string category, string title, string description, string genre, string author, float price)
        {
            Console.Clear();
            Product product = new Product();

            product.EditProduct(id, category, title, description, genre, author, price);
        }

        private void DeleteProduct(int id)
        {
            Console.Clear();
            Product product = new Product();

            product.DeleteProduct(id);
        }
    }
}
