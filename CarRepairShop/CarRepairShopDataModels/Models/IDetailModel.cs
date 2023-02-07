using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopDataModels.Models
{
    public interface IDetailModel : IId
    {
        string DetailName { get; }
        double Cost { get; }
    }
}
