using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Think.WinStore.Data;
using Think.WinStore.Common;
using System.Threading.Tasks;
using Windows.UI.ApplicationSettings;
using Windows.ApplicationModel.Resources;

// The Search Contract item template is documented at http://go.microsoft.com/fwlink/?LinkId=234240

namespace Think.WinStore
{
    // TODO: Respond to activation for search results
    //
    // The following code could not be automatically added to your application subclass,
    // either because the appropriate class could not be located or because a method with
    // the same name already exists.  Ensure that appropriate code deals with activation
    // by displaying search results for the specified search term.
    //
    // /// <summary>
    // /// Invoked when the application is activated to display search results.
    // /// </summary>
    // /// <param name="args">Details about the activation request.</param>
    // protected override void OnSearchActivated(Windows.ApplicationModel.Activation.SearchActivatedEventArgs args)
    // {
    //     Think_Win8.SearchResultsPage.Activate(args.QueryText, args.PreviousExecutionState);
    // }
    /// <summary>
    /// This page displays search results when a global search is directed to this application.
    /// </summary>
    public sealed partial class SearchResultsPage : Think.WinStore.Common.LayoutAwarePage
    {
        private UIElement _previousContent;
        private ApplicationExecutionState _previousExecutionState;

        ResourceLoader loader = new Windows.ApplicationModel.Resources.ResourceLoader();


    	// Collection of RecipeDataItem collections representing search results
        private Dictionary<string, List<QuoteItem>> _results = new Dictionary<string, List<QuoteItem>>();

        public SearchResultsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Determines how best to support navigation back to the previous application state.
        /// </summary>
        public static void Activate(String queryText, ApplicationExecutionState previousExecutionState)
        {
            var previousContent = Window.Current.Content;
            var frame = previousContent as Frame;

            if (frame != null)
            {
                // If the app is already running and uses top-level frame navigation we can just
                // navigate to the search results
                frame.Navigate(typeof(SearchResultsPage), queryText);
            }
            else
            {
                // Otherwise bypass navigation and provide the tools needed to emulate the back stack
                
                SearchResultsPage page = new SearchResultsPage();
                page._previousContent = previousContent;
                page._previousExecutionState = previousExecutionState;
                page.LoadState(queryText, null);
                Window.Current.Content = page;
                
            }

            // Either way, active the window
            Window.Current.Activate();
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
        protected async override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var queryText = navigationParameter as String;

            this.DefaultViewModel["QuoteDataSource"] = ThinkDataSource.Instance;
            this.DefaultViewModel["BackgroundColor"] = App.Current.RequestedTheme == ApplicationTheme.Light ? App.Current.Resources["LightBackground"] as string : App.Current.Resources["DarkBackground"] as string;

            // TODO: Application-specific searching logic.  The search process is responsible for
            //       creating a list of user-selectable result categories:
            //
            //       filterList.Add(new Filter("<filter name>", <result count>));
            //
            //       Only the first filter, typically "All", should pass true as a third argument in
            //       order to start in an active state.  Results for the active filter are provided
            //       in Filter_SelectionChanged below.

            var filterList = new List<Filter>();
            filterList.Add(new Filter(loader.GetString("SearchAllFilter"), 0, true));

           	// Search recipes and tabulate results
	        string query = queryText.ToLower();
	        var all = new List<QuoteItem>();
            _results.Add(loader.GetString("SearchAllFilter"), all);


            while (ThinkDataSource.GetStatusLoad()==0)
            {
                await Task.Delay(20);
            }
            
	        foreach (var group in ThinkDataSource.GetGroups("AllGroups") )
	        {
	            var items = new List<QuoteItem>();
	            _results.Add(group.Title, items);
	                
	            foreach (var item in group.Items)
	            {
	                if (item.Autor.ToLower().Contains(query) || item.Quote.ToLower().Contains(query))
	                {
	                    all.Add(item);
	                    items.Add(item);
	                }
	            }
	
	            filterList.Add(new Filter(group.Title, items.Count, false));
	        }
	
	        filterList[0].Count = all.Count;


            // Communicate results through the view model
            this.DefaultViewModel["QueryText"] = '\u201c' + queryText + '\u201d';
            this.DefaultViewModel["CanGoBack"] = this._previousContent != null;
            this.DefaultViewModel["Filters"] = filterList;
            this.DefaultViewModel["ShowFilters"] = filterList.Count > 1;
        }

