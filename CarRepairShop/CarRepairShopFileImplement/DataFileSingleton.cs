using CarRepairShopFileImplement.Models;
using FoodOrdersFileImplement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarRepairShopFileImplement
{
    internal class DataFileSingleton
    {
        private static DataFileSingleton? instance;
        private readonly string CarFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string DetailFileName = "Detail.xml";
        public List<Component> Components { get; private set; }
        public List<Order> Orders { get; private set; }
        public List<Detail> Details { get; private set; }
        public static DataFileSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataFileSingleton();
            }
            return instance;
        }
        public void SaveComponents() => SaveData(Components, CarFileName, "Components", x => x.GetXElement);
        public void SaveDishes() => SaveData(Details, DetailFileName, "Dishes", x => x.GetXElement);
        public void SaveOrders() => SaveData(Orders, OrderFileName, "Orders", x => x.GetXElement);
        private DataFileSingleton()
        {
            Components = LoadData(CarFileName, "Component", x => Component.Create(x)!)!;
            Details = LoadData(DetailFileName, "Dish", x => Detail.Create(x)!)!;
            Orders = LoadData(OrderFileName, "Order", x => Order.Create(x)!)!;
        }
        private static List<T>? LoadData<T>(string filename, string xmlNodeName, Func<XElement, T> selectFunction)
        {
            if (File.Exists(filename))
            {
                return XDocument.Load(filename)?.Root?.Elements(xmlNodeName)?.Select(selectFunction)?.ToList();
            }
            return new List<T>();
        }
        private static void SaveData<T>(List<T> data, string filename, string xmlNodeName, Func<T, XElement> selectFunction)
        {
            if (data != null)
            {
                new XDocument(new XElement(xmlNodeName, data.Select(selectFunction).ToArray())).Save(filename);
            }
        }
    }
}
