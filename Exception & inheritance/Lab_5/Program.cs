using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    
    sealed class Car : Engine
    {
        public string carBrand { get; set; }
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

    

    class TransportAgency
    {
        public List<Car> carList { get; set; }
        public TransportAgency()
        {
            carList = new List<Car>();
        }
        public void Print()
        {
            Console.WriteLine("\nСписок автомобилей агенства:\n");
            foreach (var i in carList)
            {
                Console.WriteLine(i);
            }
        }
        

    }

    class TransportController : IComparer<Car>
    {
        public long GeneralCost(TransportAgency TA)
        {
            long sum = 0;
            foreach(var i in TA.carList)
            {
                sum += i.CostOfObject;
            }
            return sum;
        }

        public int Compare(Car list1, Car list2)
        {
            if(list1.GasExpense > list2.GasExpense)
            {
                return 1;
            }
            else if(list1.GasExpense < list2.GasExpense)
            {
                return -1;
            }

            return 0;
        }

        public void Power(TransportAgency TA, int[] power)
        {
            int count = 0;
            for(int i = 0; i< TA.carList.Count;i++)
            {
                if(TA.carList[i].PowerOfEngine >= power[0] && TA.carList[i].PowerOfEngine <= power[1])
                {
                    Console.WriteLine(TA.carList[i]);
                    count++;
                }
            }
            if (count == 0)
                Console.WriteLine("В данном диапазоне ничего не найдено");
        }
    }

    

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                try
                {
                    Car carTry1 = new Car("Mercedes", "АКrП", 10, 125, 12000, "Germany");
                }
                catch (TransmissionTypeException ex)
                {
                    Console.WriteLine("Было сгенерировано исключение: " + ex.Message + "\n");
                    Console.WriteLine("Метод: " + ex.TargetSite + "\n");
                    Console.WriteLine("Место: " + ex.StackTrace + "\n");
                }
                finally
                {
                    Console.WriteLine("Трансмиссия проверена\n");
                    Console.WriteLine("-----------------------------------------\n");
                }

                try
                {
                    Car carTry2 = new Car("Mercedes", "АКПП", -2, 200, 12000, "Germany");
                }
                catch (IntTypeException ex)
                {
                    Console.WriteLine("Было сгенерировано исключение: " + ex.Message + "\n");
                    Console.WriteLine("Метод: " + ex.TargetSite + "\n");
                    Console.WriteLine("Место: " + ex.StackTrace + "\n");
                }
                finally
                {
                    Console.WriteLine("Расход топлива проверен\n");
                    Console.WriteLine("-----------------------------------------\n");
                }

                try
                {
                    Car carTry2 = new Car(null, "АКПП", 3, 200, 12000, "Germany");
                }
                catch (BrandTypeException ex)
                {
                    Console.WriteLine("Было сгенерировано исключение: " + ex.Message + "\n");
                    Console.WriteLine("Метод: " + ex.TargetSite + "\n");
                    Console.WriteLine("Место: " + ex.StackTrace + "\n");
                }
                finally
                {
                    Console.WriteLine("Марка автомобиля проверена\n");
                    Console.WriteLine("-----------------------------------------\n");
                }

                try
                {
                    Train train = new Train();
                    train.arr[5] = 5;
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Было сгенерировано исключение: " + ex.Message + "\n");
                    Console.WriteLine("Метод: " + ex.TargetSite + "\n");
                    Console.WriteLine("Место: " + ex.StackTrace + "\n");
                }
                finally
                {
                    Console.WriteLine("Массив проверен\n");
                    Console.WriteLine("-----------------------------------------\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Было сгенерировано исключение: " + ex.Message + "\n");
                Console.WriteLine("Метод: " + ex.TargetSite + "\n");
                Console.WriteLine("Место: " + ex.StackTrace + "\n");
            }
            finally
            {
                Console.WriteLine("\nПроверка закончена\n");
                Console.WriteLine("-----------------------------------------\n");

            }
           
            Logger person = new Logger();

            try
            {
                person.Login = "Andrejе";
                person.Password = "Andrej13";
                string Info = "Время: " + Convert.ToString(DateTime.Now) + "\nЛогин: " + person.Login + "\nПароль: " + person.Password + "\nАвторизация пройдена успешно";
                person.ConsoleLogger(Info);
                person.FileLogger(Info);
            }
            catch (LoggerException ex)
            {
                string Info = "Время: " + Convert.ToString(DateTime.Now) + "\nИсключение: " + ex.Message + "\nМетод: " + ex.TargetSite + "\nМесто: " + ex.StackTrace;
                person.ConsoleLogger(Info);
                person.FileLogger(Info);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("\nПроверка авторизации завершена\n");
            }



            ///////////////////////////////////////////
            //                  Lab_5
            ///////////////////////////////////////////

            //Car car = new Car("Mercedes", "АКПП", 10, 125, 12000, "Germany");
            //Car car2 = new Car("AUDI", "МКПП", 15, 300, 20000, "Germany");
            //car2.GenerateWIN();
            //TransportController control = new TransportController();
            //TransportAgency TA1 = new TransportAgency();
            //TA1.carList.Add(car);
            //TA1.carList.Add(car2);
            //TA1.carList.Add(new Car("BMW", "МКПП", 22, 201, 21100, "Germany"));
            //TA1.carList.Sort(control);
            //TA1.Print();
            //Console.WriteLine($"Общая стоимость всех автомобилей: {control.GeneralCost(TA1)}");
            //int[] powerArray = new int[2];
            //Console.WriteLine("Введите диапазон мощности автомобиля: ");
            //Console.WriteLine("Первая точка диапазона: ");
            //powerArray[0] = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Вторая точка диапазона: ");
            //powerArray[1] = Convert.ToInt32(Console.ReadLine());
            //if (powerArray[0] > powerArray[1])
            //    throw new Exception("Первая точка диапазона не может быть больше второй");

            //control.Power(TA1, powerArray);





            ///////////////////////////////////////////
            //                  Lab_4
            ///////////////////////////////////////////
            //bool eq = car.Equals(car2);
            //Console.WriteLine("\n" + eq + "\n");

            //Train train = new Train("Belarussian railway", "Belarus", 100, 10, "Бизнесс", 200, "Брест-Витебск");
            //Console.WriteLine(train.ToString());
            //train.AllRouteCost();
            //Carriage newCarr = new Carriage("RR", "Russia", 1000, 25, "Бизнесс");
            //Console.WriteLine("\n" + newCarr.ToString());
            //Train tr = newCarr as Train;
            //if(tr == null)
            //    Console.WriteLine("\nПреобразование не удалось\n");
            //else
            //    Console.WriteLine(tr.ToString());

            //newCarr = train as Carriage;
            //if (newCarr == null)
            //    Console.WriteLine("\nПреобразование не удалось\n");
            //else
            //{
            //    Console.WriteLine("Результат преобразования: ");
            //    Console.WriteLine(newCarr.ToString());
            //}

            //Engine[] veh = new Engine[3];
            //Engine eng = car2 as Car;
            //veh[0] = eng;
            //veh[1] = car;
            //veh[2] = car2;


            //Print print = new Print();
            //for(int i = 0; i<veh.Length;i++)
            //{
            //    print.IamPrinting(veh[i]);
            //}



        }
    }
}
