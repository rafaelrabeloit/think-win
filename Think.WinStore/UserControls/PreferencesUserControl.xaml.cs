using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Think.WinStore.Data;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ApplicationSettings;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Think.WinStore.UserControls
{
    public sealed partial class PreferencesUserControl : UserControl
    {
        bool loading;
                   

        public PreferencesUserControl()
        {
            
            this.InitializeComponent();

            string back = App.Current.RequestedTheme == ApplicationTheme.Light ? App.Current.Resources["LightBackground"] as string : App.Current.Resources["DarkBackground"] as string;
            //gridContent.Background = new SolidColorBrush( App.GetColorFromHexString(back) );



            List<string> lstUpdate = new List<string>();
            lstUpdate.Add(App.loader.GetString("HalfHour"));
            lstUpdate.Add(App.loader.GetString("Hour"));
            lstUpdate.Add(App.loader.GetString("SixHour"));
            lstUpdate.Add(App.loader.GetString("TwelveHour"));
            lstUpdate.Add(App.loader.GetString("Daily"));

            changeUpdate.ItemsSource = lstUpdate;

            

            List<string> lstTheme = new List<string>();
            lstTheme.Add(App.loader.GetString("Light"));
            lstTheme.Add(App.loader.GetString("Dark"));

            changeTheme.ItemsSource = lstTheme;


            List<string> lstLanguage = new List<string>();
            //changeLanguage.ItemsSource = App.ReturnSuportedLanguages();


            List<string> lstColors = new List<string>();
            lstColors.Add(App.loader.GetString("Blue"));
            lstColors.Add(App.loader.GetString("Green"));
            lstColors.Add(App.loader.GetString("Orange"));
            lstColors.Add(App.loader.GetString("Purple"));
            lstColors.Add(App.loader.GetString("Red"));
            lstColors.Add(App.loader.GetString("Yellow"));

            changeColor.ItemsSource = lstColors;


            loading = true;
            /*
            changeTheme.SelectedIndex = (int)App.ReturnUserTheme();
            changeColor.SelectedIndex = App.ReturnQuoteColorIndice();
            changeLanguage.SelectedIndex = App.ReturnLanguagesIndice();
            changeUpdate.SelectedIndex = App.returnPeriodicUpdateRecurrence();
            UpdateToggle.IsOn = App.isTilePollingStarted();
             * */
            loading = false;
        }

	    private void OnBackButtonClicked(object sender, RoutedEventArgs e)
	    {
	        if (this.Parent.GetType() == typeof(Popup))
	        {
	            ((Popup)this.Parent).IsOpen = false;
	        }
	        SettingsPane.Show();
	    }

        private void UpdateToggle_Toggled(object sender, RoutedEventArgs e)
        {
            if (loading) return;

            if (!(sender as ToggleSwitch).IsOn)
            {
                //App.stopTilePolling();

                if(messageFeedback != null)
                    messageFeedback.Text = "Update stoped.";
            }
            else
            {
                /*
                switch (changeUpdate.SelectedIndex)
                {
                    case 0: App.changeTilePolling(PeriodicUpdateRecurrence.HalfHour);
                        break;
                    case 1: App.changeTilePolling(PeriodicUpdateRecurrence.Hour);
                        break;
                    case 2: App.changeTilePolling(PeriodicUpdateRecurrence.SixHours);
                        break;
                    case 3: App.changeTilePolling(PeriodicUpdateRecurrence.TwelveHours);
                        break;
                    case 4: App.changeTilePolling(PeriodicUpdateRecurrence.Daily);
                        break;
                }
                */

                if (messageFeedback != null)
                    messageFeedback.Text = "Update started.";
            }
        }

        private void clearAllQuotes_Click(object sender, RoutedEventArgs e)
        {
            /*
            ThinkDataSource.Instance.AllGroups.Clear();
            ThinkDataSource.Instance.QuoteList.Clear();

            ThinkDataSource.SaveLocalDataAsync();
            */

            (Window.Current.Content as Frame).Navigate(typeof(GroupedItemsPage), "AllGroups");
        }

        private void clearRecents_Click(object sender, RoutedEventArgs e)
        {
            //ThinkDataSource.Instance.ClearAllRecentGroup();
        }

        private void changeUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loading) return;
            /*
            switch (changeUpdate.SelectedIndex)
            {
                case 0: App.changeTilePolling(PeriodicUpdateRecurrence.HalfHour);
                    break;
                case 1: App.changeTilePolling(PeriodicUpdateRecurrence.Hour);
                    break;
                case 2: App.changeTilePolling(PeriodicUpdateRecurrence.SixHours);
                    break;
                case 3: App.changeTilePolling(PeriodicUpdateRecurrence.TwelveHours);
                    break;
                case 4: App.changeTilePolling(PeriodicUpdateRecurrence.Daily);
                    break;
            }
            */
            loading = true;
            UpdateToggle.IsOn = true;
            loading = false;
        }

        private void changeTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loading) return;
            
            /*
            switch (changeTheme.SelectedIndex)
            {
                case 0: App.SetUserTheme(ApplicationTheme.Light);
                    break;
                case 1: App.SetUserTheme(ApplicationTheme.Dark);
                    break;
            }
            */

            var str = App.loader.GetString("PreferencesErrorRestart");
            messageFeedback.Text = str;
        }

        private void changeLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loading) return;

            /*
            App.SetLanguage(changeLanguage.SelectedItem as string);
            App.changeTilePolling(App.returnPeriodicUpdateRecurrence());
            */

            loading = true;
            UpdateToggle.IsOn = true;
            loading = false;
        }

        private void changeColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loading) return;

            /*
            switch (changeColor.SelectedIndex)
            {
                case 0: App.SetQuoteColor(QuoteItem.BLUE);
                    break;
                case 1: App.SetQuoteColor(QuoteItem.GREEN);
                    break;
                case 2: App.SetQuoteColor(QuoteItem.ORANGE);
                    break;
                case 3: App.SetQuoteColor(QuoteItem.PURPLE);
                    break;
                case 4: App.SetQuoteColor(QuoteItem.RED);
                    break;
                case 5: App.SetQuoteColor(QuoteItem.YELLOW);
                    break;
            }
            */
        }

        private void applyToAll_Click(object sender, RoutedEventArgs e)
        {
            /*
            foreach (var item in ThinkDataSource.Instance.QuoteList)
            {
                item.SColor = App.ReturnQuoteColor();
            }
            var g = ThinkDataSource.Instance.ReturnFavoriteGroup();
            
            if (g != null)
            {
                foreach (var item in g.Items)
                {
                    item.SColor = App.ReturnQuoteColor();
                }
            }
            ThinkDataSource.SaveLocalDataAsync();
             * */
        }
    }
}
