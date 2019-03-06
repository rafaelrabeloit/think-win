using Think.WinStore.Common;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
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
using Think.WinStore.UserControls;
using Windows.UI.ApplicationSettings;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Notifications;
using Think.WinStore.Data;

// The Grid App template is documented at http://go.microsoft.com/fwlink/?LinkId=234226

namespace Think.WinStore
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool settings = false;
        public Frame rootFrame;
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public static string startArguments = "";
        public static Guid guid;
        public static int version = 2;
        public static bool first = false;

        public static string UrlGetQuote = "http://neptune.li/think/webservice-v02/get_quote.php?DeviceID=";
        public static string UrlGetTile = "http://neptune.li/think/webservice-v02/tile_quote.php?DeviceID=";
        public static string UrlGetUnread = "http://neptune.li/think/webservice-v02/unread_quotes.php?DeviceID=";
        public static string UrlGetAll = "http://neptune.li/think/webservice-v02/all_quotes.php?DeviceID=";

        public static TypedEventHandler<DataTransferManager, DataRequestedEventArgs> Handler = null;

        public static string LanguageTag;

        /// <summary>
        /// 
        /// </summary>
        public static ResourceLoader loader = new Windows.ApplicationModel.Resources.ResourceLoader();



        #region TilePolling Admin

        public static string ReturnUrlPolling()
        {
            return UrlGetTile + guid.ToString() + "&Language=" + ReturnLanguage();
        }

        public static string ReturnUrlQuote()
        {
            return UrlGetQuote + guid.ToString() + "&Language=" + ReturnLanguage();
        }

        public static string ReturnUrlUnread()
        {
            return UrlGetUnread + guid.ToString();
        }

        public static string ReturnUrlAll()
        {
            return UrlGetAll + guid.ToString();
        }


        public static void initiateTilePolling()
        {
            // update the tile poll URI
            Uri polledUri = new Uri(ReturnUrlPolling());
            PeriodicUpdateRecurrence recurrence = PeriodicUpdateRecurrence.TwelveHours;
            TileUpdateManager.CreateTileUpdaterForApplication().StartPeriodicUpdate(polledUri, recurrence);


            localSettings.Values["UPDATE_TIME"] = 3;
            localSettings.Values["UPDATE_START"] = true;
        }

        public static void changeTilePolling(PeriodicUpdateRecurrence t)
        {
            Uri polledUri = new Uri(ReturnUrlPolling());
            PeriodicUpdateRecurrence recurrence = t;
            TileUpdateManager.CreateTileUpdaterForApplication().StartPeriodicUpdate(polledUri, recurrence);

            switch (t)
            {
                case PeriodicUpdateRecurrence.HalfHour: localSettings.Values["UPDATE_TIME"] = 0;
                    break;
                case PeriodicUpdateRecurrence.Hour: localSettings.Values["UPDATE_TIME"] = 1;
                    break;
                case PeriodicUpdateRecurrence.SixHours: localSettings.Values["UPDATE_TIME"] = 2;
                    break;
                case PeriodicUpdateRecurrence.TwelveHours: localSettings.Values["UPDATE_TIME"] = 3;
                    break;
                case PeriodicUpdateRecurrence.Daily: localSettings.Values["UPDATE_TIME"] = 4;
                    break;
            }
            localSettings.Values["UPDATE_START"] = true;
        }

        public static void changeTilePolling(int i)
        {
            Uri polledUri = new Uri(ReturnUrlPolling());

            PeriodicUpdateRecurrence t;

            switch (i)
            {
                case 0: t = PeriodicUpdateRecurrence.HalfHour;
                    break;
                case 1: t = PeriodicUpdateRecurrence.Hour;
                    break;
                case 2: t = PeriodicUpdateRecurrence.SixHours;
                    break;
                case 3: t = PeriodicUpdateRecurrence.TwelveHours;
                    break;
                case 4: t = PeriodicUpdateRecurrence.Daily;
                    break;
                default: t = PeriodicUpdateRecurrence.Daily;
                    break;
            }

            PeriodicUpdateRecurrence recurrence = t;
            TileUpdateManager.CreateTileUpdaterForApplication().StartPeriodicUpdate(polledUri, recurrence);

            localSettings.Values["UPDATE_START"] = true;
        }


        public static void stopTilePolling()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().StopPeriodicUpdate();
            localSettings.Values["UPDATE_START"] = false;
        }

        public static bool isTilePollingStarted()
        {
            return (bool)localSettings.Values["UPDATE_START"];
        }

        public static int returnPeriodicUpdateRecurrence()
        {
            return (int)localSettings.Values["UPDATE_TIME"];
        }

        #endregion


        #region Theme Admin

        public static ApplicationTheme ReturnUserTheme()
        {
            int aux = 0;
            aux = (int)localSettings.Values["UserTheme"];

            if (aux == 0) return ApplicationTheme.Light;
            else return ApplicationTheme.Dark;
        }

        public static void SetUserTheme(ApplicationTheme t)
        {
            localSettings.Values["UserTheme"] = (int)t;
        }

        #endregion


        #region Color Admin

        public static string ReturnQuoteColor()
        {
            return (string)localSettings.Values["QuoteColor"];
        }

        public static int ReturnQuoteColorIndice()
        {
            var aux = (string)localSettings.Values["QuoteColor"];

            if (aux.Equals(QuoteItem.BLUE))
                return 0;
            if (aux.Equals(QuoteItem.GREEN))
                return 1;
            if (aux.Equals(QuoteItem.ORANGE))
                return 2;
            if (aux.Equals(QuoteItem.PURPLE))
                return 3;
            if (aux.Equals(QuoteItem.RED))
                return 4;
            if (aux.Equals(QuoteItem.YELLOW))
                return 5;

            return 0;
        }

        public static void SetQuoteColor(string t)
        {
            localSettings.Values["QuoteColor"] = (string)t;
        }

        #endregion


        #region Language Admin

        public static string ReturnLanguage()
        {
            return (string)localSettings.Values["Language"];
        }

        public static void SetLanguage(string t)
        {

            if (ReturnLanguagesIndice(t) == -1)
                t = ReturnSuportedLanguages().First();

            localSettings.Values["Language"] = (string)t;
        }

        public static List<string> ReturnSuportedLanguages()
        {
            List<string> lang = new List<string>();
            lang.Add("en-US");
            lang.Add("pt-BR");

            return lang;
        }

        public static int ReturnLanguagesIndice(string lang)
        {
            int c = -1;
            List<string> l = ReturnSuportedLanguages();
            foreach (var i in l)
                if (i.Equals(lang))
                    break;
                else
                    c++;
            return c;
        }

        public static int ReturnLanguagesIndice()
        {
            var lang = ReturnLanguage();
            int c = -1;
            List<string> l = ReturnSuportedLanguages();
            foreach (var i in l)
                if (i.Equals(lang))
                {
                    c++;
                    break;
                }
                else
                    c++;
            return c;
        }

        #endregion


        public void CheckFirstRun()
        {
            if (localSettings.Values.ContainsKey("version")) first = ((int)localSettings.Values["version"]) != version;
            else first = true;

            if (true/*first*/)
            {
                localSettings.Values["version"] = version;

                guid = Guid.NewGuid();
                localSettings.Values["GUID"] = guid;

                this.RequestedTheme = ApplicationTheme.Light;
                LanguageTag = Windows.Globalization.Language.CurrentInputMethodLanguageTag;

                SetUserTheme(RequestedTheme);
                SetQuoteColor(QuoteItem.GREEN);
                SetLanguage(LanguageTag);

                initiateTilePolling();
            }
            else
            {
                RequestedTheme = ReturnUserTheme();

                guid = (Guid)localSettings.Values["GUID"];
            }
        }


        public static Windows.UI.Color GetColorFromHexString(string hexValue)
        {
            var a = Convert.ToByte(hexValue.Substring(1, 2), 16);
            var r = Convert.ToByte(hexValue.Substring(3, 2), 16);
            var g = Convert.ToByte(hexValue.Substring(5, 2), 16);
            var b = Convert.ToByte(hexValue.Substring(7, 2), 16);
            return Windows.UI.Color.FromArgb(a, r, g, b);
        }



        /// <summary>
        /// Initializes the singleton Application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            CheckFirstRun();
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active

            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                //Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");
                // Set the default language
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(GroupedItemsPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();


            if (!settings)
            {
                SettingsPane.GetForCurrentView().CommandsRequested += OnCommandsRequested;
                settings = true;
            }

        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }


        protected override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            if (!settings)
            {
                SettingsPane.GetForCurrentView().CommandsRequested += OnCommandsRequested;
                settings = true;
            }
            // Activate immediately if data is available
            //Think.WinStore.SearchResultsPage.Activate(args.QueryText, args.PreviousExecutionState);
        }

        public void OnCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {

            // Add an About command
            var about = new SettingsCommand("about", loader.GetString("About"), (handler) =>
            {
                var settings = new SettingsFlyout();
                
                settings.Content = new AboutUserControl();
                settings.Title = loader.GetString("About");
                settings.Show();
            });

            args.Request.ApplicationCommands.Add(about);

            // Add a Preferences command
            var preferences = new SettingsCommand("preferences", loader.GetString("Preferences"), (handler) =>
            {
                var settings = new SettingsFlyout();
                settings.Content = new PreferencesUserControl();
                settings.Title = loader.GetString("Preferences");
                settings.Show();
            });

            args.Request.ApplicationCommands.Add(preferences);

        }
    }
}
