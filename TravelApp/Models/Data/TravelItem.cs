using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    class TravelItem
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public int Amount { get; set; }
        public string Category { get; set; }

        public TravelItem(string naam, int amount, string category)
        {
            this.Name = naam ?? throw new ArgumentNullException(nameof(naam));
            this.Amount = amount;
            this.Category = category ?? throw new ArgumentNullException(nameof(category));
            IsChecked = false;
        }

        public TravelItem(string naam) : this(naam, 0, "default") { }

        public override string ToString()
        {
            return Name + " (" + Amount + ")";
        }

    }

    class TravelItemGroup : IGrouping<string, TravelItem>
    {
        public string Key { get; }
        private List<TravelItem> _travelItemGroup;

        public TravelItemGroup(string key, IEnumerable<TravelItem> items)
        {
            Key = key;
            _travelItemGroup = new List<TravelItem>(items);
        }
        public IEnumerator<TravelItem> GetEnumerator() => _travelItemGroup.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _travelItemGroup.GetEnumerator();
    }
}
