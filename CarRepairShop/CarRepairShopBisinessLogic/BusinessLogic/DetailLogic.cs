using CarRepairShopContracts.BusinessLogicsContracts;
using CarRepairShopContracts.StoragesContracts;
using CarRepairShopContracts.ViewModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CarRepairShopBisinessLogic.BusinessLogic
{
    public class DetailLogic : IDetailLogic
    {
        private readonly ILogger _logger;
        private readonly IDetailStorage _detailStorage;
        public DetailLogic(ILogger<DetailLogic> logger, IDetailStorage detailStorage)
        {
            _logger = logger;
            _detailStorage = detailStorage;
        }
        public List<DetailViewModel>? ReadList(DetailSearchModel? model)
        {
            _logger.LogInformation("ReadList. DetailName:{DetailName}. Id:{Id}", model?.DetailName, model?.Id);
            var list = model == null ? _detailStorage.GetFullList() : _detailStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public DetailViewModel? ReadElement(DetailSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. DetailName:{DetailName}. Id:{ Id}", model.DetailName, model.Id);
            var element = _detailStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }
        public bool Create(DetailBindingModel model)
        {
            CheckModel(model);
            if (_detailStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(DetailBindingModel model)
        {
            CheckModel(model);
            if (_detailStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(DetailBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_detailStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(DetailBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.DetailName))
            {
                throw new ArgumentNullException("Нет названия компонента",
               nameof(model.DetailName));
            }
            if (model.Cost <= 0)
            {
                throw new ArgumentNullException("Цена компонента должна быть больше 0", nameof(model.Cost));
            }
            _logger.LogInformation("Detail. DetailName:{DetailName}. Cost:{ Cost}. Id: { Id}", model.DetailName, model.Cost, model.Id);
            var element = _detailStorage.GetElement(new DetailSearchModel
            {
                DetailName = model.DetailName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Компонент с таким названием уже есть");
            }
        }
    }
}
