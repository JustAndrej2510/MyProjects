using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{

    partial class Train : Carriage
    {
        public void AllRouteCost()
        {
            Console.WriteLine("Стоимость всего маршрута: " + this.CostOfObject * this.NumberOfTickets);
        }

        public override string ToString()
        {
            return $"Тип объекта: {this.GetType()}\n" +
                $"Страна производитель:{this.countryOfOrigin}\n" +
                $"Компания: {this.companyType}\n" +
                $"Тип вагона: {this.CarriageClass}\n" +
                $"Количество вагонов:{this.AmountOfCarriages}\n" +
                $"Маршрут: {this.route}\n" +
                $"Количество билетов: {this.numberOfTickets}\n" +
                $"Стоимость билета: {this.CostOfObject}\n";
        }
    }

   
}
