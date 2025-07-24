using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Подготовка
{
    class Car : Transport
    {
        private bool requiresLicense;

        public bool RequiresLicense
        {
            get { return requiresLicense; }
            set { requiresLicense = value; }
        }
        public Car(string model, double price, bool isRented = false, bool requiresLicense = false)
            : base(model, price, isRented)
        {
            RequiresLicense = requiresLicense;
        }
        public override void Rent(int hours)
        {

            if (!IsRented)
            {
                double total = PricePerHour * hours + 100;
                Console.WriteLine($"Оплата за аренду машины: {Model} {total} сом");
                IsRented = true;
            }
            else
            {
                Console.WriteLine("Машина занята");
            }
        }
    }
}
