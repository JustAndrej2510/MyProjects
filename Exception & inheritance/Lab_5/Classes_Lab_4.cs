using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class Print
    {
        public virtual void IamPrinting(Engine obj)
        {
            Console.WriteLine(obj.ToString());
        }
    }
    interface IWIN
    {
        long WIN { get; set; }
        void GenerateWIN();
    }
    class Vehicle
    {
        public string countryOfOrigin { get; set; }
        private long costOfObject;
        public long CostOfObject
        {
            get
            {
                return costOfObject;
            }
            set
            {
                if (value < 1)
                    costOfObject = 0;
                else
                    costOfObject = value;
            }
        }

        public Vehicle(long cost, string country)
        {
            countryOfOrigin = country;
            costOfObject = cost;
        }
        public Vehicle() { }

        public override string ToString()
        {
            return $"Страна производитель:{this.countryOfOrigin}\n" +
                $"Стоимость:{this.CostOfObject}\n" +
                $"Тип объекта: {this.GetType()}\n";

        }


    }
    class Engine : Vehicle, IWIN
    {
        private long powerOfEngine;
        public long PowerOfEngine
        {
            get
            {
                return powerOfEngine;
            }
            set
            {
                if (value < 1)
                    powerOfEngine = 1;
                else
                    powerOfEngine = value;
            }
        }
        public long WIN { get; set; }
        public virtual void GenerateWIN()
        {
            WIN = (powerOfEngine * 101) / 2;
        }

        public Engine(long power, string country, long cost) : base(cost, country)
        {
            powerOfEngine = power;
        }
        public Engine() { }
        public override string ToString()
        {
            return $"Мощность двигателя: {this.PowerOfEngine}\n" + base.ToString();


        }

    }
    class Express : Vehicle
    {
        public string companyType { get; set; }
        public Express(string company, string country, int cost) : base(cost, country)
        {
            companyType = company;
        }
        public Express() { }
        public override string ToString()
        {
            return $"Тип объекта: {this.GetType()}\n" +
                $"Страна производитель:{this.countryOfOrigin}\n" +
                $"Компания: {this.companyType}\n" +
                $"Стоимость:{this.CostOfObject}\n";

        }
    }

    class Carriage : Express
    {
        private string carriageClass;
        public string CarriageClass
        {
            get
            {
                return carriageClass;
            }
            set
            {
                if (value == "Бизнесс" || value == "Эконом")
                    carriageClass = value;
                else
                    carriageClass = "No information";
            }
        }

        private int amountOfCarriages;
        public int AmountOfCarriages
        {
            get
            {
                return amountOfCarriages;
            }
            set
            {
                if (value < 0)
                    amountOfCarriages = 0;
                else
                    amountOfCarriages = value;
            }
        }
        public Carriage(string company, string country, int cost, int amount, string type) : base(company, country, cost)
        {
            amountOfCarriages = amount;
            carriageClass = type;
        }
        public Carriage() { }
        public override string ToString()
        {
            return $"Тип объекта: {this.GetType()}\n" +
                $"Страна производитель:{this.countryOfOrigin}\n" +
                $"Компания: {this.companyType}\n" +
                $"Тип вагона: {this.carriageClass}\n " +
                $"Количество вагонов:{this.amountOfCarriages}\n " +
                $"Стоимость:{this.CostOfObject}\n";

        }
    }

    partial class Train : Carriage
    {
        public string route { get; set; }
        public const int i = 2;
        public int[] arr = new int[i];
        private int numberOfTickets;
        public int NumberOfTickets
        {
            get
            {
                return numberOfTickets;
            }
            set
            {
                if (value < 0)
                    numberOfTickets = 0;
                else
                    numberOfTickets = value;
            }
        }
        public Train(string company, string country, int cost, int amount, string type, int tickets, string routeName) : base(company, country, cost, amount, type)
        {
            numberOfTickets = tickets;
            route = routeName;
        }
        public Train() { }


    }
}
