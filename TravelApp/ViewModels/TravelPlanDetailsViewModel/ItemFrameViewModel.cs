using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TravelApp.Models;
using ViewModel;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace TravelApp.ViewModels.TravelPlanDetailsViewModel
{
    /// <Summary>
    /// View model class for the ItemList Frame
    /// </Summary>
    class ItemFrameViewModel : ComputedBindableBase
    {
        #region Properties
        private const string BASE_URL = "http://localhost:51758/api/";

        public ObservableCollection<TravelItem> ItemList
        {
            get
            {
                return TravelPlan.ItemList;
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
                return new List<TravelItem>(TravelPlan.ItemList)
                    .GroupBy(i => i.Category, (key, list) => new TravelItemGroup(key, list));
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                Set(ref _isLoading, value);
            }
        }

        [PropertySource(nameof(IsLoading))]
        public bool ButtonsEnabled => !IsLoading;

        [PropertySource(nameof(SelectedItem))]
        public bool ControlButtonsVisible => _selectedItem != null;

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                Set(ref _message, value);
            }
        }

        private bool _showNewItemFields;
        public bool ShowNewItemFields
        {
            get
            {
                return _showNewItemFields;
            }
            set
            {
                Set(ref _showNewItemFields, value);
            }
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                Set(ref _userName, value);
            }
        }

        private TravelPlan _travelPlan;
        public TravelPlan TravelPlan
        {
            get
            {
                return _travelPlan;
            }
            set
            {
                Set(ref _travelPlan, value);
            }
        }
        #endregion

        #region Constructors
        public ItemFrameViewModel(NavViewNavigationEventArgs e)
        {
            UserName = e.Username;
            TravelPlan = e.TravelPlan;
            IsLoading = false;
            ResetMessage();
            ShowNewItemFields = false;
            
        }
        #endregion

        #region Methods
        private void ResetMessage()
        {
            Message = "";
        }

        public async void OnNewClicked(string newname, int newAmount, string newCategory)
        {
            if (!ShowNewItemFields)
            {
                ShowNewItemFields = true;
            }
            else
            {
                if (string.IsNullOrEmpty(newname))
                {
                    Message = "A name is required, try again.";
                    return;
                }

                IsLoading = true;
                Message = "Processing, please wait.";
                int newAmountRef = newAmount < 1 ? 1 : newAmount;
                string newCategoryRef = string.IsNullOrEmpty(newCategory) ? "No Category" : newCategory;
                TravelItem newItem = new TravelItem(newname, newAmountRef, newCategoryRef);

                //Restcall
                try
                {
                    HttpClient httpClient = new HttpClient();
                    Uri uri = new Uri(BASE_URL + "User/addTravelItem/" + UserName + "/" + TravelPlan.Name);

                    HttpStringContent content = new HttpStringContent(
                        JsonConvert.SerializeObject(newItem),
                        UnicodeEncoding.Utf8,
                        "application/json");

                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(
                        uri,
                        content);

                    httpResponseMessage.EnsureSuccessStatusCode();
                    var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                    Message = httpResponseBody;

                    if (httpResponseBody.Split(" ")[0] != "Error:")
                    {
                        TravelPlan.AddTravelItem(newItem);
                        RaisePropertyChanged("ItemList");
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }

                ShowNewItemFields = false;
                IsLoading = false;
            }
        }

        public void OnCloseClicked()
        {
            ShowNewItemFields = false;
        }
        #endregion
    }
}
