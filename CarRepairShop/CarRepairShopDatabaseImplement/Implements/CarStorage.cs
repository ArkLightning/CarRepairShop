using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.StoragesContracts;
using CarRepairShopContracts.ViewModels;
using CarRepairShopDatabaseImplement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopDatabaseImplement.Implements
{
    public class CarStorage: ICarStorage
    {
        public List<CarViewModel> GetFullList()
        {
            using var context = new CarRepairShopDatabase();
            return context.Cars
                    .Include(x => x.DetailComponents)
                    .ThenInclude(x => x.DetailName)
                    .ToList()
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public List<CarViewModel> GetFilteredList(CarSearchModel model)
        {
            if (string.IsNullOrEmpty(model.CarName))
            {
                return new();
            }
            using var context = new CarRepairShopDatabase();
            return context.Cars
                    .Include(x => x.DetailComponents)
                    .ThenInclude(x => x.DetailName)
                    .Where(x => x.CarName.Contains(model.CarName))
                    .ToList()
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public CarViewModel? GetElement(CarSearchModel model)
        {
            if (string.IsNullOrEmpty(model.CarName) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new CarRepairShopDatabase();
            return context.Cars
                .Include(x => x.DetailComponents)
                .ThenInclude(x => x.DetailName)
                .FirstOrDefault(x => (!string.IsNullOrEmpty(model.CarName) && x.CarName == model.CarName) ||
                                (model.Id.HasValue && x.Id == model.Id))
                ?.GetViewModel;
        }

        public CarViewModel? Insert(CarBindingModel model)
        {
            using var context = new CarRepairShopDatabase();
            var newIceCream = Car.Create(model);
            if (newIceCream == null)
            {
                return null;
            }
            context.Cars.Add(newIceCream);
            context.SaveChanges();
            return newIceCream.GetViewModel;
        }

        public CarViewModel? Update(CarBindingModel model)
        {
            using var context = new CarRepairShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var car = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);
                if (car == null)
                {
                    return null;
                }
                car.Update(model);
                context.SaveChanges();
                car.UpdateDetails(context, model);
                transaction.Commit();
                return car.GetViewModel;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public CarViewModel? Delete(CarBindingModel model)
        {
            using var context = new CarRepairShopDatabase();
            var element = context.Cars
                .Include(x => x.DetailComponents)
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Cars.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
