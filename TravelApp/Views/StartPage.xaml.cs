using System;
using System.ComponentModel;
using TravelApp.ViewModels;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
        private StartPageViewModel _vm;

        private StartPageViewModel Vm
        {
            get
            {
                return _vm;
            }
        }

        public StartPage()
        {
            this.InitializeComponent();
            _vm = new StartPageViewModel();

            //Navigationhandler
            _vm.NavigateToTravelPlanPage += (s, e) =>
            {
                this.Frame.Navigate(typeof(TravelPlanPage), e);
            };
        }

        private void LoginButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Vm.OnLogin(nameInput.Text, pwdInput.Password);
        }

        private void AddButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Vm.onAddUser();
        }
    }
}
