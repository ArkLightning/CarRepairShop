using CarRepairShopDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopContracts.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название изделия")]
        public string CarName { get; set; } = string.Empty;
        [DisplayName("Цена")]
        public double Price { get; set; }
        public Dictionary<int, (IDetailModel, int)> CarDetails { get; set; } = new();
    }
}
