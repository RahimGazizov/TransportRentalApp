using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Подготовка
{
    abstract class Transport
    {
        private string model;
        private double pricePerHour;
        private bool isRented;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }
        public double PricePerHour
        {
            get { return pricePerHour; }
            set { pricePerHour = value; }
        }
        public bool IsRented
        {
            get { return isRented; }
            set { isRented = value; }
        }
        public Transport(string model, double pricePerHour, bool isRented = false)
        {
            Model = model;
            PricePerHour = pricePerHour;
            IsRented = isRented;
        }
        public abstract void Rent(int hours);
        public void Return()
        {
            isRented = false;
        }
    }
}
