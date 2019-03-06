using Think.WinStore.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.DataTransfer;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace Think.WinStore
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class GroupedItemsPage : Think.WinStore.Common.LayoutAwarePage
    {
        public bool load = false;



        public GroupedItemsPage()
        {
            this.InitializeComponent();

            // Register handler for DataRequested events for sharing
            if (App.Handler != null)
                DataTransferManager.GetForCurrentView().DataRequested -= App.Handler;

            App.Handler = new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(datatransferManager_DataRequested);
            DataTransferManager.GetForCurrentView().DataRequested += App.Handler;
        }


        void datatransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            args.Request.Data.Properties.Title = "Think";
            args.Request.Data.Properties.Description = App.loader.GetString("ShareAppMessageDescription");
            args.Request.Data.SetText(App.loader.GetString("ShareAppMessageContent"));
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var groups = ThinkDataSource.GetGroups((String)navigationParameter);
            this.DefaultViewModel["Groups"] = groups;
            this.DefaultViewModel["ThinkDataSource"] = ThinkDataSource.Instance;

            this.DefaultViewModel["BackgroundColor"] = App.Current.RequestedTheme == ApplicationTheme.Light ? App.Current.Resources["LightBackground"] as string : App.Current.Resources["DarkBackground"] as string;
            
            LoadingAsync();
        }


        public async void LoadingAsync()
        {

            progressRing.IsActive = true;

            while (ThinkDataSource.GetStatusLoad() == ThinkDataSource.Loading) await Task.Delay(100);

            progressRing.IsActive = false;


            if (ThinkDataSource.Instance.AllGroups.Count == 0)
            {
                VisualStateManager.GoToState(this, "AllHaveNoItems", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "AllHaveItems", true);
            }


            if (ThinkDataSource.GetStatusLoad() == ThinkDataSource.InternetError)
            {
                VisualStateManager.GoToState(this, "InternetError", true);
            }
            else if (ThinkDataSource.GetStatusLoad() == ThinkDataSource.XMLCorruptedError)
            {
                VisualStateManager.GoToState(this, "CorruptedXMLError", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "NoError", true);
            }

            while (groupedItemsViewSource.View == null) await Task.Delay(100);
            (zoomItemLandscape.ZoomedOutView as ListViewBase).ItemsSource = groupedItemsViewSource.View.CollectionGroups;
            (zoomItemPortrait.ZoomedOutView as ListViewBase).ItemsSource = groupedItemsViewSource.View.CollectionGroups;
             
        }

        /// <summary>
        /// Invoked when a group header is clicked.
        /// </summary>
        /// <param name="sender">The Button used as a group header for the selected group.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Header_Click(object sender, RoutedEventArgs e)
        {
            // Determine what group the Button instance represents
            var group = (sender as FrameworkElement).DataContext;

            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            this.Frame.Navigate(typeof(GroupDetailPage), ((QuoteGroup)group).UniqueId);
        }

        /// <summary>
        /// Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            string itemId;

            itemId = ((QuoteItem)e.ClickedItem).UniqueId;
           
            this.Frame.Navigate(typeof(ItemDetailPage), itemId);
        }
        
        private void ItemView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var i = e.AddedItems.First() as QuoteItem;
            i.SColor = QuoteItem.ORANGE;
            i.WSize++;
        }

        private void shuffleButton_Click(object sender, RoutedEventArgs e)
        {
            var items = new List<QuoteItem>();

            Random r = new Random();
            int randInt = r.Next(0, ThinkDataSource.Instance.QuoteList.Count);
            
            var itemId = ThinkDataSource.Instance.QuoteList.ElementAt(randInt).UniqueId;

            this.Frame.Navigate(typeof(ItemDetailPage), itemId);
        }

        private async void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            syncButton.IsEnabled = false;
            ThinkDataSource.StartLoadingStatus();
            LoadingAsync();
            await ThinkDataSource.Instance.LoadAllRemoteDataAsync();
            ThinkDataSource.SaveLocalDataAsync();
            ThinkDataSource.StopLoadingStatus();
            syncButton.IsEnabled = true;
        }
        private async void syncButton_Click(object sender, RoutedEventArgs e)
        {
            syncButton.IsEnabled = false;
            ThinkDataSource.StartLoadingStatus();
            LoadingAsync();
            await ThinkDataSource.Instance.LoadOneRemoteDataAsync();
            ThinkDataSource.SaveLocalDataAsync();
            ThinkDataSource.StopLoadingStatus();
            syncButton.IsEnabled = true;
        }
        private async void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            syncButton.IsEnabled = false;
            ThinkDataSource.StartLoadingStatus();
            LoadingAsync();
            await ThinkDataSource.Instance.LoadNewRemoteDataAsync();
            ThinkDataSource.SaveLocalDataAsync();
            ThinkDataSource.StopLoadingStatus();
            syncButton.IsEnabled = true;
        }

    }
}
