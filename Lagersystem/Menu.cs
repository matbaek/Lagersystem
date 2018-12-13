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
            do
            {
                menuStructure();
                Console.Write("Indtast menu punkt: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        menuRunning = false;
                        break;
                    case "1":
                        ShowCatalog();
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Menupunktet findes ikke, prøv igen.");
                        Console.ReadLine();
                        break;
                }
            } while (menuRunning);
        }

        public void menuStructure()
        {
            List<string> menu = new List<string>()
            {
                "Vis katalog"
            };

            Console.Clear();
            Console.Write("╔");
            Console.Write(new String('═', Console.WindowWidth - 3));
            Console.WriteLine("╗");
            for (int i = 0; i < menu.Count; i++)
            {
                MenuStructureSetup(i + 1 + ". " + menu[i]);
            }

            MenuStructureSetup("0. Exit");
            Console.Write("╚");
            Console.Write(new String('═', Console.WindowWidth - 3));
            Console.WriteLine("╝");
        }
        
        public void MenuStructureSetup(string menuPoint)
        {
            string text = "║  " + menuPoint;
            StringBuilder spaces = new StringBuilder();
            for (int i = 0; i < (Console.WindowWidth - text.Length - 2); i++)
            {
                spaces.Append(" ");
            }
            Console.WriteLine(text + spaces + "║");
        }

        //Menu metoder
        //private void MetodeNavn()
        //{
        //    Console.Write("Text: ");
        //    string inputText = Console.ReadLine();
        //    Console.Clear();
        //    //Kald til metoden
        //}
        private void ShowCatalog()
        {
            Console.Clear();
            Product product = new Product();
            List<Product> products = product.StringToObject();
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine(products[i].ToString());
            }
        }
    }
}
