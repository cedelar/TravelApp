using TravelApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace TravelApp.Views
{
    /// <summary>
    /// Code behind class for the Login page
    /// </summary>
    public sealed partial class StartPage : Page
    {
        #region Properties
        private StartPageViewModel _vm;
        #endregion

        #region Constructors
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
        #endregion

        #region Methods
        private void LoginButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _vm.OnLogin(nameInput.Text, pwdInput.Password);
        }

        private void AddButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _vm.OnAddUser(newNameInput.Text, newPasswordInput.Text);
            newNameInput.Text = "";
            newPasswordInput.Text = "";
        }
        #endregion
    }
}
