using Microsoft.EntityFrameworkCore;
using CarRepairShopContracts.BindingModels;
using CarRepairShopContracts.SearchModels;
using CarRepairShopContracts.StoragesContracts;
using CarRepairShopContracts.ViewModels;
using CarRepairShopDatabaseImplement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopDatabaseImplement.Implements
{
    public class OrderStorage: IOrderStorage
    {
        public OrderViewModel? Delete(OrderBindingModel model)
        {
            using var context = new CarRepairShopDatabase();
            var element = context.Orders
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                // для отображения КОРРЕКТНОЙ viewmodelи
                var deletedElement = context.Orders
                                            .Include(x => x.Detail)
                                            .FirstOrDefault(x => x.Id == model.Id)
                                            ?.GetViewModel;
                context.Orders.Remove(element);
                context.SaveChanges();
                return deletedElement;
            }
            return null;
        }

        public OrderViewModel? GetElement(OrderSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return null;
            }
            using var context = new CarRepairShopDatabase();
            return context.Orders
                .Include(x => x.Detail)
                .FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
                ?.GetViewModel;
        }

        public List<OrderViewModel> GetFilteredList(OrderSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return new();
            }
            using var context = new CarRepairShopDatabase();
            return context.Orders
                    .Include(x => x.Detail)
                    .Where(x => x.Id == model.Id)
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public List<OrderViewModel> GetFullList()
        {
            using var context = new CarRepairShopDatabase();
            return context.Orders
                    .Include(x => x.Detail)
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public OrderViewModel? Insert(OrderBindingModel model)
        {
            var newOrder = Order.Create(model);
            if (newOrder == null)
            {
                return null;
            }
            using var context = new CarRepairShopDatabase();
            context.Orders.Add(newOrder);
            context.SaveChanges();
            return context.Orders
                          .Include(x => x.Detail)
                          .FirstOrDefault(x => x.Id == newOrder.Id)
                          ?.GetViewModel;
        }

        public OrderViewModel? Update(OrderBindingModel model)
        {
            using var context = new CarRepairShopDatabase();
            var order = context.Orders.FirstOrDefault(x => x.Id == model.Id);
            if (order == null)
            {
                return null;
            }
            order.Update(model);
            context.SaveChanges();
            return context.Orders
                          .Include(x => x.Detail)
                          .FirstOrDefault(x => x.Id == model.Id)
                          ?.GetViewModel;
        }
    }
}
