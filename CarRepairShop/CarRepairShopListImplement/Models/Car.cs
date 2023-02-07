using CarRepairShopContracts.ViewModels;
using CarRepairShopDataModels.Models;
using CarRepairShopContracts.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;

namespace CarRepairShopListImplement.Models
{
    public class Car : ICarModel
    {
        public int Id { get; private set; }
        public string CarName { get; private set; } = string.Empty;
        public double Price { get; private set; }
        public Dictionary<int, (IDetailModel, int)> CarDetails { get; private set; } = new Dictionary<int, (IDetailModel, int)>();
        public static Car? Create(CarBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new Car()
            {
                Id = model.Id,
                CarName = model.CarName,
                Price = model.Price,
                CarDetails = model.CarDetails
            };
        }
        public void Update(CarBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            CarName = model.CarName;
            Price = model.Price;
            CarDetails = model.CarDetails;
        }
        public CarViewModel GetViewModel => new()
        {
            Id = Id,
            CarName = CarName,
            Price = Price,
            CarDetails = CarDetails
        };

    }
}
