using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_12
{
    interface Buses
    {
        void BusAge(ref int yearOfExp, out int busAge);
    }

    class Bus:Buses
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
        public static int BusMilage(string distanceOnRoute, string yearOfExpl)
        {
            return Convert.ToInt32(distanceOnRoute) * 365 *(2020 - Convert.ToInt32(yearOfExpl));
        }
        static Bus() // Статический конструктор, который запускается при создании объекта класса Bus
        {
            Console.WriteLine("Welcome to the bus station!\n");
        }


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
}