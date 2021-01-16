using TravelApp.ViewModels.TravelPlanDetailsViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelApp.Views.TravelPlanDetailsPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskFrame : Page
    {
        private TaskFrameViewModel _vm;



        public TaskFrame()
        {
            this.InitializeComponent();
        }

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
        }

        private void CloseButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _vm.OnCloseClicked();
        }
    }
}
