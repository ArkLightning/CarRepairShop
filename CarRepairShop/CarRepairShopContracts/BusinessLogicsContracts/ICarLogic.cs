﻿using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopContracts.BusinessLogicsContracts
{
    public interface ICarLogic
    {
        List<CarViewModel>? ReadList(CarSearchModel? model);
        CarViewModel? ReadElement(CarSearchModel model);
        bool Create(CarBindingModel model);
        bool Update(CarBindingModel model);
        bool Delete(CarBindingModel model);
    }
}
