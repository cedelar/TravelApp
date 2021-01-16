using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelApp.ViewModels.TravelPlanDetailsViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelApp.Views.TravelPlanDetailsPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ItemFrame : Page
    {
        private ItemFrameViewModel _vm;
        public ItemFrame()
        {
            this.InitializeComponent();
        }

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
    }
}
