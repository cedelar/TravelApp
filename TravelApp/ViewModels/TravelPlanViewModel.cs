using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using TravelApp.Models;
using ViewModel;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace TravelApp.ViewModels
{
    /// <Summary>
    /// View model class for the TravelplanList page
    /// </Summary>
    class TravelPlanViewModel : ComputedBindableBase
    {
        #region Properties
        private const string BASE_URL = "http://localhost:51758/api/";

        private User _user;

        public string UserName
        {
            get
            {
                return _user.UserName;
            }
            set
            {
                _user.UserName = value;
                RaisePropertyChanged("UserName");
            }
        }

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

        private bool _showNewTravelPlanFields;
        public bool ShowNewTravelPlanFields
        {
            get
            {
                return _showNewTravelPlanFields;
            }
            set
            {
                Set(ref _showNewTravelPlanFields, value);
            }
        }

        private bool  _isLoading;
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

        public ObservableCollection<TravelPlan> Travelplans
        {
            get
            {
                return _user.Travelplans;
            }
        }

        private TravelPlan _selectedItem;
        public TravelPlan SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if(_selectedItem != value)
                {
                    Set(ref _selectedItem, value);
                }
            }
        }

        [PropertySource(nameof(SelectedItem))]
        public bool ControlButtonsVisible => _selectedItem != null;

        public event EventHandler<TravelPlanToNavViewNavigationEventArgs> NavigateToNavViewWrapperPage;
        #endregion

        #region Constructors
        public TravelPlanViewModel(StartToTravelPlanNavigationEventArgs args)
        {
            _user = args.User;
            ResetMessage();
        }
        #endregion

        #region Methods
        private void ResetMessage()
        {
            Message = "";
        }

        public async void OnNewTravelPlan(string newName, DateTime newStartDate, DateTime newEndDate, string newDestination)
        {
            ResetMessage();
            if (ShowNewTravelPlanFields)
            {
                if (string.IsNullOrEmpty(newName))
                {
                    Message = "A name is required, try again.";
                    return;
                }

                IsLoading = true;
                Message = "Processing, please wait.";
                DateTime newStartDateRef = newStartDate.Date == new DateTime(1601,1,1).Date ? new DateTime() : newStartDate;
                DateTime newEndDateRef = newEndDate.Date == new DateTime(1601, 1, 1).Date ? new DateTime() : newEndDate;
                string newDestinationRef = string.IsNullOrEmpty(newDestination) ? "-" : newDestination;
                TravelPlan newTravelPlan = new TravelPlan(newName, newStartDateRef, newEndDateRef, newDestinationRef);

                //Restcall
                try
                {
                    HttpClient httpClient = new HttpClient();
                    Uri uri = new Uri(BASE_URL + "User/addTravelPlan/" + UserName);

                    HttpStringContent content = new HttpStringContent(
                        JsonConvert.SerializeObject(newTravelPlan),
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
                        _user.AddTravelPlan(newTravelPlan);
                        RaisePropertyChanged("TravelPlans");
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }

                ShowNewTravelPlanFields = false;
                IsLoading = false;
            }
            else
            {
                ShowNewTravelPlanFields = true;
            }
        }

        public async void OnDeleteTravelPlan()
        {
            ResetMessage();
            if(SelectedItem == null)
            {
                Message = "Please select an item before deleting.";
                return;
            }

            //Restcall
            try
            {
                IsLoading = true;
                Message = "Processing, please wait.";
                HttpClient httpClient = new HttpClient();
                Uri uri = new Uri(BASE_URL + "User/deleteTravelPlan/" + UserName + "/" + SelectedItem.Name);

                _user.RemoveTravelPlan(SelectedItem);
                RaisePropertyChanged("TravelPlans");

                HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(uri);

                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                Message = httpResponseBody;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            finally
            {
                SelectedItem = null;
            }
            IsLoading = false;
        }

        public void OnSelectTravelPlan()
        {
            ResetMessage();
            if (_selectedItem != null)
            {
                TravelPlanToNavViewNavigationEventArgs args = new TravelPlanToNavViewNavigationEventArgs
                {
                    User = _user,
                    SelectedTravelPlanName = _selectedItem.Name
                };
                NavigateToNavViewWrapperPage?.Invoke(this, args);
            }
        }

        public void OnNewPlanCloseButton()
        {
            ShowNewTravelPlanFields = false;
        }
        #endregion
    }
}
