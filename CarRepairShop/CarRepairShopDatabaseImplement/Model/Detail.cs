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
        public int Id { get; set; }

        [Required]
        public string DetailName { get; set; } = string.Empty;

        [Required]
        public double Cost { get; set; }

        private Dictionary<int, (ICarModel, int)>? _dishComponents = null;

        //??
        [NotMapped]
        public Dictionary<int, (ICarModel, int)> DishComponents
        {
            get
            {
                if (_dishComponents == null)
                {
                    _dishComponents = Components.ToDictionary(recPC => recPC.ComponentId, recPC => (recPC.Car as ICarModel, recPC.Count));
                }
                return _dishComponents;
            }
        }

        [ForeignKey("DishId")]
        public virtual List<DetailComponent> Components { get; set; } = new();

        public static Detail Create(CarRepairShopDatabase context, DetailBindingModel model)
        {
            return new Detail()
            {
                Id = model.Id,
                DetailName = model.DetailName,
                Cost = model.Cost,
                Components = model.DetailComponents.Select(x => new DetailComponent
                {
                    Car = context.Cars.First(y => y.Id == x.Key),
                    Count = x.Value.Item2
                }).ToList()
            };
        }

        public void Update(DetailBindingModel model)
        {
            DetailName = model.DetailName;
            Cost = model.Cost;
        }

        public DetailViewModel GetViewModel => new()
        {
            Id = Id,
            DetailName = DetailName,
            Cost = Cost,
            DetailComponents = DishComponents
        };

        public void UpdateComponents(CarRepairShopDatabase context, DetailBindingModel model)
        {
            var detailComponents = context.DetailComponents.Where(rec => rec.DetailId == model.Id).ToList();
            if (detailComponents != null && detailComponents.Count > 0)
            {   // удалили те в бд, которых нет в модели
                context.DetailComponents.RemoveRange(detailComponents.Where(rec => !model.DetailComponents.ContainsKey(rec.ComponentId)));
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in detailComponents)
                {
                    updateComponent.Count = model.DetailComponents[updateComponent.ComponentId].Item2;
                    model.DetailComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            var detail = context.Details.First(x => x.Id == Id);
            //добавляем в бд блюда которые есть в моделе, но ещё нет в бд
            foreach (var dc in model.DetailComponents)
            {
                context.DetailComponents.Add(new DetailComponent
                {
                    Detail = detail,
                    Car = context.Cars.First(x => x.Id == dc.Key),
                    Count = dc.Value.Item2
                });
                context.SaveChanges();
            }
            _dishComponents = null;
        }
    }
}
