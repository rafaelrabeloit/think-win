using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ApplicationSettings;
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
    public sealed partial class AboutUserControl : UserControl
    {

        public AboutUserControl()
        {
            this.InitializeComponent();

            string back = App.Current.RequestedTheme == ApplicationTheme.Light ? App.Current.Resources["LightBackground"] as string : App.Current.Resources["DarkBackground"] as string;
            //gridContent.Background = new SolidColorBrush(App.GetColorFromHexString(back));
        }

	    private void OnBackButtonClicked(object sender, RoutedEventArgs e)
	    {
	        if (this.Parent.GetType() == typeof(Popup))
	        {
	            ((Popup)this.Parent).IsOpen = false;
	        }
	        SettingsPane.Show();
	    }

    }
}
