using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class TravelItem
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public int Amount { get; set; }
        public string Category { get; set; }

        [JsonConstructor]
        public TravelItem(string name, int amount, string category)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Amount = amount;
            this.Category = category ?? throw new ArgumentNullException(nameof(category));
            IsChecked = false;
        }

        public TravelItem(string naam) : this(naam, 0, "No Category") { }

        public override string ToString()
        {
            return Name + " (" + Amount + ") " + Category;
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
