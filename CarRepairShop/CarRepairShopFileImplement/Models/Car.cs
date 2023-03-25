using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.StoragesContracts;
using CarRepairShopContracts.ViewModels;
using CarRepairShopFileImplement.Models;
using CarRepairShopDataModels.Models;
using System.Xml.Linq;

namespace CarRepairShopFileImplement.Models
{
    public class Car : ICarModel
    {
        public int Id { get; private set; }
        public string CarName { get; private set; } = string.Empty;
        public double Price { get; set; }
        public static Car? Create(CarBindingModel model)
        { 
            if (model == null)
            {
                return null;
            }
            return new Car()
            {
                Id = model.Id,
                CarName = model.CarName,
                Price = model.Price
            };
        }
        public static Car? Create(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new Car()
            {
                Id = Convert.ToInt32(element.Attribute("Id")!.Value),
                CarName = element.Element("ComponentName")!.Value,
                Price= Convert.ToDouble(element.Element("Cost")!.Value)
            };
        }
        public void Update(CarBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            CarName = model.CarName;
            Price = model.Price;
        }
        public CarViewModel GetViewModel => new()
        {
            Id = Id,
            CarName = CarName,
            Price = Price
        };
        public XElement GetXElement => new(
            "Component",
            new XAttribute("Id", Id),
            new XElement("CartName", CarName),
            new XElement("Cost", Price.ToString())
        );
    }
}