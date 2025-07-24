using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Подготовка
{
    class Bike : Transport
    {

        public Bike(string model, double price, bool isRented = false)
            : base(model, price, isRented) { }
        public override void Rent(int hours)
        {
            if (!IsRented)
            {
                PricePerHour *= hours;
                Console.WriteLine($"Цена за аренду байка: {PricePerHour}");
                IsRented = true;
            }
            else
            {
                Console.WriteLine("Байк занят");
            }
        }
    }
}
