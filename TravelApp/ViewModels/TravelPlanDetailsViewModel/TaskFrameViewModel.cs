using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models;
using ViewModel;

namespace TravelApp.ViewModels.TravelPlanDetailsViewModel
{
    class TaskFrameViewModel : ComputedBindableBase
    {
        private ObservableCollection<TravelTask> _taskList;

        public ObservableCollection<TravelTask> TaskList
        {
            get
            {
                return _taskList;
            }
            set
            {
                Set(ref _taskList, value);
                RaisePropertyChanged("SortedTaskList");
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
                return _taskList;
            }
        }

        public TaskFrameViewModel(NavViewTasksEventArgs e)
        {
            _taskList = e.Tasks;
            SelectedBoxValue = "Nothing";
        }

        public void SelectionChanged(string input)
        {
            SelectedBoxValue = input;
        }
    }
}

