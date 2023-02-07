using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRepairShopDataModels.Models;

namespace CarRepairShopContracts.BindingModels
{
    public class CarBindingModel : ICarModel
    {
        public int Id { get; set; }
        public string CarName { get; set; } = String.Empty;
        public double Price { get; set; }
        public Dictionary<int, (IDetailModel, int)> CarDetails { get; set; } = new();

    }
}
