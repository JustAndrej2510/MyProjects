using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class Logger
    {
        private string login;
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                if (!value.Equals("Andrej"))
                {
                    throw new LoggerException("Неверный логин");
                }
                else
                    login = value;
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (!value.Equals("Andrej123"))
                {
                    throw new LoggerException("Неверный пароль");
                }
                else
                    password = value;
            }
        }
        DateTime date = DateTime.Now;

        public Logger (string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }
        public Logger() { }
        public void ConsoleLogger(string INFO)
        {
            Console.WriteLine(Convert.ToString(INFO));
        }
        public void FileLogger(string INFO)
        {
            FileStream fstream = null;
            try
            {
                fstream = new FileStream(@"D:\file.txt", FileMode.OpenOrCreate);
                StreamWriter file = new StreamWriter(fstream, Encoding.Default);
                file.WriteLine(INFO);
                Console.WriteLine("Данные записаны в файл");
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Вызвано исключение:" + ex.Message);
            }
            finally
            {
                if (fstream != null)
                    fstream.Close();
            }
        }

    }
}
