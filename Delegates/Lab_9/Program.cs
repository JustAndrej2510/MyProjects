using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_9
{
    public delegate void Software();
    public delegate void Report();
  
    class App
    {
        public event Software Upgrade;
        public event Report Work;
        private float version;
        public float Version
        {
            get
            {
                return version;
            }
            set
            {
                if (value > 0)
                {
                    version = value;
                }
                else
                    version = 0.1f;
            }
        }
        public App(float version)
        {
            this.Version = version;
        }

        public void Start()
        {
            Console.WriteLine("Приложение работает");
            Work?.Invoke();
        }
       
        public void PrintVersion()
        {
            Console.WriteLine("Текущая версия приложения: " + version);
        }
        public void UpgradeVer()
        {
            Version += 0.1f;
            Console.WriteLine("Приложение обновлено");
            Upgrade?.Invoke();
            
        }
       
    }

    class StringWork
    {
       
        public string DeleteSymbol(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ',' || str[i] == '.' || str[i] == '!' || str[i] == '?' || str[i] == ':' || str[i] == ';')
                    str = str.Remove(i, 1);
            }
            Console.WriteLine("Знаки препинания удалены");
            Console.WriteLine(str);
            return str;
        }

        public string UpperSymbol(string str)
        {
            char[] charStr = str.ToCharArray();
            charStr[0] = Char.ToUpper(charStr[0]);
            str = new string(charStr);
            Console.WriteLine("Первая буква поднята в верхний регистр");
            Console.WriteLine(str);
            return str;
            
        }

        public string  DeleteExtraSpace(string str)
        {
            for(int i = 0; i<str.Length; i++)
            {
                if(str[i]==' ' && str[i+1]==' ')
                    str = str.Remove(i, 1);
            }
            Console.WriteLine("Лишние пробелы удалены");
            Console.WriteLine(str);
            return str;
        }

        public string CodeString(string str)
        {
            char temp = ' ';
            char[] charStr = str.ToCharArray();
            for(int i=0; i<charStr.Length; i+=2)
            {
                if (i >= charStr.Length - 1)
                    break;
                temp = charStr[i];
                charStr[i] = charStr[i + 1];
                charStr[i + 1] = temp;
                
            }
            str = new string(charStr);
            Console.WriteLine("Строка зашифрована");
            Console.WriteLine(str);
            return str;
        }

        public string UncodeString(string str)
        {
            char temp = ' ';
            char[] charStr = str.ToCharArray();
            for (int i = 0; i < charStr.Length; i += 2)
            {
                if (i >= charStr.Length - 1)
                    break;
                temp = charStr[i];
                charStr[i] = charStr[i + 1];
                charStr[i + 1] = temp;

            }
            str = new string(charStr);
            Console.WriteLine("Строка расшифрована");
            Console.WriteLine(str);
            return str;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            //1
            App app1 = new App(1.0f);
            app1.Work += app1.PrintVersion;
            app1.Upgrade += app1.PrintVersion;
            app1.Start();
            app1.UpgradeVer();

            //2
            StringWork s = new StringWork();
            string str = "hello  wor,ld!";
            Console.WriteLine(str);
            Func<string,string> fc = s.UpperSymbol;
            str = Check(str, fc);
            fc += s.DeleteSymbol;
            str = Check(str, fc);
            fc += s.DeleteExtraSpace;
            str = Check(str, fc);
            fc += s.CodeString;
            str = Check(str, fc);
            fc += s.UncodeString;
            str = Check(str, fc);

        }
        public static string Check(string str, Func<string, string> fc)
        {
            Console.WriteLine("---------------------------------------------------------");
            return fc?.Invoke(str);
        }
    }
}
