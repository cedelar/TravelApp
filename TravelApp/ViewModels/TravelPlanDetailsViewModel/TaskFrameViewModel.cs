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
    /// View model class for the TaskList Frame
    /// </Summary>
    class TaskFrameViewModel : ComputedBindableBase
    {
        #region Properties
        private const string BASE_URL = "http://localhost:51758/api/";

        public ObservableCollection<TravelTask> TaskList
        {
            get
            {
                return TravelPlan.TaskList;
            }
        }

        private string _selectedBoxValue;
        public string SelectedBoxValue
        {
            get 
            { 
                return _selectedBoxValue; 
            }
            set 
            {
                Set(ref _selectedBoxValue, value);
            }
        }

        private TravelTask _selectedItem;
        public TravelTask SelectedItem
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

        [PropertySource(nameof(SelectedBoxValue))]
        public ObservableCollection<TravelTask> SortedTaskList
        {
            get
            {
                switch (SelectedBoxValue)
                {
                    case "Priority":
                        return new ObservableCollection<TravelTask>(new List<TravelTask>(TaskList).OrderByDescending(a => a.Priority));
                    case "Name":
                        return new ObservableCollection<TravelTask>(new List<TravelTask>(TaskList).OrderBy(a => a.Name));
                }
                return TaskList;
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

        private bool _showNewTaskFields;
        public bool ShowNewTaskFields
        {
            get
            {
                return _showNewTaskFields;
            }
            set
            {
                Set(ref _showNewTaskFields, value);
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
        public TaskFrameViewModel(NavViewNavigationEventArgs e)
        {
            UserName = e.Username;
            TravelPlan = e.TravelPlan;
            IsLoading = false;
            ResetMessage();
            ShowNewTaskFields = false;
            SelectedBoxValue = "Nothing";
        }
        #endregion

        #region Methods
        private void ResetMessage()
        {
            Message = "";
        }

        public void SelectionChanged(string input)
        {
            SelectedBoxValue = input;
        }

        public async void OnNewClicked(string newName, string newPriority, string newDescription)
        {
            if (!ShowNewTaskFields)
            {
                ShowNewTaskFields = true;
            }
            else
            {
                if (string.IsNullOrEmpty(newName))
                {
                    Message = "A name is required, try again.";
                    return;
                }

                IsLoading = true;
                Message = "Processing, please wait.";
                string newDescriptionRef = string.IsNullOrEmpty(newDescription) ? "No Description" : newDescription;
                Priority newPriorityRef;
                switch (newPriority)
                {
                    case "Low": 
                        newPriorityRef = Priority.Low;
                        break;
                    case "Normal":
                        newPriorityRef = Priority.Normal;
                        break;
                    case "High":
                        newPriorityRef = Priority.High;
                        break;
                    case "Very High":
                        newPriorityRef = Priority.VeryHigh;
                        break;
                    default:
                        newPriorityRef = Priority.Normal;
                        break;
                }
                NetworkTravelTask newTask = new NetworkTravelTask(newName, newPriorityRef, newDescriptionRef);

                //Restcall
                try
                {
                    HttpClient httpClient = new HttpClient();
                    Uri uri = new Uri(BASE_URL + "User/addTravelTask/" + UserName + "/" + TravelPlan.Name);

                    HttpStringContent content = new HttpStringContent(
                        JsonConvert.SerializeObject(newTask),
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
                        TravelPlan.AddTravelTask(new TravelTask(newTask.Name, newTask.Priority, newTask.Description));
                        RaisePropertyChanged("TaskList");
                        RaisePropertyChanged("SortedTaskList");
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }

                ShowNewTaskFields = false;
                IsLoading = false;
            }
        }

        public void OnCloseClicked()
        {
            ShowNewTaskFields = false;
        }
        #endregion
    }
}

