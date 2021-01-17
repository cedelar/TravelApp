using TravelApp.ViewModels.TravelPlanDetailsViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TravelApp.Views.TravelPlanDetailsPage
{
    /// <summary>
    /// Code behind class for the TaskList Frame
    /// </summary>
    public sealed partial class TaskFrame : Page
    {
        #region Properties
        private TaskFrameViewModel _vm;
        #endregion

        #region Constructors
        public TaskFrame()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Methods
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _vm = new TaskFrameViewModel((Models.NavViewNavigationEventArgs) e.Parameter);
            base.OnNavigatedTo(e);
        }

        private void OrderBySelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.SelectionChanged(e.AddedItems[0].ToString());
        }

        private void NewButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _vm.OnNewClicked(
                nameInput.Text, 
                priorityInput.SelectedValue == null ? "" : priorityInput.SelectedValue.ToString(), 
                descriptionInput.Text
                );

            //reset
            nameInput.Text = "";
            descriptionInput.Text = "";
        }

        private void CloseButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _vm.OnCloseClicked();
        }
        #endregion
    }
}
