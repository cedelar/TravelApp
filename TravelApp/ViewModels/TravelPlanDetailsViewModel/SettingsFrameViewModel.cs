using Newtonsoft.Json;
using System;
using TravelApp.Models;
using ViewModel;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace TravelApp.ViewModels.TravelPlanDetailsViewModel
{
    /// <Summary>
    /// View model class for the Settings Frame
    /// </Summary>
    class SettingsFrameViewModel : ComputedBindableBase
    {
        #region Properties
        private const string BASE_URL = "http://localhost:51758/api/";

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
        #endregion

        #region Constructors
        public SettingsFrameViewModel(NavViewNavigationEventArgs e)
        {
            UserName = e.Username;
            TravelPlan = e.TravelPlan;
            IsLoading = false;
            ResetMessage();
        }
        #endregion

        #region Methods
        private void ResetMessage()
        {
            Message = "";
        }

        public async void OnEditButton(DateTime newStartDate, DateTime newEndDate, string newDestination)
        {
            ResetMessage();

            IsLoading = true;
            Message = "Processing, please wait.";
            DateTime newStartDateRef = newStartDate.Date == new DateTime(1601, 1, 1).Date ? TravelPlan.StartDate : newStartDate;
            DateTime newEndDateRef = newEndDate.Date == new DateTime(1601, 1, 1).Date ? TravelPlan.EndDate : newEndDate;
            string newDestinationRef = string.IsNullOrEmpty(newDestination) ? TravelPlan.Destination : newDestination;
            TravelPlan editTravelPlan = new TravelPlan(TravelPlan.Name, newStartDateRef, newEndDateRef, newDestinationRef);

            //REST
            try
            {
                HttpClient httpClient = new HttpClient();
                Uri uri = new Uri(BASE_URL + "User/editTravelPlan/" + UserName);

                HttpStringContent content = new HttpStringContent(
                    JsonConvert.SerializeObject(editTravelPlan),
                    UnicodeEncoding.Utf8,
                    "application/json");

                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(
                    uri,
                    content);

                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                Message = httpResponseBody;

                if (httpResponseBody.Split(" ")[0] != "Error:")
                {
                    if (TravelPlan.StartDate.Date != editTravelPlan.StartDate.Date)
                    {
                        TravelPlan.StartDate = editTravelPlan.StartDate;
                    }
                    if (TravelPlan.EndDate.Date != editTravelPlan.EndDate.Date)
                    {
                        TravelPlan.EndDate = editTravelPlan.EndDate;
                    }
                    if (TravelPlan.Destination != editTravelPlan.Destination)
                    {
                        TravelPlan.Destination = editTravelPlan.Destination;
                    }
                    RaisePropertyChanged("TravelPlan");
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            IsLoading = false;
        }
        #endregion
    }
}
