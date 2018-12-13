using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagersystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.Run();
        }

        void Run()
        {
            bool checkingLogin = true;
            while (checkingLogin)
            {
                Login login = new Login();
                if (login.CheckLogin())
                {
                    checkingLogin = false;
                    Menu menu = new Menu();
                    menu.ShowMenu();
                }
                else
                {
                    Console.WriteLine("\nLogin fejlede, prøv igen!");
                    Console.ReadLine();
                }
            }
        }
    }
}
