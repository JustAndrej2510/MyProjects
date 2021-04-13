using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_11
{
    class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
    }
    class Team
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
    class Bus
    {
        public string driverFullName { get; set; }
        private int busNumber;
        public int BusNumber
        {
            set
            {
                if (value < 0)
                    Console.WriteLine("Bus number can't be less than 0");
                else
                    busNumber = value;
            }
            get
            {
                return busNumber;
            }
        }
        private int numberOfPath;
        public int NumberOfPath
        {
            set
            {
                if (value < 0)
                    Console.WriteLine("Path number can't be less than 0");
                else
                    numberOfPath = value;
            }
            get
            {
                return numberOfPath;
            }
        }
        public string busBrand { get; set; }
        public int yearOfExp;
        public int YearOfExp
        {
            set
            {
                if (value > 1980 && value < 2021)
                    yearOfExp = value;
                else
                    Console.WriteLine("Bus can't be more than 40 years old");
            }

        }
        public readonly double ID;
        public const int constVar = 2;
        private static int counter = 0;

        public Bus() // Конструктор без параметров (по-умолчанию)
        {
            driverFullName = "No information";
            busNumber = 100;
            NumberOfPath = 100;
            busBrand = "No information";
            yearOfExp = 2000;
            ID = (busNumber * NumberOfPath) / constVar;
            counter++;
        }

        public Bus(string fullName, int number, int path, string brand, int year) // Конструктор с параметрами
        {
            driverFullName = fullName;
            busNumber = number;
            NumberOfPath = path;
            busBrand = brand;
            yearOfExp = year;
            ID = (busNumber * NumberOfPath) / constVar;
            counter++;
        }

        public Bus(string fullName)
        {
            driverFullName = fullName;
            busNumber = 26;
            NumberOfPath = 26;
            busBrand = "MAZ";
            yearOfExp = 1994;
            ID = (busNumber * NumberOfPath) / constVar;
            counter++;
        }

        public void BusAge(ref int yearOfExp, out int busAge) // Метод для рассчета возраста автобуса
        {
            busAge = DateTime.Now.Year - yearOfExp;

        }

        static Bus() // Статический метод, который запускается при создании объекта класса Bus
        {
            Console.WriteLine("Welcome to the bus station!\n");
        }

        //private Bus() { } - закрытый конструктор. В данном случае не нужно использовать, т.к. он запрещает создания экземпляров класса по-умолчанию

        public static void CounterOfBuses(out int amountBuses) // Метод, который считает количество созданных объектов класса
        {
            amountBuses = counter;
            Console.WriteLine($"Amount of buses: {counter} \n");
        }

        public void Info() // Метод, который выводит информацию об объекте
        {
            Console.WriteLine("Information about bus:");
            Console.WriteLine($"Driver name: {driverFullName}\n" +
                              $"Number: {busNumber}\n" +
                              $"Number of the path: {numberOfPath}\n" +
                              $"Brand: {busBrand}\n" +
                              $"Year of exploitation: {yearOfExp}\n" +
                              $"ID: {ID}\n");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //1
            string[] month = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            var newMonth1 = month.Where(n => n.Length > 5); //длина строки больше 5
            Console.WriteLine("Длина строки больше 5 элементов: ");
            foreach(var i in newMonth1)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////");

            var newMonth2 = month.Where(m => (m == "January" || m == "December" || m == "February" || m == "June" || m == "July" || m == "August"));
            Console.WriteLine("Зимние и летние месяцы: ");
            foreach (var i in newMonth2)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////");

            Console.WriteLine("Сортировка по алфавиту: ");
            var newMonth3 = month.OrderBy(s => s[0]);//сортировка по алфавиту
            foreach (var i in newMonth3)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////");

            Console.WriteLine("Есть u и не менее 4 символов: ");
            var newMonth4 = month.Where(u => u.Contains("u")).Where(s=>s.Length>4);
            foreach (var i in newMonth4)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////");

            //2
            List<Bus> listOfBus = new List<Bus>()
            {
                { new Bus("Albert", 1945, 10, "Mercedes", 2002)},
                { new Bus("Volodya", 2280, 17, "Tesla", 2005)},
                { new Bus("Elon", 7777, 192, "Tesla", 2020)},
                { new Bus("Dimon", 1276, 29, "Mercedes", 2010)},
                { new Bus("Ilya", 1234, 25, "AUDI", 2018)},
                { new Bus("Tolyan", 1111, 10, "AUDI", 2019)},
                { new Bus("Petrovich", 9999, 25, "Mercedes", 2009)},
                { new Bus("Miron", 1222, 192, "Mercedes", 2000)},
                { new Bus("Lexa", 5555, 190, "Lexus", 2010)},
            };
            foreach(var i in listOfBus)
            {
                i.Info();
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////");

            //3
            Console.WriteLine("Список автобусов по 25-ому маршруту: ");
            var busList1 = listOfBus.Where(N=>N.NumberOfPath==25);
            foreach( var i in busList1)
            {
                Console.WriteLine($"Driver name: {i.driverFullName}\n" +
                              $"Number: {i.BusNumber}\n" +
                              $"Number of the path: {i.NumberOfPath}\n" +
                              $"Brand: {i.busBrand}\n" +
                              $"Year of exploitation: {i.yearOfExp}\n");
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////");

            Console.WriteLine("Список автобусов эксплуатирующихся больше 17 лет: ");
            var busList2 = listOfBus.Where(y => y.yearOfExp < 2003);
            foreach (var i in busList2)
            {
                Console.WriteLine($"Driver name: {i.driverFullName}\n" +
                              $"Number: {i.BusNumber}\n" +
                              $"Number of the path: {i.NumberOfPath}\n" +
                              $"Brand: {i.busBrand}\n" +
                              $"Year of exploitation: {i.yearOfExp}\n");
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////");

            Console.WriteLine("Автобус, который больше всего эксплуатируются: ");
            var busList3 = listOfBus.OrderBy(y => y.yearOfExp);
            foreach (var i in busList3)
            {
                Console.WriteLine($"Driver name: {i.driverFullName}\n" +
                              $"Number: {i.BusNumber}\n" +
                              $"Number of the path: {i.NumberOfPath}\n" +
                              $"Brand: {i.busBrand}\n" +
                              $"Year of exploitation: {i.yearOfExp}\n");
                break;
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////");

            Console.WriteLine("Два автобуса, которые меньше всего эксплуатируются: ");
            var busList4 = listOfBus.OrderBy(y => y.yearOfExp);
            int countBuses = 0;
            foreach (var i in busList4)
            {
                if (countBuses == 2) break;
                Console.WriteLine($"Driver name: {i.driverFullName}\n" +
                              $"Number: {i.BusNumber}\n" +
                              $"Number of the path: {i.NumberOfPath}\n" +
                              $"Brand: {i.busBrand}\n" +
                              $"Year of exploitation: {i.yearOfExp}\n");
                countBuses++;
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////");

            Console.WriteLine("Сортировка автобусов по номеру: ");
            var busList5 = listOfBus.OrderBy(y => y.BusNumber);
            foreach (var i in busList5)
            {
                Console.WriteLine(
                     $"Number: {i.BusNumber}\n" +
                     $"Driver name: {i.driverFullName}\n" +
                     $"Number of the path: {i.NumberOfPath}\n" +
                     $"Brand: {i.busBrand}\n" +
                     $"Year of exploitation: {i.yearOfExp}\n");
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////");

            //4
            var selfQ = listOfBus.OrderBy(n => n.yearOfExp).Where(b => b.yearOfExp < 2019).Take(4).GroupBy(k=>k.busBrand).ToList();
            foreach(var m in selfQ)
            {
                Console.WriteLine("Марка: " + m.Key + " " + "Количество в списке: " + m.Count() + "\n");
                foreach( var i in m )
                {
                    Console.WriteLine(
                     $"Number: {i.BusNumber}\n" +
                     $"Driver name: {i.driverFullName}\n" +
                     $"Number of the path: {i.NumberOfPath}\n" +
                     $"Brand: {i.busBrand}\n" +
                     $"Year of exploitation: {i.yearOfExp}\n");
                }
            }
            Console.WriteLine("/////////////////////////////////////////////////////////////////////");
            //5
            List<Team> teams = new List<Team>()
            {
                new Team { Name = "Арсенал", Country ="Англия" },
                new Team { Name = "Тоттенхэм", Country ="Англия" },
                new Team { Name = "Бавария", Country ="Германия" }
            };
            List<Player> players = new List<Player>()
            {
                new Player {Name="Озил", Team="Арсенал"},
                new Player {Name="Ляказетт", Team="Арсенал"},
                new Player {Name="Артета", Team="Арсенал"},
                new Player {Name="Кейн", Team="Тоттенхэм"},
                new Player {Name="Льорис", Team="Тоттенхэм"},
                new Player {Name="Алонсо", Team="Бавария"},
                new Player {Name="Лам", Team="Бавария"},
                new Player {Name="Роббен", Team="Бавария"}
            };

            var resultJoin = teams.Join(players, t => t.Name, p => p.Team, (t, p) => new { Name = p.Name, Team = t.Name, t.Country });

            foreach (var item in resultJoin)
                Console.WriteLine($"{item.Name} - {item.Team} ({item.Country})");
        }
    }
}
