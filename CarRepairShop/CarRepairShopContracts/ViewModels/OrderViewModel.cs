﻿using CarRepairShopDataModels.Enums;
using CarRepairShopDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopContracts.ViewModels
{
    public class OrderViewModel:IOrderModel
    {
        [DisplayName("Номер")]
        public int Id { get; set; }
        public int CarId { get; set; }
        [DisplayName("Изделие")]
        public string CarName { get; set; } = string.Empty;
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        public double Sum { get; set; }
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; } = OrderStatus.Неизвестен;
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; } = DateTime.Now;
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
