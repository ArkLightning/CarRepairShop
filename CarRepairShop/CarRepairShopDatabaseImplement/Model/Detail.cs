using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.ViewModels;
using CarRepairShopDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopDatabaseImplement.Model
{
    internal class Detail : IDetailModel
    {
        public int Id { get; private set; }
        [Required]
        public string DetailName { get; private set; } = string.Empty;
        [Required]
        public double Cost { get; set; }
        //[ForeignKey("DetailId")]
        public virtual List<CarDetail> ShipDetails { get; set; } = new();
        public static Detail? Create(DetailBindingModel model)
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
        public static Detail Create(DetailViewModel model)
        {
            return new Detail
            {
                Id = model.Id,
                DetailName = model.DetailName,
                Cost = model.Cost
            };
        }
        public void Update(DetailBindingModel model)
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
