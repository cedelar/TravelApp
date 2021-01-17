using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TravelApp.Models
{
    /// <Summary>
    /// Data class for the TravelItem
    /// </Summary>
    public class TravelItem
    {
        #region Properties
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public int Amount { get; set; }
        public string Category { get; set; }
        #endregion

        #region Constructors
        [JsonConstructor]
        public TravelItem(string name, int amount, string category)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Amount = amount;
            this.Category = category ?? throw new ArgumentNullException(nameof(category));
            IsChecked = false;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return Name + " (" + Amount + ") " + Category;
        }
        #endregion
    }

    /// <Summary>
    /// Class used for displaying TravelItems grouped by Category in a ListView
    /// </Summary>
    class TravelItemGroup : IGrouping<string, TravelItem>
    {
        #region Properties
        public string Key { get; }
        private List<TravelItem> _travelItemGroup;
        #endregion

        #region Constructors
        public TravelItemGroup(string key, IEnumerable<TravelItem> items)
        {
            Key = key;
            _travelItemGroup = new List<TravelItem>(items);
        }
        #endregion

        #region Methods
        public IEnumerator<TravelItem> GetEnumerator() => _travelItemGroup.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _travelItemGroup.GetEnumerator();
        #endregion
    }
}
