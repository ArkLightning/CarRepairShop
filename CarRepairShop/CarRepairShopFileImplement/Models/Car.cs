using CarRepairShopContracts.ViewModels;
using CarRepairShopDataModels.Models;
using CarRepairShopContracts.BindingModels;
using System.Xml.Linq;

namespace CarRepairShopFileImplement.Models
{
    public class Car : ICarModel
    {
        public int Id { get; private set; }
        public string ComponentName { get; private set; } = string.Empty;
        public double Cost { get; set; }
        public static Car? Create(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Car()
            {
                Id = model.Id,
                ComponentName = model.ComponentName,
                Cost = model.Cost
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
                ComponentName = element.Element("ComponentName")!.Value,
                Cost = Convert.ToDouble(element.Element("Cost")!.Value)
            };
        }
        public void Update(CarBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            ComponentName = model.ComponentName;
            Cost = model.Cost;
        }
        public CarViewModel GetViewModel => new()
        {
            Id = Id,
            ComponentName = ComponentName,
            Cost = Cost
        };
        public XElement GetXElement => new(
            "Component",
            new XAttribute("Id", Id),
            new XElement("ComponentName", ComponentName),
            new XElement("Cost", Cost.ToString())
        );
    }
}