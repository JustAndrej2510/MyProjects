using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lab_14
{

    class Program
    {
        static void Main(string[] args)
        {
            //1
            Car ford = new Car("Ford", "МКПП", 10, 109, 5550, "Germany");
            Console.WriteLine("Объект для сериализации: ");
            Console.WriteLine(ford.ToString());
            Console.WriteLine("///////Binary///////");
            BinaryFormatter binSer = new BinaryFormatter();
            SoapFormatter soapSer = new SoapFormatter();
            XmlSerializer xmlSer = new XmlSerializer(typeof(Car));
            DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(Car));
            //Бинарная сериализация
            using (Stream bf = new FileStream("binary.bin", FileMode.OpenOrCreate))
            {
                binSer.Serialize(bf, ford);
            }
            // Десериализация
            using (Stream bf = File.OpenRead("binary.bin"))
            {
                Car deserFord = (Car)binSer.Deserialize(bf);
                Console.WriteLine("Десериализованный объект: \n" + deserFord);
            }
            Console.WriteLine("///////Soap///////");
            // Soap Serialize
            using (Stream sf = new FileStream("soap.dat", FileMode.OpenOrCreate))
            {
                soapSer.Serialize(sf, ford);
            }
            // Soap Deserialize
            using (Stream sf = File.OpenRead("soap.dat"))
            {
                Car deserFord = (Car)soapSer.Deserialize(sf);
                Console.WriteLine("Десериализованный объект: \n" + deserFord);
            }
            Console.WriteLine("///////XML///////");
            // Xml Serialize
            using (Stream sf = new FileStream("xml.xml", FileMode.OpenOrCreate))
            {
                xmlSer.Serialize(sf, ford);
            }
            // Xml Deserialize
            using (Stream sf = File.OpenRead("xml.xml"))
            {
                Car deserFord = (Car)xmlSer.Deserialize(sf);
                Console.WriteLine("Десериализованный объект: \n" + deserFord);
            }
            Console.WriteLine("///////XML///////");
            // Json Serialize
            using (Stream sf = new FileStream("json.json", FileMode.OpenOrCreate))
            {
                jsonSer.WriteObject(sf, ford);
            }
            // Json Deserialize
            using (Stream sf = File.OpenRead("json.json"))
            {
                Car deserFord = (Car)jsonSer.ReadObject(sf);
                Console.WriteLine("Десериализованный объект: \n" + deserFord);
            }

            //2
            List<Car> carList = new List<Car>();
            carList.Add(new Car("AUDI", "АКПП", 15, 210, 15000, "Germany"));
            carList.Add(new Car("BMW", "АКПП", 13, 190, 12000, "Germany"));
            carList.Add(new Car("LADA", "МКПП", 9, 70, 1000, "Russia"));
            carList.Add(ford);
            Console.WriteLine("\nОбъекты для сериализации: ");
            foreach(var i in carList)
            {
                Console.WriteLine(i);
            }
            DataContractJsonSerializer jsonList = new DataContractJsonSerializer(typeof(List<Car>));
            // Json Serialize
            using (Stream sf = new FileStream("ListJson.json", FileMode.OpenOrCreate))
            {
                jsonList.WriteObject(sf, carList);
            }
            // Json Deserialize
            using (Stream sf = File.OpenRead("ListJson.json"))
            {
                List<Car> deserList = (List<Car>)jsonList.ReadObject(sf);
                Console.WriteLine("Десериализованные объекты: \n");
                foreach (var i in deserList)
                {
                    Console.WriteLine(i);
                }
                
            }
            //3
            XmlDocument newXml = new XmlDocument();
            newXml.Load("cars.xml");


            XmlNodeList nodelist1 = newXml.DocumentElement.SelectNodes("*");
            XmlNode node = newXml.DocumentElement.SelectSingleNode("car[@name='Ford']");

            foreach (XmlNode x in nodelist1)
            {
                Console.WriteLine(x.OuterXml);
            }
            Console.WriteLine();
            if (node != null)
                Console.WriteLine(node.OuterXml);
            Console.WriteLine();

            //4
            XDocument xdoc = new XDocument(new XElement("phones",
                         new XElement("phone",
                         new XAttribute("name", "iPhone 6"),
                         new XElement("company", "Apple"),
                         new XElement("price", "1000")),
                     new XElement("phone",
                         new XAttribute("name", "Samsung Galaxy S5"),
                         new XElement("company", "Samsung"),
                         new XElement("price", "1100")),
                     new XElement("phone",
                        new XAttribute("name","Xiaomi Redmi Note 5"),
                        new XElement("company", "Xiaomi"),
                        new XElement("price", "600"))));
            xdoc.Save("phones.xml");

            XDocument cars = XDocument.Load("cars.xml");
            Console.WriteLine("\nДанные из файла:\n");
            foreach (XElement i in cars.Element("cars").Elements("car"))
            {
                XAttribute nameAttribute = i.Attribute("name");
                XElement country = i.Element("country");
                XElement power = i.Element("power");
                XElement expance = i.Element("expance");

                if (nameAttribute != null && country != null && power != null && expance != null)
                {
                    Console.WriteLine($"Компания: {nameAttribute.Value}");
                    Console.WriteLine($"Страна: {country.Value}");
                    Console.WriteLine($"Мощность: {power.Value}");
                    Console.WriteLine($"Расход топлива: {expance.Value}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Немецкие машины:\n ");
            var car = cars.Descendants("car").Where(p => (string)p.Element("country") == "Germany");
            foreach(var i in car)
                Console.WriteLine(i);

            Console.WriteLine("\nСортировка по расходу топлива:\n ");
            var expanceOrder = cars.Descendants("car").OrderBy(e=>int.Parse(e.Element("expance").Value));
            foreach (var i in expanceOrder)
                Console.WriteLine(i);

        }
    }
}
