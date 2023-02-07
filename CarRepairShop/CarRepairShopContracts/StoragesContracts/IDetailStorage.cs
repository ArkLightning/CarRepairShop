using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopContracts.StoragesContracts
{
    public interface IDetailStorage
    {
        List<DetailViewModel> GetFullList();
        List<DetailViewModel> GetFilteredList(DetailSearchModel model);
        DetailViewModel? GetElement(DetailSearchModel model);
        DetailViewModel? Insert(DetailBindingModel model);
        DetailViewModel? Update(DetailBindingModel model);
        DetailViewModel? Delete(DetailBindingModel model);
    }
}
