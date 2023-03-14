using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.ViewModels;
using CarRepairShopDataModels.Models;
using FoodOrdersFileImplement;
using System.Xml.Linq;
namespace FoodOrdersFileImplement.Models
{
    public class Detail : ICarModel
    {
        public int Id { get; private set; }

        public string DetailName { get; private set; } = string.Empty;

        public double Price { get; private set; }

        //словарь для файла, так как нам в файле нужно хранить просто id компонента и его количество
        public Dictionary<int, int> Components { get; private set; } = new();

        private Dictionary<int, (ICarModel, int)>? _detailComponents = null;

        public Dictionary<int, (ICarModel, int)> DetailComponents
        {
            get
            {
                if (_detailComponents == null)
                {
                    var _source = DataFileSingleton.GetInstance();
                    _detailComponents = Components.ToDictionary(x => x.Key, y => ((_source.Components.FirstOrDefault(z => z.Id == y.Key) as IComponentModel)!, y.Value));
                }
                return _detailComponents;
            }
        }
        public static Detail? Create(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Detail()
            {
                Id = model.Id,
                DetailName = model.DetailName,
                Price = model.Price,
                Components = model.DetailComponents.ToDictionary(x => x.Key, x => x.Value.Item2)
            };
        }
        public static Detail? Create(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new Detail()
            {
                Id = Convert.ToInt32(element.Attribute("Id")!.Value),
                DetailName = element.Element("DetailName")!.Value,
                Price = Convert.ToDouble(element.Element("Price")!.Value),
                Components = element.Element("DetailComponents")!.Elements("DetailComponent").ToDictionary(x => Convert.ToInt32(x.Element("Key")?.Value), x => Convert.ToInt32(x.Element("Value")?.Value))
            };
        }
        public void Update(DetailBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            DetailName = model.DetailName;
            Price = model.Price;
            Components = model.DetailComponents.ToDictionary(x => x.Key, x => x.Value.Item2);
            //обнуляем словарь, чтобы в случае обновления, у нас был в дальнейшем сформирован актуальный словарь
            // с помощью get метода
            _detailComponents = null;
        }
        public DetailViewModel GetViewModel => new()
        {
            Id = Id,
            DetailName = DetailName,
            Price = Price,
            DetailComponents = DetailComponents
        };

        public XElement GetXElement => new(
            "Detail",
            new XAttribute("Id", Id),
            new XElement("DetailName", DetailName),
            new XElement("Price", Price.ToString()),
            new XElement("DetailComponents", Components.Select(x =>
                new XElement("DishComponent",
                new XElement("Key", x.Key),
                new XElement("Value", x.Value))).ToArray()
            )
        );
    }
}