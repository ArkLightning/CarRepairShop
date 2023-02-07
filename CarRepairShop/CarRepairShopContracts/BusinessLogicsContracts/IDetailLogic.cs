using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopContracts.BusinessLogicsContracts
{
    public interface IDetailLogic
    {
        List<DetailViewModel>? ReadList(DetailSearchModel? model);
        DetailViewModel? ReadElement(DetailSearchModel model);
        bool Create(DetailBindingModel model);
        bool Update(DetailBindingModel model);
        bool Delete(DetailBindingModel model);
    }
}
