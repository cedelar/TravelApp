using System;
using System.Collections.Generic;
using System.Linq;
using TravelApp.ViewModels;
using TravelApp.Views.TravelPlanDetailsPage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TravelApp.Views
{
    /// <summary>
    /// Code behind class for the NavigationView Wrapperpage
    /// </summary>
    public sealed partial class NavWrapperPage : Page
    {
        #region Properties
        private NavWrapperViewModel _vm;

        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("ItemTab", typeof(ItemFrame)),
            ("TaskTab", typeof(TaskFrame)),
            ("RoutesTab", typeof(RouteFrame))
        };
        #endregion

        #region Constructors
        public NavWrapperPage()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Methods
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _vm = new NavWrapperViewModel((Models.TravelPlanToNavViewNavigationEventArgs) e.Parameter);
            base.OnNavigatedTo(e);
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigated += On_Navigated;
            NavView.SelectedItem = NavView.MenuItems[0];
            NavView_Navigate("ItemTab", new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            
            if (args.IsSettingsInvoked == true)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Name.ToString();
                if(navItemTag == "BackTab")
                {
                    this.Frame.Navigate(typeof(TravelPlanPage), _vm.GetArgs(typeof(TravelPlanPage)));
                }else if(navItemTag == "LogoutTab")
                {
                    this.Frame.Navigate(typeof(StartPage), null);
                }
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void NavView_Navigate(
            string navItemTag,
            Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo)
        {
            Type _page = null;
            if (navItemTag == "settings")
            {
                _page = typeof(SettingsFrame);
            }
            else
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                _page = item.Page;
            }

            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if (!(_page is null) && !Type.Equals(preNavPageType, _page))
            {
                ContentFrame.Navigate(_page, _vm.GetArgs(_page), transitionInfo);
            }
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(SettingsFrame))
            {
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
                NavView.Header = "Settings";
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Name.Equals(item.Tag));

                NavView.Header =
                    ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            On_BackRequested();
        }

        private bool On_BackRequested()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            if (NavView.IsPaneOpen &&
                (NavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }
        #endregion
    }
}
