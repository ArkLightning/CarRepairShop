using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.ViewModels;
using CarRepairShopDataModels.Enums;
using CarRepairShopDataModels.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopListImplement.Models
{
    public class Order : IOrderModel
    {
        public int Id { get; private set; }
        public int CarId { get; private set; }
        public string CarName { get; private set; }
        public int Count { get; private set; }
        public double Sum { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Неизвестен;
        public DateTime DateCreate { get; private set; } = DateTime.Now;
        public DateTime? DateImplement { get; private set; }

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
                CarName = model.CarName,
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
            Id = model.Id;
            CarId = model.CarId;
            CarName = model.CarName;
            Count = model.Count;
            Sum = model.Sum;
            Status = model.Status;
            DateCreate = model.DateCreate;
            DateImplement = model.DateImplement;
        }
        public OrderViewModel GetViewModel => new()
        {
            Id = Id,
            CarId = CarId,
            CarName = CarName,
            Count = Count,
            Sum = Sum,
            Status = Status,
            DateCreate = DateCreate,
            DateImplement = DateImplement
        };

    }
}