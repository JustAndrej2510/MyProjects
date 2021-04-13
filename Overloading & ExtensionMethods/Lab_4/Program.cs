using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    public static class StatisticOperation // Cтатический класс, содержащий обычные и расширенные методы для нужного класса 
    {
        public static int Sum(List<int> currentList) // Нахождение суммы элементов списка
        {
            int sum = 0;
            foreach(var i in currentList)
            {
                sum += i;
            }
            return sum;
        }

        public static int Difference(List<int> currentList) // Разница между max и min элементами списка 
        {
            int diff = 0;
            int max = currentList[0];
            int min = currentList[0];
            for(int i = 0;i<currentList.Count;i++)
            {
                if (currentList[i] > max)
                    max = currentList[i];
                else if (currentList[i] < min)
                    min = currentList[i];
            }
            diff = max - min;
            return diff;
            
        }
        // Методы расширения
        public static string ExtensionString(this string str, string surname ) // Метод расширения для string. Объеденяет две строки через _
        {
            string newstr;
            int pos = -1;
            for(int i=0;i<str.Length;i++)
            {
                if (str[i] == '_')
                {
                    pos = i;
                    newstr = str.Insert(i, surname);
                    return newstr;
                    
                }
            }
            if(pos == -1)
            {
                str = String.Concat(str, '_');
            }
            newstr = String.Concat(str, surname);

            return newstr;

        }
        public static string ExtensionSubstring(this string str, int ind) // Для string. Обрезает строку до заданного индекса
        {
            string newstr;
            newstr = str.Substring(ind-1);

            return newstr;

        }

        public static int ExtensionList(this List<int> list) // Метод расширения для списка. Находит сумму элементов списка
        { 
            int sum = 0;
            foreach(var i in list)
            {
                sum += i;
            }
            return sum;
        }
    }

    public class List
    {
        public List<int> number { get; set; } // Односвязный список из коллекции
        public List() // Конструктор для списка
        {
            this.number = new List<int>() {};
        }

        public class Owner
        {
            public int _ID { get; set; }
            public string _name { get; set; }
            public string _organization { get; set; }
            
        }
        public Owner owner;
        public List(int id, string name, string organization) // Конструктор для Owner
        {
            this.owner = new Owner() { _ID = id, _name = name, _organization = organization};
        }

        public class Date                                      
        {
            private int day;
            public int Day
            {
                get
                {
                    return day;
                }
                set
                {
                    if (day < 1 && day > 2020)
                        day = -1;
                    else
                        day = value;

                }
            }
            private int month;
            public int Month
            {
                get
                {
                    return month;
                }
                set
                {
                    if (month < 1 && month > 2020)
                        month = -1;
                    else
                        month = value;
                }
            }
            private int year;
            public int Year
            {
                get
                {
                    return year;
                }
                set
                {
                    if (year < 1 && year > 2020)
                        year = -1;
                    else
                        year = value;
                }
            }

            
        }
        public Date date;
        public List(int day, int month, int year) // Конструктор для Date
        {
            this.date = new Date() { Day = day, Month = month,Year = year };
        }

        public static List operator +(List list1, List list2) // Объединение двух списков
        {
            List newList = new List();
            for(int i=0;i<list1.number.Count;i++)
            {
                newList.number.Add(list1.number[i]);
            }
            for (int i = 0; i < list2.number.Count; i++)
            {
                newList.number.Add(list2.number[i]);
            }
            return newList;
        }

        public static List operator <(List list1, List list2)     // Добаление списка 2 к списку 1 
        {
            list1.number.AddRange(list2.number);
            return list1;
        }
        public static List operator >(List list1, List list2)     // Добавление списка 1 к списку 2
        {
            list2.number.AddRange(list1.number);
            return list2;
        }
        public static List operator !(List list1) // Реверсия элементов списка
        {
            int count = list1.number.Count;
            int temp;
            for (int i = 0, j = count - 1; i < j;i++,j--)
            {
                temp = list1.number[j];
                list1.number[j] = list1.number[i];
                list1.number[i] = temp;
            }
            return list1;
        }
        public static bool operator !=(List list1, List list2)
        {
            return !list1.Equals(list2);
            
        }
        public static bool operator ==(List list1, List list2)
        {
            return list1.Equals(list2);
        }
        public override bool Equals(object list)                //переопределение
        {
            List temp = (List)list;
            if (list == null || list.GetType() != this.GetType() || temp.number.Count != this.number.Count)
            {
                return false;
            }
            for (int i = 0; i < this.number.Count; i++)
            {
                if (temp.number[i] != this.number[i])
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            Random rnd = new Random();
            int hash;
            hash = rnd.Next() / 10;
            return hash;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List myList = new List();
            myList.number.Add(20);
            myList.number.Add(10);
            myList.number.Add(25);
            Console.Write("Первый список: ");
            foreach (var i in myList.number)
                Console.Write(i + " ");
            Console.WriteLine();
            StatisticOperation.Sum(myList.number);
            Console.WriteLine($"Разность между максимальным и минимальным элементами списка: {StatisticOperation.Difference(myList.number)}");
            int sum = myList.number.ExtensionList();
            Console.WriteLine($"Сумма элементов списка: {sum}");

            List myList1 = new List(110, "Elon", "Tesla");
            myList1.owner._ID = 1100;
            myList1.owner._name = "Elon";
            myList1.owner._organization = "Tesla";
            myList1.owner._name = myList1.owner._name.ExtensionString("Musk");
            
            Console.WriteLine($"Добавление фамилии к имени владельца: {myList1.owner._name}");
            myList1.owner._organization = myList1.owner._organization.ExtensionSubstring(3);
            Console.WriteLine($"Удаление до определенного индекса: {myList1.owner._organization}");
            List myList3 = new List();
            myList3.number.Add(1);
            myList3.number.Add(2);
            myList3.number.Add(3);
            Console.Write("Второй список: ");
            foreach (var i in myList3.number)
                Console.Write(i + " ");
            Console.WriteLine();
            List myList4 = new List();
            myList4.number.Add(1);
            myList4.number.Add(4);
            myList4.number.Add(9);
            Console.Write("Третий список: ");
            foreach (var i in myList4.number)
                Console.Write(i + " ");
            Console.WriteLine();
            List mainList1 = myList3 + myList4;
            Console.Write("Объединение двух списков: ");
            foreach (var i in mainList1.number)
                Console.Write(i + " ");
            Console.WriteLine();
            mainList1 = null;
            mainList1 = myList3 > myList4;
            foreach (var i in mainList1.number)
                Console.Write(i + " ");
            Console.WriteLine();
            List mainList2 = new List();
            mainList2 = !mainList1;
            foreach (var i in mainList2.number)
                Console.Write(i + " ");
            Console.WriteLine();
            bool temp = myList != mainList1;
            Console.WriteLine(temp);

        }
    }
}


