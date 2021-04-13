using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_14
{
    class TransmissionTypeException : ArgumentException
    {
        public string Value { get; }
        public TransmissionTypeException() : base() { }
        public TransmissionTypeException(string val, string message) : base(message)
        {
            Value = val;
        }

        public override string ToString()
        {
            return Message;
        }
    }
    class IntTypeException : ArgumentException
    {
        public int Value { get; }
        public IntTypeException(int val, string message) : base(message)
        {
            Value = val;
        }
        public override string ToString()
        {
            return Message;
        }
    }

    class BrandTypeException : ArgumentException
    {
        public BrandTypeException(string message) : base(message)
        {

        }
        public override string ToString()
        {
            return Message;
        }
    }

    interface IWIN
    {
        long WIN { get; set; }
        void GenerateWIN();
    }

    [Serializable]
    public class Vehicle
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
    [Serializable]
    public abstract class Engine : Vehicle, IWIN
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
    [Serializable]
    public sealed class Car : Engine
    {
        public string carBrand { get; set; }
        [NonSerialized]
        private string transmissionType;
        public string TransmissionType
        {
            get
            {
                return transmissionType;
            }
            set
            {
                if (value == "АКПП" || value == "МКПП")
                    transmissionType = value;
                else
                    throw new TransmissionTypeException(value, "Такой коробки передач не существует");
            }
        }
        private int gasExpense;
        public int GasExpense
        {
            get
            {
                return gasExpense;
            }
            set
            {
                if (value < 1)
                    throw new IntTypeException(value, "Расход топлива не может быть меньше 1");
                else
                    gasExpense = value;
            }
        }

        public Car(string brand, string transmission, int gas, long power, long cost, string country) : base(power, country, cost)
        {
            if (brand == null)
                throw new BrandTypeException("Значение марки автомобиля не может быть пустым");
            else
                carBrand = brand;

            TransmissionType = transmission;
            GasExpense = gas;
        }
        public Car() { }

        public override void GenerateWIN()
        {
            Random rand = new Random();
            int ID = rand.Next(0, 100);
            base.GenerateWIN();
            WIN += ID;
        }
        public override int GetHashCode()
        {
            return carBrand.GetHashCode() + transmissionType.GetHashCode() + PowerOfEngine.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            Car car = (Car)obj;

            return (this.GetHashCode() == car.GetHashCode());
        }

        public override string ToString()
        {
            return
                $"Марка автомобиля: {this.carBrand}\n" +
                $"Трансмиссия: {this.transmissionType}\n" +
                $"Расход топлива: {this.gasExpense}\n" + base.ToString();

        }
    }
}
