using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Подготовка
{
    public class TransportApp
    {
        private List<Transport> transports;
        private RentalService rentalService;
        private string dataFilePath = @"C:\Users\Gaziz\OneDrive\Desktop\классы(ООП)\data.json";
        public void Run()
        {
            transports = new List<Transport>();
            rentalService = new RentalService(transports);
            rentalService.LoadFile(dataFilePath);
            bool exit = true;
            while (exit)
            {
                Console.Write("Выберите действие\n1-Добавить транспорт\n2-Арендовать машину" +
                    "\n3-Список свободых транспортов\n4-Вернуть транспорт\n5-Скачать файл\n6-Прсмотр транспорта по категориям" +
                    "\n7-Фильтрация по цене\n8-Удалить транспорт\n9-Изменить транспорт\n10-Выход\n>" + " ");
                try
                {
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1:
                            Console.Write("Выберите тип транспорта\n1-Автомобиль\n2-Байк\n3-Скутер\n>" + " ");
                            int choose = Convert.ToInt32(Console.ReadLine());
                            string name = "";
                            double price;
                            if (choose == 1)
                            {
                                while (true)
                                {
                                    Console.Write("Введите модель машины\n>" + " ");
                                    name = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(name))
                                    {
                                        var modelCar = rentalService.Exists(name);
                                        if (modelCar != null)
                                        {
                                            Console.WriteLine("Такая модель машины уже есть");
                                            break;
                                        }

                                        Console.Write("Введите цену за аренду машины за час\n>" + " ");
                                        price = Convert.ToDouble(Console.ReadLine());
                                        if (price > 0)
                                        {
                                            Car car = new Car(name, price);
                                            transports.Add(car);
                                            Console.WriteLine("Машина добавлена");
                                            rentalService.DownloadFile(dataFilePath);
                                            break;
                                        }
                                        else
                                            Console.WriteLine("Введите цену за аренду > 0");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Вы не вели модель машины");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                }
                            }
                            else if (choose == 2)
                            {
                                while (true)
                                {
                                    Console.Write("Введите модель байка\n>" + " ");
                                    name = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(name))
                                    {
                                        var modelBike = rentalService.Exists(name);
                                        if (modelBike != null)
                                        {
                                            Console.WriteLine("Такая модель байка уже есть");
                                            break;
                                        }
                                        Console.Write("Введите цену за аренду байка за час\n>" + " ");
                                        price = Convert.ToDouble(Console.ReadLine());
                                        if (price > 0)
                                        {
                                            Bike bike = new Bike(name, price);
                                            transports.Add(bike);
                                            Console.WriteLine("Байк добавлен");
                                            rentalService.DownloadFile(dataFilePath);
                                            break;
                                        }
                                        else
                                            Console.WriteLine("Введите цену за аренду > 0");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Вы не вели модель байка");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                }
                            }
                            else
                            {
                                while (true)
                                {
                                    Console.Write("Введите модель скутер\n>" + " ");
                                    name = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(name))
                                    {
                                        var modelScooter = rentalService.Exists(name);
                                        if (modelScooter != null)
                                        {
                                            Console.WriteLine("Такая модель скутера уже есть");
                                            break;
                                        }
                                        Console.Write("Введите цену за аренду скутер за час\n>" + " ");
                                        price = Convert.ToDouble(Console.ReadLine());
                                        if (price > 0)
                                        {
                                            Scooter scooter = new Scooter(name, price);
                                            transports.Add(scooter);
                                            Console.WriteLine("Скутер добавлен");
                                            rentalService.DownloadFile(dataFilePath);
                                            break;
                                        }
                                        else
                                            Console.WriteLine("Введите цену за аренду > 0");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Вы не вели модель скутера");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                }
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 2:

                            while (true)
                            {
                                Console.Write("Введите модель\n>" + " ");
                                string model = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(model))
                                {
                                    Console.Write("На сколько часов арендовать\n>" + " ");
                                    int hours = Convert.ToInt32(Console.ReadLine());
                                    if (hours > 0)
                                    {
                                        rentalService.RentTransport(model, hours);
                                        break;
                                    }
                                    else
                                        Console.WriteLine("Введите количество часов больше 0");
                                }
                                else
                                {
                                    Console.WriteLine("Нужно вести модель");
                                }
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 3:
                            var transportFree = rentalService.ShowAvailable();
                            if (transportFree.Count > 0)
                            {
                                var cars = transportFree.Where(x => x is Car).Select(x => x.Model);
                                var bikes = transportFree.Where(x => x is Bike).Select(x => x.Model);
                                var scooters = transportFree.Where(x => x is Scooter).Select(x => x.Model);
                                if (cars.Any())
                                    Console.WriteLine($"Свободные машины:" + string.Join(", ", cars));
                                if (bikes.Any())
                                    Console.WriteLine($"Свободные байки:" + string.Join(", ", bikes));
                                if (scooters.Any())
                                    Console.WriteLine($"Свободные скутеры:" + string.Join(", ", scooters));
                            }
                            else
                            {
                                Console.WriteLine("Список пуст");
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 4:
                            while (true)
                            {
                                Console.Write("Введите модель\n>" + " ");
                                string model = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(model))
                                {
                                    rentalService.ReturnTransport(model);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Нужно вести модель");
                                }
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 5:
                            string path = @"C:\Users\Gaziz\OneDrive\Desktop";
                            string fileName = "data.json";
                            string fullWay = Path.Combine(path, fileName);
                            rentalService.DownloadFile(fullWay);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 6:
                            while (true)
                            {
                                Console.Write("Выберите тип транспорта который хотите посмотреть\n1-Атомобиль\n2-Байки\n3-Скутеры\n>" + " ");
                                string typeName = "";
                                Type type = null;
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        type = typeof(Car);
                                        typeName = "машины";
                                        break;
                                    case "2":
                                        type = typeof(Bike);
                                        typeName = "байки";
                                        break;
                                    case "3":
                                        type = typeof(Scooter);
                                        typeName = "скутеры";
                                        break;
                                    default:
                                        Console.WriteLine("Вы вели не коретное число");
                                        Console.ReadKey();
                                        Console.Clear();
                                        continue;
                                }
                                var models = rentalService.GetAvailableTransportModelsByType(type);
                                if (models.Any())
                                {
                                    Console.WriteLine($"Сободные {typeName}:" + string.Join(", ", models));
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine($"Все {typeName} заняты");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 7:
                            var sortPrice = rentalService.PriceList();
                            Dictionary<bool, string> pairs = new Dictionary<bool, string>();
                            pairs.Add(false, "Свободен");
                            pairs.Add(true, "Занят");
                            while (true)
                            {
                                Console.Write("Выберите тип транспорта который хотите посмотреть\n1-Атомобиль\n2-Байки\n3-Скутеры\n>" + " ");
                                int choise = Convert.ToInt32(Console.ReadLine());
                                if (choise <= 3)
                                {
                                    if (sortPrice.Count > 0)
                                    {
                                        foreach (var prices in sortPrice)
                                        {
                                            if (choise == 1)
                                            {
                                                if (prices is Car)
                                                    Console.WriteLine($"Модель машины: {prices.Model}\nЦена за аренду в час: {prices.PricePerHour}" +
                                                        $"\nСтатус: {pairs[prices.IsRented]}");
                                            }
                                            if (choise == 2)
                                            {
                                                if (prices is Bike)
                                                    Console.WriteLine($"Модель байка: {prices.Model}\nЦена за аренду в час: {prices.PricePerHour}" +
                                                        $"\nСтатус: {pairs[prices.IsRented]}");
                                            }
                                            if (choise == 3)
                                            {
                                                if (prices is Scooter)
                                                    Console.WriteLine($"Модель скутера: {prices.Model}\nЦена за аренду в час: {prices.PricePerHour}" +
                                                        $"\nСтатус: {pairs[prices.IsRented]}");
                                            }

                                        }
                                        break;
                                    }

                                    else
                                        Console.WriteLine("Список пуст");
                                }
                                else
                                {
                                    Console.Write("Введите число от 1 до 3\n>" + " ");
                                    Console.ReadKey();
                                }
                            }

                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 8:
                            while (true)
                            {
                                Console.Write("Введите модель транспорта который хотите удалить\n>" + " ");
                                string model = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(model))
                                {
                                    rentalService.RemoveTransport(model);
                                    rentalService.DownloadFile(dataFilePath);
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Вы не вели модель транспорта");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }

                            break;
                        case 9:
                            while (true)
                            {
                                Console.Write("Введите модель транспорта который хотите изменить\n>" + " ");
                                string model = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(model))
                                {
                                    rentalService.EditTransport(model);
                                    rentalService.DownloadFile(dataFilePath);
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Вы не вели модель транспорта");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }
                            break;
                        case 10:
                            exit = false;
                            break;
                        default:
                            Console.WriteLine("Введите число от 1 до 10");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите число");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
} 