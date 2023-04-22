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
        private Dictionary<int, (IDetailModel, int)>? _carDetails = null;

        //[ForeignKey("DetailId")]
        public virtual List<Detail> DetailComponents { get; set; } = new();

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

        public void UpdateDetails(CarRepairShopDatabase context,
       CarBindingModel model)
        {
            var carDetails = context.DetailComponents.Where(rec =>
           rec.CarId == model.Id).ToList();
            if (carDetails != null && carDetails.Count > 0)
            { // удалили те, которых нет в модели
                context.DetailComponents.RemoveRange(carDetails.Where(rec
               => !model.CarDetails.ContainsKey(rec.CarId)));
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateDetail in carDetails)
                {
                    updateDetail.Count = model.CarDetails[updateDetail.CarId].Item2;
                    model.CarDetails.Remove(updateDetail.CarId);
                }
                context.SaveChanges();
            }
            var car = context.Cars.First(x => x.Id == Id);
            foreach (var pc in model.CarDetails)
            {
                context.DetailComponents.Add(new CarDetail
                {
                    Car = car,
                    Detail = context.Details.First(x => x.Id == pc.Key),
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            _carDetails = null;
        }
    }
}
