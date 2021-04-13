using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    interface IGeneric<T>
    {
        void Add(T person);
        void Delete(T person);
        void Print();
    }
    class Person
    {
        public string name { get; set; }
        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value < 1 && value > 120)
                    throw new IntTypeException("Возраст не может быть меньше 1 и больше 120");
                else
                    age = value;
            }
        }
        public string town { get; set; }
        public Person(string name,int age,string town)
        {
            this.name = name;
            this.Age = age;
            this.town = town;
        }
        public Person() { }
    }
    class CommonClass<T> : IGeneric<T> where T : Person
    {
        public List<T> personList;
        public CommonClass()
        {
            this.personList = new List<T>();
        }
        public void Add(T person)
        {
            personList.Add(person);
            Console.WriteLine("Элемент добавлен в список");
        }

        public void Delete(T person)
        {
            if (personList.Remove(person))
                Console.WriteLine("Элемент удален из списка");
            else
                Console.WriteLine("Элемент не найден");
        }
        public void Print()
        {
            Console.WriteLine("Содержимое списка: ");
            foreach (T i in personList)
            {
                Console.WriteLine("Имя: "+i.name+" Возраст: " + i.Age + " Город: " + i.town);
            }
        }
        public void WriteToFile()
        {
            FileStream fstream = null;
            try
            {
                fstream = new FileStream(@"file.txt", FileMode.OpenOrCreate);
                StreamWriter file = new StreamWriter(fstream, Encoding.Default);
                foreach (T i in personList)
                {
                    file.WriteLine(i.name + " " + i.Age + " " + i.town);
                }
               
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

        public void ReadFromFile()
        {
            string str;
            FileStream file = null;
            try
            {
                file = new FileStream(@"file.txt", FileMode.Open);
                StreamReader sr = new StreamReader(file);
                Console.WriteLine("Данные из файла: ");
                while((str = sr.ReadLine())!=null)
                {
                    Console.WriteLine(str);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Вызвано исключение:" + ex.Message);
            }
            finally
            {
                if (file!= null)
                    file.Close();
            }
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person("Mikhail",55,"Odessa");
            Person person2 = new Person("Elesey", 18, "Barnaul");
            Person person3 = new Person("Ashot", 25, "Grozny");
            CommonClass<Person> cp = new CommonClass<Person>();
            cp.Add(person1);
            cp.Add(person2);
            cp.Add(person3);
            cp.Print();
            cp.WriteToFile();
            cp.ReadFromFile();
        }
    }
}
