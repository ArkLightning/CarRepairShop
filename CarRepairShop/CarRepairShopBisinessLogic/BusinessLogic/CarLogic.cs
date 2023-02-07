using Microsoft.Extensions.Logging;
using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.BusinessLogicsContracts;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.StoragesContracts;
using CarRepairShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopBusinessLogic.BusinessLogics
{
    public class CarLogic : ICarLogic
    {
        private readonly ILogger _logger;
        private readonly ICarStorage _carStorage;
        public CarLogic(ILogger<CarLogic> logger, ICarStorage carStorage)
        {
            _logger = logger;
            _carStorage = carStorage;
        }
        public List<CarViewModel>? ReadList(CarSearchModel? model)
        {
            _logger.LogInformation("ReadList. CarName:{CarName}. Id:{Id}", model?.CarName, model?.Id);
            var list = model == null ? _carStorage.GetFullList() : _carStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public CarViewModel? ReadElement(CarSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. CarName:{CarName}. Id:{ Id}", model.CarName, model.Id);
            var element = _carStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }
        public bool Create(CarBindingModel model)
        {
            CheckModel(model);
            if (_carStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(CarBindingModel model)
        {
            CheckModel(model);
            if (_carStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(CarBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_carStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(CarBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.CarName))
            {
                throw new ArgumentNullException("Нет названия компонента",
               nameof(model.CarName));
            }
            if (model.Price <= 0)
            {
                throw new ArgumentNullException("Цена компонента должна быть больше 0", nameof(model.Price));
            }
            _logger.LogInformation("Car. CarName:{CarName}. Price:{Price}. Id: {Id}", model.CarName, model.Price, model.Id);
            var element = _carStorage.GetElement(new CarSearchModel
            {
                CarName = model.CarName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Компонент с таким названием уже есть");
            }
        }
    }
}
