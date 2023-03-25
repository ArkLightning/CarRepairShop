using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.StoragesContracts;
using CarRepairShopContracts.ViewModels;
using CarRepairShopFileImplement.Models;
using FoodOrdersFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopFileImplement.Implements
{
    internal class DetailStorage : IDetailStorage
    {
        private readonly DataFileSingleton _source;
        public DetailStorage()
        {
            _source = DataFileSingleton.GetInstance();
        }
        public List<DetailViewModel> GetFullList()
        {
            return _source.Details.Select(x => x.GetViewModel).ToList();
        }
        public List<DetailViewModel> GetFilteredList(DetailSearchModel model)
        {
            if (string.IsNullOrEmpty(model.DetailName))
            {
                return new();
            }
            return _source.Details.Where(x => x.DetailName.Contains(model.DetailName)).Select(x => x.GetViewModel).ToList();
        }
        public DetailViewModel? GetElement(DetailSearchModel model)
        {
            if (string.IsNullOrEmpty(model.DetailName) && !model.Id.HasValue)
            {
                return null;
            }
            return _source.Details.FirstOrDefault(x => (!string.IsNullOrEmpty(model.DetailName) && x.DetailName == model.DetailName) || (model.Id.HasValue && x.Id == model.Id))?.GetViewModel;
        }
        public DetailViewModel? Insert(DetailBindingModel model)
        {
            model.Id = _source.Details.Count > 0 ? _source.Details.Max(x => x.Id) + 1 : 1;
            var newDoc = Detail.Create(model);
            if (newDoc == null)
            {
                return null;
            }
            _source.Details.Add(newDoc);
            _source.SaveDetails();
            return newDoc.GetViewModel;
        }

        public DetailViewModel? Update(DetailBindingModel model)
        {
            var detail = _source.Details.FirstOrDefault(x => x.Id == model.Id);
            if (detail == null)
            {
                return null;
            }
            detail.Update(model);
            _source.SaveDetails();
            return detail.GetViewModel;
        }

        public DetailViewModel? Delete(DetailBindingModel model)
        {
            var document = _source.Details.FirstOrDefault(x => x.Id == model.Id);
            if (document == null)
            {
                return null;
            }
            document.Update(model);
            _source.SaveDetails();
            return document.GetViewModel;
        }
    }
}
