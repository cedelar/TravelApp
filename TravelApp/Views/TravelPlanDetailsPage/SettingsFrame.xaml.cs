using TravelApp.ViewModels.TravelPlanDetailsViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TravelApp.Views.TravelPlanDetailsPage
{
    /// <summary>
    /// Code behind class for the Settings Frame
    /// </summary>
    public sealed partial class SettingsFrame : Page
    {
        #region Properties
        private SettingsFrameViewModel _vm;
        #endregion

        #region Constructors
        public SettingsFrame()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Methods
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _vm = new SettingsFrameViewModel((Models.NavViewNavigationEventArgs)e.Parameter);
            base.OnNavigatedTo(e);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.OnEditButton(
                startDatePicker.Date.DateTime,
                endDatePicker.Date.DateTime,
                destinationInput.Text);

            //reset
            startDatePicker.SelectedDate = null;
            endDatePicker.SelectedDate = null;
            destinationInput.Text = "";
        }
        #endregion
    }
}