        /// <summary>
        /// Invoked when the back button is pressed.
        /// </summary>
        /// <param name="sender">The Button instance representing the back button.</param>
        /// <param name="e">Event data describing how the button was clicked.</param>
        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            // Return the application to the state it was in before search results were requested
            if (this.Frame != null && this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
            else if (this._previousContent != null)
            {
                Window.Current.Content = this._previousContent;
            }
            else
            {
                // TODO: invoke the app's normal launch behavior, using this._previousExecutionState
                //       as appropriate.  Exact details can vary from app to app, which is why an
                //       implementation isn't included in the Search Contract template.  Typically
                //       this method and OnLaunched in App.xaml.cs can call a common method.                
                Frame rootFrame = new Frame();
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                if (!rootFrame.Navigate(typeof(GroupedItemsPage), "AllGroups"))
                {
                    throw new Exception("Failed to create initial page");
                }
                
                // Place the frame in the current Window and ensure that it is active
                Window.Current.Content = rootFrame;
                Window.Current.Activate();
                
                //SettingsPane.GetForCurrentView().CommandsRequested += (Application.Current as App).OnCommandsRequested;
            }
        }

        /// <summary>
        /// Invoked when a filter is selected using the ComboBox in snapped view state.
        /// </summary>
        /// <param name="sender">The ComboBox instance.</param>
        /// <param name="e">Event data describing how the selected filter was changed.</param>
        void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Determine what filter was selected
            var selectedFilter = e.AddedItems.FirstOrDefault() as Filter;
            if (selectedFilter != null)
            {
                // Mirror the results into the corresponding Filter object to allow the
                // RadioButton representation used when not snapped to reflect the change
                selectedFilter.Active = true;

                // TODO: Respond to the change in active filter by setting this.DefaultViewModel["Results"]
                //       to a collection of items with bindable Image, Title, Subtitle, and Description properties
                this.DefaultViewModel["Results"] = _results[selectedFilter.Name];

                // Ensure results are found
                object results;
                ICollection resultsCollection;
                if (this.DefaultViewModel.TryGetValue("Results", out results) &&
                    (resultsCollection = results as ICollection) != null &&
                    resultsCollection.Count != 0)
                {
                    VisualStateManager.GoToState(this, "ResultsFound", true);
                    return;
                }

            }

            // Display informational text when there are no search results.
            VisualStateManager.GoToState(this, "NoResultsFound", true);
        }

        /// <summary>
        /// Invoked when a filter is selected using a RadioButton when not snapped.
        /// </summary>
        /// <param name="sender">The selected RadioButton instance.</param>
        /// <param name="e">Event data describing how the RadioButton was selected.</param>
        void Filter_Checked(object sender, RoutedEventArgs e)
        {
            // Mirror the change into the CollectionViewSource used by the corresponding ComboBox
            // to ensure that the change is reflected when snapped
            if (filtersViewSource.View != null)
            {
                var filter = (sender as FrameworkElement).DataContext;
                filtersViewSource.View.MoveCurrentTo(filter);
            }
        }

        /// <summary>
        /// View model describing one of the filters available for viewing search results.
        /// </summary>
        private sealed class Filter : Think.WinStore.Common.BindableBase
        {
            private String _name;
            private int _count;
            private bool _active;

            public Filter(String name, int count, bool active = false)
            {
                this.Name = name;
                this.Count = count;
                this.Active = active;
            }

            public override String ToString()
            {
                return Description;
            }

            public String Name
            {
                get { return _name; }
                set { if (this.SetProperty(ref _name, value)) this.OnPropertyChanged("Description"); }
            }

            public int Count
            {
                get { return _count; }
                set { if (this.SetProperty(ref _count, value)) this.OnPropertyChanged("Description"); }
            }

            public bool Active
            {
                get { return _active; }
                set { this.SetProperty(ref _active, value); }
            }

            public String Description
            {
                get { return String.Format("{0} ({1})", _name, _count); }
            }
        }


        private void OnItemClick(object sender, ItemClickEventArgs e)
    	{
            // Navigate to the page showing the recipe that was clicked
            string itemId;

            itemId = ((QuoteItem)e.ClickedItem).UniqueId;
            


            //Inicializado pelo Search Charm
            if (this.Frame == null)
            {
                Frame rootFrame = new Frame();
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                App.startArguments = itemId;

                if (!rootFrame.Navigate(typeof(GroupedItemsPage), "AllGroups"))
                {
                    throw new Exception("Failed to create initial page");
                }
                
                // Place the frame in the current Window and ensure that it is active
                Window.Current.Content = rootFrame;
                Window.Current.Activate();
            }
            else
            {   //Inicializado na app
                this.Frame.Navigate(typeof(ItemDetailPage), itemId);
            }
    	}

    }
}
