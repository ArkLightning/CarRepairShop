using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.StoragesContracts;
using CarRepairShopContracts.ViewModels;
using CarRepairShopListImplement.Models;
using CarRepairShopListImplement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopListImplement.Implements
{
    public class DetailStorage : IDetailStorage
    {
        private readonly DataListSingleton _source;
        public DetailStorage()
        {
            _source = DataListSingleton.GetInstance();
        }
        public List<DetailViewModel> GetFullList()
        {
            var result = new List<DetailViewModel>();
            foreach (var detail in _source.Details)
            {
                result.Add(detail.GetViewModel);
            }
            return result;
        }
        public List<DetailViewModel> GetFilteredList(DetailSearchModel model)
        {
            var result = new List<DetailViewModel>();
            if (string.IsNullOrEmpty(model.DetailName))
            {
                return result;
            }
            foreach (var detail in _source.Details)
            {
                if (detail.DetailName.Contains(model.DetailName))
                {
                    result.Add(detail.GetViewModel);
                }
            }
            return result;
        }
        public DetailViewModel? GetElement(DetailSearchModel model)
        {
            if (string.IsNullOrEmpty(model.DetailName) && !model.Id.HasValue)
            {
                return null;
            }
            foreach (var detail in _source.Details)
            {
                if ((!string.IsNullOrEmpty(model.DetailName) &&
               detail.DetailName == model.DetailName) ||
                (model.Id.HasValue && detail.Id == model.Id))
                {
                    return detail.GetViewModel;
                }
            }
            return null;
        }
        public DetailViewModel? Insert(DetailBindingModel model)
        {
            model.Id = 1;
            foreach (var component in _source.Details)
            {
                if (model.Id <= component.Id)
                {
                    model.Id = component.Id + 1;
                }
            }
            var newDetail = Detail.Create(model);
            if (newDetail == null)
            {
                return null;
            }
            _source.Details.Add(newDetail);
            return newDetail.GetViewModel;
        }
        public DetailViewModel? Update(DetailBindingModel model)
        {
            foreach (var detail in _source.Details)
            {
                if (detail.Id == model.Id)
                {
                    detail.Update(model);
                    return detail.GetViewModel;
                }
            }
            return null;
        }
        public DetailViewModel? Delete(DetailBindingModel model)
        {
            for (int i = 0; i < _source.Details.Count; ++i)
            {
                if (_source.Details[i].Id == model.Id)
                {
                    var element = _source.Details[i];
                    _source.Details.RemoveAt(i);
                    return element.GetViewModel;
                }
            }
            return null;
        }

    }
}
