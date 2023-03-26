using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.ViewModels;
using CarRepairShopDataModels.Enums;
using CarRepairShopDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopDatabaseImplement.Model
{
    internal class Order : IOrderModel
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public double Sum { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public virtual Detail Detail { get; set; }

        public DateTime? DateImplement { get; set; }

        public static Order? Create(OrderBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new Order()
            {
                Id = model.Id,
                CarId = model.CarId,
                Count = model.Count,
                Sum = model.Sum,
                Status = model.Status,
                DateCreate = model.DateCreate,
                DateImplement = model.DateImplement
            };
        }

        public void Update(OrderBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            Status = model.Status;
            DateImplement = model.DateImplement;
        }

        public OrderViewModel GetViewModel => new()
        {
            Id = Id,
            CarId = CarId,
            Count = Count,
            Sum = Sum,
            Status = Status,
            DateCreate = DateCreate,
            DateImplement = DateImplement,
            CarName = Detail.DetailName
        };
    }
}
