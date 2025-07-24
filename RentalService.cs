using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Подготовка
{
    class RentalService
    {
        List<Transport> transports = new List<Transport>();
        public RentalService(List<Transport> transports) { this.transports = transports; }

        public List<Transport> ShowAvailable()
        {
            return transports.Where(x => !x.IsRented).ToList();
        }
        public Transport Exists(string model)
        {
            return transports.FirstOrDefault(x => x.Model == model);
        }
        public void RentTransport(string model, int hours)
        {
            Transport transport = transports.FirstOrDefault(x => x.Model.ToLower().Trim() == model.ToLower().Trim());
            if (transport != null)
            {
                transport.Rent(hours);
            }
            else
            {
                Console.WriteLine("Транспорт не найден");
            }
        }
        public void DownloadFile(string filename)
        {
            string json = JsonConvert.SerializeObject(transports, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            });
            File.WriteAllText(filename, json);
        }
        public void LoadFile(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                transports.Clear();
                transports.AddRange(JsonConvert.DeserializeObject<List<Transport>>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                }));
            }
        }
        public void ReturnTransport(string model)
        {
            Transport transport = transports.FirstOrDefault(x => x.Model.ToLower().Trim() == model.ToLower().Trim());
            if (transport != null)
            {
                if (transport.IsRented)
                {
                    transport.Return();
                    Console.WriteLine($"Транспорт {transport.Model} освободился ");
                }
            }
            else
                Console.WriteLine("Транспорт не найден");
        }
        public List<Transport> PriceList()
        {
            return transports.OrderBy(x => x.PricePerHour).ToList();
        }
        public List<string> GetAvailableTransportModelsByType(Type type)
        {
            return transports.Where(x => !x.IsRented && x.GetType() == type).
                Select(x => x.Model).ToList();
        }
        public void RemoveTransport(string model)
        {
            Transport transport = transports.FirstOrDefault(x => x.Model.ToLower().Trim()
            == model.ToLower().Trim());
            if (transport != null)
            {
                if (!transport.IsRented)
                {
                    transports.Remove(transport);
                    Console.WriteLine($"Транспорт: {model} удален");
                }
                else
                {
                    Console.Write($"Транспорт сейчас в аренде\nВернуть его? 1-да, 2-нет\n>" + " ");
                    int choise = Convert.ToInt32(Console.ReadLine());
                    if (choise == 1)
                    {
                        transport.Return();
                        transports.Remove(transport);
                        Console.WriteLine($"Транспорт {model} возвращен и удален");
                    }
                    else
                    {
                        Console.WriteLine("Транспорт не удален");
                        return;
                    }
                }
            }
            else
                Console.WriteLine("Транспорт не найден");
        }
        public void EditTransport(string model)
        {
            Transport transport = transports.FirstOrDefault(x => x.Model.ToLower().Trim() == model.ToLower().Trim());
            if (transport != null)
            {

                while (true)
                {
                    Console.Write("Что вы хотите изменить? 1-Модель, 2-Цену\n>" + " ");
                    int choise = Convert.ToInt32(Console.ReadLine());
                    if (choise == 1)
                    {
                        Console.Write("Введите модель\n>" + " ");
                        string newModel = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newModel))
                        {
                            transport.Model = newModel;
                            Console.WriteLine("Модель изменена");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Вы не вели модель");
                        }
                    }
                    else if (choise == 2)
                    {
                        Console.Write("Введите новую цену\n>" + " ");
                        double newPrice = Convert.ToDouble(Console.ReadLine());
                        if (newPrice > 0)
                        {
                            transport.PricePerHour = newPrice;
                            Console.WriteLine("Цена изменена");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Введите корректную цену");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Выберите число от 1 до 2");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
