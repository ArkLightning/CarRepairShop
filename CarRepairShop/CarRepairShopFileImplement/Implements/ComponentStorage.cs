using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopFileImplement.Implements
{
    internal class ComponentStorage
    {
        private readonly DataFileSingleton _source;
        public ComponentStorage()
        {
            _source = DataFileSingleton.GetInstance();
        }
        public List<ComponentViewModel> GetFullList()
        {
            return _source.Components.Select(x => x.GetViewModel).ToList();
        }
        public List<ComponentViewModel> GetFilteredList(ComponentSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ComponentName))
            {
                return new();
            }
            return _source.Components.Where(x => x.ComponentName.Contains(model.ComponentName)).Select(x => x.GetViewModel).ToList();
        }

        //FirstOrDefault выбирается первый или ничего, то есть вернёт первое совпадение или null
        public ComponentViewModel? GetElement(ComponentSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ComponentName) && !model.Id.HasValue)
            {
                return null;
            }
            return _source.Components.FirstOrDefault(x => (!string.IsNullOrEmpty(model.ComponentName) && x.ComponentName == model.ComponentName) || (model.Id.HasValue && x.Id == model.Id))?.GetViewModel;
        }
        public ComponentViewModel? Insert(ComponentBindingModel model)
        {
            model.Id = _source.Components.Count > 0 ? _source.Components.Max(x => x.Id) + 1 : 1;
            var newComponent = Component.Create(model);
            if (newComponent == null)
            {
                return null;
            }
            _source.Components.Add(newComponent);
            _source.SaveComponents();
            return newComponent.GetViewModel;
        }
        public ComponentViewModel? Update(ComponentBindingModel model)
        {
            var component = _source.Components.FirstOrDefault(x => x.Id == model.Id);
            if (component == null)
            {
                return null;
            }
            component.Update(model);
            _source.SaveComponents();
            return component.GetViewModel;
        }
        public ComponentViewModel? Delete(ComponentBindingModel model)
        {
            var element = _source.Components.FirstOrDefault(x => x.Id == model.Id);
            if (element != null)
            {
                _source.Components.Remove(element);
                _source.SaveComponents();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
