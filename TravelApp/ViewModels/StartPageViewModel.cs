using Newtonsoft.Json;
using System;
using TravelApp.Models;
using ViewModel;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace TravelApp.ViewModels
{
    /// <Summary>
    /// View model class for the Login page
    /// </Summary>
    class StartPageViewModel : ComputedBindableBase
    {
        #region Properties
        private const string BASE_URL = "http://localhost:51758/api/";

        public event EventHandler<StartToTravelPlanNavigationEventArgs> NavigateToTravelPlanPage;

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

        private bool _showNewUserFields;
        public bool ShowNewUserFields
        {
            get
            {
                return _showNewUserFields;
            }
            set
            {
                Set(ref _showNewUserFields, value);
            }
        }
        #endregion

        #region Constructors
        public StartPageViewModel()
        {
            ShowNewUserFields = false;
            IsLoading = false;
            ResetMessage();
        }
        #endregion

        #region Methods
        private void ResetMessage()
        {
            Message = "";
        }

        public async void OnLogin(string username, string password)
        {
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Message = "Please enter a username and a password before logging in.";
                return;
            }
            IsLoading = true;
            Message = "Loading, please wait.";

            //REST
            var uri = new System.Uri(BASE_URL + "User/getByName/" + username + "/" + password);
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var result = await httpClient.GetStringAsync(uri);
                    User user = JsonConvert.DeserializeObject<User>(result);
                    if (user.UserName != "NOK")
                    {
                        StartToTravelPlanNavigationEventArgs args = new StartToTravelPlanNavigationEventArgs();
                        args.User = user;
                        NavigateToTravelPlanPage?.Invoke(this, args);
                    }
                    else
                    {
                        //Login failed
                        Message = "Incorrect logincredentials, try again (" + user.Password + ")" ;
                    }
                }
                catch (Exception ex)
                {
                    Message = "Error: " + ex.Message;
                }
            }
            IsLoading = false;
        }

        public async void OnAddUser(string newUsername, string newPassword)
        {
            if (!ShowNewUserFields)
            {
                ShowNewUserFields = true;
            }
            else
            {
                Message = "Communicating with server, please wait.";
                IsLoading = true;
                ShowNewUserFields = false;
                if(string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword))
                {
                    Message = "Neither Username nor password of a new account can be empty! Try again.";
                }
                else
                {
                    //Restcall
                    try
                    {
                        HttpClient httpClient = new HttpClient();
                        Uri uri = new Uri(BASE_URL + "User/addUser");
                        string Jsoncontent = "{ \"Username\": \"" + newUsername + "\"" + "" +
                            ",\"Password\":\"" + newPassword + "\" }";

                        HttpStringContent content = new HttpStringContent(
                            Jsoncontent,
                            UnicodeEncoding.Utf8,
                            "application/json");

                        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(
                            uri,
                            content);

                        httpResponseMessage.EnsureSuccessStatusCode();
                        var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                        Message = httpResponseBody;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.Message;
                    }
                }
            }
            IsLoading = false;
        }
        #endregion
    }
}
