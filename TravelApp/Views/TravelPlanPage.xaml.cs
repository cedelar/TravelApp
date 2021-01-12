
using System;
using TravelApp.ViewModels;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TravelPlanPage : Page
    {
        private TravelPlanViewModel _vm;


        public TravelPlanPage()
        {
            this.InitializeComponent();
        }

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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.OnEditTravelPlan();
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
            _vm.OnNewTravelPlan();
        }
    }
}
