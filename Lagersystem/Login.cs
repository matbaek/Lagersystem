using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnection;

namespace Lagersystem
{
    class Login
    {
        public bool CheckLogin()
        {
            Connection connection = new Connection();
            Console.Clear();
            Console.Write("Brugernavn: ");
            string username = Console.ReadLine();
            Console.Write("Kodeord: ");
            string password = ReadPassword();

            return connection.CheckLoginInformation(username, password);
        }

        public string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Enter)
            {
                if (key.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += key.KeyChar;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    password = password.Substring(0, password.Length - 1);
                    int pos = Console.CursorLeft;
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                }
                key = Console.ReadKey(true);
            }
            return password;
        }
    }
}
