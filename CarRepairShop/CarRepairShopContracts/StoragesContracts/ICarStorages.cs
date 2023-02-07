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
    public interface ICarStorage
    {
        List<CarViewModel> GetFullList();
        List<CarViewModel> GetFilteredList(CarSearchModel model);
        CarViewModel? GetElement(CarSearchModel model);
        CarViewModel? Insert(CarBindingModel model);
        CarViewModel? Update(CarBindingModel model);
        CarViewModel? Delete(CarBindingModel model);
    }
}
