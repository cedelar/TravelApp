using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TravelApp.Models;
using ViewModel;

namespace TravelApp.ViewModels.TravelPlanDetailsViewModel
{
    class ItemFrameViewModel: ComputedBindableBase
    {
        private ObservableCollection<TravelItem> _itemList;

        public ObservableCollection<TravelItem> ItemList
        {
            get
            {
                return _itemList;
            }
            set
            {
                Set(ref _itemList, value);
            }
        }

        private TravelItem _selectedItem;
        public TravelItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    Set(ref _selectedItem, value);
                }
            }
        }

        [PropertySource(nameof(ItemList))]
        public IEnumerable<TravelItemGroup> GroupedTravelItems
        {
            get
            {
                return new List<TravelItem>(_itemList)
                    .GroupBy(i => i.Category, (key, list) => new TravelItemGroup(key, list));
            }
        }

        public ItemFrameViewModel(NavViewItemsEventArgs e)
        {
            _itemList = e.Items;
        }
    }
}
