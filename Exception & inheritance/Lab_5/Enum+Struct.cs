using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    enum CarColor
    {
        White = 1,
        Black,
        Green,
        DarkBlue
    }

    struct Driver
    {
        string name;
        int age;
        string hometown;

        Driver(string name, int age, string hometown)
        {
            this.name = name;
            this.age = age;
            this.hometown = hometown;
        }

        void PrintDriver()
        {
            Console.WriteLine("Информация о водителе:");
            Console.WriteLine($"Имя:{name} \nВозраст:{age} \nГород: {hometown}");
        }
    }
}
