
using System;
using TravelApp.ViewModels;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TravelApp.Views
{
    /// <summary>
    /// Code behind class for the TravelPlanList page
    /// </summary>
    public sealed partial class TravelPlanPage : Page
    {
        #region Properties
        private TravelPlanViewModel _vm;
        #endregion

        #region Constructors
        public TravelPlanPage()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Methods
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _vm = new TravelPlanViewModel((Models.StartToTravelPlanNavigationEventArgs) e.Parameter);

            _vm.NavigateToNavViewWrapperPage += (s, ea) =>
            {
                this.Frame.Navigate(typeof(NavWrapperPage), ea);
            };
            base.OnNavigatedTo(e);
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.OnSelectTravelPlan();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Are you sure you want to delete " + _vm.SelectedItem.Name + "?");
            dialog.Commands.Add(new UICommand("Yes", null));
            dialog.Commands.Add(new UICommand("No", null));
            dialog.DefaultCommandIndex = 1;
            dialog.CancelCommandIndex = 0;
            var cmd = await dialog.ShowAsync();

            if (cmd.Label == "Yes")
            {
                _vm.OnDeleteTravelPlan();
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.OnNewTravelPlan(
                nameInput.Text, 
                startDatePicker.Date.DateTime, 
                endDatePicker.Date.DateTime, 
                destinationInput.Text
                );

            //reset inputfields
            nameInput.Text = "";
            startDatePicker.SelectedDate = null;
            endDatePicker.SelectedDate = null;
            destinationInput.Text = "";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.OnNewPlanCloseButton();
        }
        #endregion
    }
}
