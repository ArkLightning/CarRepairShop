using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.StoragesContracts;
using CarRepairShopContracts.ViewModels;
using CarRepairShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopFileImplement.Implements
{
    internal class CarStorage : ICarStorage
    {
        private readonly DataFileSingleton _source;
        public CarStorage()
        {
            _source = DataFileSingleton.GetInstance();
        }
        public List<CarViewModel> GetFullList()
        {
            return _source.Cars.Select(x => x.GetViewModel).ToList();
        }
        public List<CarViewModel> GetFilteredList(CarSearchModel model)
        {
            if (string.IsNullOrEmpty(model.CarName))
            {
                return new();
            }
            return _source.Cars.Where(x => x.CarName.Contains(model.CarName)).Select(x => x.GetViewModel).ToList();
        }

        //FirstOrDefault выбирается первый или ничего, то есть вернёт первое совпадение или null
        public CarViewModel? GetElement(CarSearchModel model)
        {
            if (string.IsNullOrEmpty(model.CarName) && !model.Id.HasValue)
            {
                return null;
            }
            return _source.Cars.FirstOrDefault(x => (!string.IsNullOrEmpty(model.CarName) && x.CarName == model.CarName) || (model.Id.HasValue && x.Id == model.Id))?.GetViewModel;
        }
        public CarViewModel? Insert(CarBindingModel model)
        {
            model.Id = _source.Cars.Count > 0 ? _source.Cars.Max(x => x.Id) + 1 : 1;
            var newComponent = Car.Create(model);
            if (newComponent == null)
            {
                return null;
            }
            _source.Cars.Add(newComponent);
            _source.SaveCars();
            return newComponent.GetViewModel;
        }
        public CarViewModel? Update(CarBindingModel model)
        {
            var component = _source.Cars.FirstOrDefault(x => x.Id == model.Id);
            if (component == null)
            {
                return null;
            }
            component.Update(model);
            _source.SaveCars();
            return component.GetViewModel;
        }
        public CarViewModel? Delete(CarBindingModel model)
        {
            var element = _source.Cars.FirstOrDefault(x => x.Id == model.Id);
            if (element != null)
            {
                _source.Cars.Remove(element);
                _source.SaveCars();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
