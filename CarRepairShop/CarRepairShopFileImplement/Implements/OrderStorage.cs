using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.StoragesContracts;
using CarRepairShopContracts.ViewModels;
using CarRepairShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopFileImplement.Implements
{
    internal class OrderStorage : IOrderStorage
    {
        private readonly DataFileSingleton _source;
        public OrderStorage()
        {
            _source = DataFileSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList()
        {
            return _source.Orders.Select(x => GetViewModel(x)).ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return new();
            }
            return _source.Orders.Where(x => x.Id == model.Id).Select(x => GetViewModel(x)).ToList();
        }

        public OrderViewModel? GetElement(OrderSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return null;
            }
            return _source.Orders.FirstOrDefault(x => (model.Id.HasValue && x.Id == model.Id))?.GetViewModel;
        }

        private OrderViewModel GetViewModel(Order order)
        {
            var viewModel = order.GetViewModel;
            viewModel.CarName = _source.Details.FirstOrDefault(x => (x.Id == order.CarId))?.DetailName;
            return viewModel;
        }

        public OrderViewModel? Delete(OrderBindingModel model)
        {
            var order = _source.Orders.FirstOrDefault(x => x.Id == model.Id);
            if (order == null)
            {
                return null;
            }
            order.Update(model);
            _source.SaveOrders();
            return order.GetViewModel;
        }

        public OrderViewModel? Insert(OrderBindingModel model)
        {
            model.Id = _source.Orders.Count > 0 ? _source.Orders.Max(x => x.Id) + 1 : 1;
            var newOrder = Order.Create(model);
            if (newOrder == null)
            {
                return null;
            }
            _source.Orders.Add(newOrder);
            _source.SaveOrders();
            return newOrder.GetViewModel;
        }

        public OrderViewModel? Update(OrderBindingModel model)
        {
            var order = _source.Orders.FirstOrDefault(x => x.Id == model.Id);
            if (order == null)
            {
                return null;
            }
            order.Update(model);
            _source.SaveOrders();
            return order.GetViewModel;
        }
    }
}
