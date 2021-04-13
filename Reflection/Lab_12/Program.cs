using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_12
{
    static class Reflector
    {
        public static Type GetTypeByName(string name)
        {
            return Type.GetType("Lab_12." + name);
        }

        public static void ClassInfo(string name)
        {
            StreamWriter file = new StreamWriter(name + ".txt");
            Type currentClass = GetTypeByName(name);
            file.WriteLine("Тип объекта: " + currentClass);
            //Конструкторы
            file.WriteLine("Публичные конструкторы: ");
            bool publicConstr = false;
            foreach(var c in currentClass.GetConstructors())
            {
                if(c.IsPublic)
                {
                    publicConstr = true;
                    file.Write(c.MemberType + " " + name + " (");
                    ParameterInfo[] parameters = c.GetParameters();
                    for (int i = 0; i<parameters.Length; i++)
                    {
                        if(i == parameters.Length-1)
                            file.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name);
                        else
                            file.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name + ", ");
                    }
                    file.Write(")\n");
                }
            }
            if(publicConstr == false)
            {
                file.WriteLine("отсутствуют");
            }

            //Методы
            file.WriteLine("\nМетоды: ");
            foreach (var c in currentClass.GetMethods())
            {
                if (c.IsPublic)
                {
                    file.Write(c.ReturnType.Name + " " + c.Name + " " + " (");
                    ParameterInfo[] parameters = c.GetParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (i == parameters.Length - 1)
                            file.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name);
                        else
                            file.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name + ", ");
                    }
                    file.Write(")\n");
                }
            }

            //Свойства
            file.WriteLine("\nСвойства: ");
            foreach (var c in currentClass.GetProperties())
            {
                file.WriteLine(c.PropertyType.Name + " " + c.Name);   
            }

            //Поля
            file.WriteLine("\nПоля: ");
            foreach (var c in currentClass.GetFields())
            {
                file.WriteLine(c.FieldType.Name + " " + c.Name);
            }

            //Интерфейсы
            file.WriteLine("\nИнтерфейсы: ");
            foreach (var c in currentClass.GetInterfaces())
            {
                file.WriteLine(c);
            }
            file.Close();
        }

        public static void GetMethodByParam(string className, string param)
        {
            Type temp = GetTypeByName(className);
            Console.WriteLine($"Методы класса {temp.Name} с параметром ({param})");
            foreach (var x in temp.GetMethods())
            {
                foreach (var y in x.GetParameters())
                {
                    if (y.ParameterType.Name == param)
                    {
                        Console.WriteLine(x);
                    }
                }
            }
            Console.WriteLine();
        }

        public static void MethodFormFile(string className, string methodName)
        {
            try
            {
                StreamReader file = new StreamReader("Parameters.txt");
                List<string> param = new List<string>();
                while (file.EndOfStream != true)
                {
                    param.Add(file.ReadLine());
                }
                //Assembly asm = Assembly.Load("Lab_12." + className);
                Type currentClass = GetTypeByName(className);
                object obj = Activator.CreateInstance(currentClass);
                MethodInfo classMethod = currentClass.GetMethod(methodName);
                var result = classMethod.Invoke(obj, param.ToArray());
                Console.WriteLine(result);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        public static Object Create(string name)
        {
            Type currentClass = GetTypeByName(name);
            //Type[] constr = null;
            object obj = Activator.CreateInstance(currentClass);
            // ConstructorInfo classConstructor = currentClass.GetConstructor(constr);
            return obj;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            //1
            Reflector.ClassInfo("Bus");
            Reflector.GetMethodByParam("Bus", "String");
            Reflector.MethodFormFile("Bus", "BusMilage");
            //2
            Object bus = Reflector.Create("Bus");
        }
    }
}
