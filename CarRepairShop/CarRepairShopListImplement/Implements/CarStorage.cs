using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.StoragesContracts;
using CarRepairShopContracts.ViewModels;
using CarRepairShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopListImplement.Implements
{
    public class CarStorage : ICarStorage
    {
        private readonly DataListSingleton _source;
        public CarStorage()
        {
            _source = DataListSingleton.GetInstance();
        }
        public List<CarViewModel> GetFullList()
        {
            var result = new List<CarViewModel>();
            foreach (var car in _source.Cars)
            {
                result.Add(car.GetViewModel);
            }
            return result;
        }
        public List<CarViewModel> GetFilteredList(CarSearchModel model)
        {
            var result = new List<CarViewModel>();
            if (string.IsNullOrEmpty(model.CarName))
            {
                return result;
            }
            foreach (var car in _source.Cars)
            {
                if (car.CarName.Contains(model.CarName))
                {
                    result.Add(car.GetViewModel);
                }
            }
            return result;
        }
        public CarViewModel? GetElement(CarSearchModel model)
        {
            if (string.IsNullOrEmpty(model.CarName) && !model.Id.HasValue)
            {
                return null;
            }
            foreach (var car in _source.Cars)
            {
                if ((!string.IsNullOrEmpty(model.CarName) &&
               car.CarName == model.CarName) ||
                (model.Id.HasValue && car.Id == model.Id))
                {
                    return car.GetViewModel;
                }
            }
            return null;
        }
        public CarViewModel? Insert(CarBindingModel model)
        {
            model.Id = 1;
            foreach (var car in _source.Cars)
            {
                if (model.Id <= car.Id)
                {
                    model.Id = car.Id + 1;
                }
            }
            var newCar = Car.Create(model);
            if (newCar == null)
            {
                return null;
            }
            _source.Cars.Add(newCar);
            return newCar.GetViewModel;
        }
        public CarViewModel? Update(CarBindingModel model)
        {
            foreach (var car in _source.Cars)
            {
                if (car.Id == model.Id)
                {
                    car.Update(model);
                    return car.GetViewModel;
                }
            }
            return null;
        }
        public CarViewModel? Delete(CarBindingModel model)
        {
            for (int i = 0; i < _source.Cars.Count; ++i)
            {
                if (_source.Cars[i].Id == model.Id)
                {
                    var element = _source.Cars[i];
                    _source.Cars.RemoveAt(i);
                    return element.GetViewModel;
                }
            }
            return null;
        }
    }
}
