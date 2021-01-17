using TravelApp.ViewModels.TravelPlanDetailsViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TravelApp.Views.TravelPlanDetailsPage
{
    /// <summary>
    /// Code behind class for the ItemList Frame
    /// </summary>
    public sealed partial class ItemFrame : Page
    {
        #region Properties
        private ItemFrameViewModel _vm;
        #endregion

        #region Constructors
        public ItemFrame()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Methods
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _vm = new ItemFrameViewModel((Models.NavViewNavigationEventArgs)e.Parameter);
            base.OnNavigatedTo(e);
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.OnNewClicked(nameInput.Text, (int) amountInput.Value, categoryInput.Text);
            nameInput.Text = "";
            amountInput.Value = 0.0;
            categoryInput.Text = "";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.OnCloseClicked();
        }
        #endregion
    }
}
