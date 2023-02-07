using CarRepairShopDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopContracts.ViewModels
{
    public class DetailViewModel : IDetailModel
    {
        public int Id { get; set; }
        [DisplayName("Название компонента")]
        public string DetailName { get; set; } = string.Empty;
        [DisplayName("Цена")]
        public double Cost { get; set; }
    }
}
