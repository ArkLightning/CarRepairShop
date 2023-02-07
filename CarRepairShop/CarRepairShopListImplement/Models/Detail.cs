using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.ViewModels;
using CarRepairShopDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopListImplement.Models
{
    public class Detail : IDetailModel
    {
        public int Id { get; private set; }
        public string DetailName { get; private set; } = string.Empty;
        public double Cost { get; set; }
        public static Detail? Create(DetailBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new Detail()
            {
                Id = model.Id,
                DetailName = model.DetailName,
                Cost = model.Cost
            };
        }
        public void Update(DetailBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            DetailName = model.DetailName;
            Cost = model.Cost;
        }
        public DetailViewModel GetViewModel => new()
        {
            Id = Id,
            DetailName = DetailName,
            Cost = Cost
        };

    }
}
