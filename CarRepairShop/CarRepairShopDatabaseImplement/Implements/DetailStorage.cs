using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.StoragesContracts;
using CarRepairShopContracts.ViewModels;
using CarRepairShopDatabaseImplement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopDatabaseImplement.Implements
{
    public class DetailStorage : IDetailStorage
    {
        public List<DetailViewModel> GetFullList()
        {
            using var context = new CarRepairShopDatabase();
            return context.Details
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public List<DetailViewModel> GetFilteredList(DetailSearchModel model)
        {
            if (string.IsNullOrEmpty(model.DetailName))
            {
                return new();
            }
            using var context = new CarRepairShopDatabase();
            return context.Details
                    .Where(x => x.DetailName.Contains(model.DetailName))
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public DetailViewModel? GetElement(DetailSearchModel model)
        {
            if (string.IsNullOrEmpty(model.DetailName) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new CarRepairShopDatabase();
            return context.Details
                    .FirstOrDefault(x => (!string.IsNullOrEmpty(model.DetailName) && x.DetailName == model.DetailName) ||
                                        (model.Id.HasValue && x.Id == model.Id))
                    ?.GetViewModel;
        }

        public DetailViewModel? Insert(DetailBindingModel model)
        {
            using var context = new CarRepairShopDatabase();
            var newDetail = Detail.Create(model);
            if (newDetail == null)
            {
                return null;
            }
            context.Details.Add(newDetail);
            context.SaveChanges();
            return newDetail.GetViewModel;
        }

        public DetailViewModel? Update(DetailBindingModel model)
        {
            using var context = new CarRepairShopDatabase();
            var additive = context.Details.FirstOrDefault(x => x.Id == model.Id);
            if (additive == null)
            {
                return null;
            }
            additive.Update(model);
            context.SaveChanges();
            return additive.GetViewModel;
        }

        public DetailViewModel? Delete(DetailBindingModel model)
        {
            using var context = new CarRepairShopDatabase();
            var element = context.Details.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Details.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }
    }
}