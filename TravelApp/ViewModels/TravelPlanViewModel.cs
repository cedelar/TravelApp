using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TravelApp.Models;
using ViewModel;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace TravelApp.ViewModels
{
    class TravelPlanViewModel : ComputedBindableBase
    {
        #region properties
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

        public TravelPlanViewModel(StartToTravelPlanNavigationEventArgs args)
        {
            _user = args.User;
            ResetMessage();

        }

        private void ResetMessage()
        {
            Message = "";
        }

        public async void OnNewTravelPlan(string newName, DateTime newStartDate, DateTime newEndDate, string newDestination)
        {
            ResetMessage();
            if (ShowNewTravelPlanFields)
            {
                //Add new plan
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
                    // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                    HttpClient httpClient = new HttpClient();
                    Uri uri = new Uri(BASE_URL + "User/addTravelPlan/" + UserName);


                    // Construct the JSON to post.
                    HttpStringContent content = new HttpStringContent(
                        JsonConvert.SerializeObject(newTravelPlan),
                        UnicodeEncoding.Utf8,
                        "application/json");

                    // Post the JSON and wait for a response.
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(
                        uri,
                        content);

                    // Make sure the post succeeded, and write out the response.
                    httpResponseMessage.EnsureSuccessStatusCode();
                    var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                    Message = httpResponseBody;
                    if (httpResponseBody.Split(" ")[0] != "Error:")
                    {
                        _user.addTravelPlan(newTravelPlan);
                        RaisePropertyChanged("TravelPlans");
                    }
                }
                catch (Exception ex)
                {
                    // Write out any exceptions.
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

        public void OnEditTravelPlan()
        {
            //TODO
            ResetMessage();
            UserName = "Edit " + SelectedItem.Name;
        }

        public async void OnDeleteTravelPlan()
        {
            //TODO
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
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                HttpClient httpClient = new HttpClient();
                Uri uri = new Uri(BASE_URL + "User/deleteTravelPlan/" + UserName + "/" + SelectedItem.Name);

                _user.RemoveTravelPlan(SelectedItem);
                RaisePropertyChanged("TravelPlans");

                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(uri);

                // Make sure the post succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                Message = httpResponseBody;
            }
            catch (Exception ex)
            {
                // Write out any exceptions.
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
    }
}
