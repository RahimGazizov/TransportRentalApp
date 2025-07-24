using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Подготовка
{
    class Scooter : Transport
    {
        public Scooter(string model, double price, bool isRented = false)
            : base(model, price, isRented) { }

        public override void Rent(int hours)
        {
            if (!IsRented)
            {
                double totol = PricePerHour * hours * 0.9;

                Console.WriteLine($"Цена за аренду скутера: {totol}");
                IsRented = true;
            }
            else
            {
                Console.WriteLine("Скутер занят");
            }
        }
    }
}
