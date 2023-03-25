using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopDataModels.Models
{
    public interface ICarModel : IId
    {
        string CarName { get; }
        double Price { get; }
        //Dictionary<int, (IDetailModel, int)> CarDetails { get; }
    }
}
