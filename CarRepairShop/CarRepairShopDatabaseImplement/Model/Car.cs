using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.ViewModels;
using CarRepairShopDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopDatabaseImplement.Model
{
    internal class Car : ICarModel
    {
        public int Id { get; private set; }

        [Required]
        public string CarName { get; private set; } = string.Empty;

        [Required]
        public double Price { get; set; }

        [ForeignKey("ComponentId")]
        public virtual List<Detail> DishComponents { get; set; } = new();

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

        public static Car Create(CarViewModel model)
        {
            return new Car
            {
                Id = model.Id,
                CarName = model.CarName,
                Price = model.Price
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
    }
}
