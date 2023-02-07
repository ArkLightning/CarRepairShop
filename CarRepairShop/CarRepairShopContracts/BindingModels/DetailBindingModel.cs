using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRepairShopDataModels.Models;

namespace CarRepairShopContracts.BindingModels
{
    public class DetailBindingModel : IDetailModel
    {
        public int Id { get; set; }
        public string DetailName { get; set; } = string.Empty;
        public double Cost { get; set; }
    }
}
