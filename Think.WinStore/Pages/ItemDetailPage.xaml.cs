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
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.StartScreen;
using Think.WinStore.TileContent;
using Windows.UI.Notifications;
using Think.WinStore.Common;


namespace Think.WinStore
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class ItemDetailPage : Think.WinStore.Common.LayoutAwarePage
    {
        public QuoteItem _item;
        private bool loading = false;

        public ItemDetailPage()
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
            args.Request.Data.Properties.Title = _item.Autor;
            args.Request.Data.Properties.Description = App.loader.GetString("ShareQuoteMessageDescription");
            args.Request.Data.SetText(_item.Quote + "\n" + _item.Autor);
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
            loading = true;

            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }

            
            _item = ThinkDataSource.GetItem((String)navigationParameter);
            
            this.DefaultViewModel["Group"] = _item.Group;
            this.DefaultViewModel["Items"] = _item.Group.Items;

            this.flipView.SelectedItem = _item;

            string back = App.Current.RequestedTheme == ApplicationTheme.Light ? App.Current.Resources["LightBackground"] as string : App.Current.Resources["DarkBackground"] as string;
            rootGrid.Background = new SolidColorBrush(App.GetColorFromHexString(back));
            
            App.startArguments = "";
            CheckAppBarButtons();

            loading = false;
        }
        
        public void unfavButton_Click(object sender, RoutedEventArgs e)
        {
            //Remove da lista de favoritos
            ThinkDataSource.Instance.RemoveItemFromFavoriteGroup(_item);

            CheckAppBarButtons();
            ThinkDataSource.SaveLocalDataAsync();
        }

        public void favButton_Click(object sender, RoutedEventArgs e)
        {
            //Adiciona na lista de favoritos
            ThinkDataSource.Instance.AddItemToFavoriteGroup(_item);

            CheckAppBarButtons();
            ThinkDataSource.SaveLocalDataAsync();
        }

        public async void pinButton_Click(object sender, RoutedEventArgs e)
        {
            this.BottomAppBar.IsSticky = true;

            _item.IsPin = true;

            var logo = new Uri("ms-appx:///Assets/Logo.png");
            var logoWide = new Uri("ms-appx:///Assets/WideLogo.png");
            Uri smallLogo = new Uri("ms-appx:///Assets/SmallLogo.png");


            SecondaryTile tile = new SecondaryTile(
	            _item.UniqueId,                 // Tile ID
	            "Think",                        // Tile short name
	            _item.Autor,                    // Tile display name
                _item.UniqueId,                 // Activation argument
                TileOptions.ShowNameOnLogo | TileOptions.ShowNameOnWideLogo,
	            logo,                           // Tile logo
                logoWide
	        );

            // Specify a foreground text value.
            // The tile background color is inherited from the parent unless a separate value is specified.
            tile.ForegroundText = ForegroundText.Light;


            // Like the background color, the small tile logo is inherited from the parent application tile by default. Let's override it, just to see how that's done.
            tile.SmallLogo = smallLogo;


	        bool created = await tile.RequestCreateAsync();

            if (created)
            {
                // Note: This sample contains an additional reference, NotificationsExtensions, which you can use in your apps
                ITileWideText09 tileContent = TileContentFactory.CreateTileWideText09();
                tileContent.TextHeading.Text = _item.Autor;
                tileContent.TextBodyWrap.Text = _item.Quote;


                ITileSquareText04 squareContent = TileContentFactory.CreateTileSquareText04();
                squareContent.TextBodyWrap.Text = _item.Quote;
                tileContent.SquareContent = squareContent;


                // Send the notification to the secondary tile by creating a secondary tile updater
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(_item.UniqueId).Update(tileContent.CreateNotification());
                
            }

            // Show pin button in the appbar
            CheckAppBarButtons();

            this.BottomAppBar.IsSticky = false;
            ThinkDataSource.SaveLocalDataAsync();
        }

        public async void unpinButton_Click(object sender, RoutedEventArgs e)
        {
            this.BottomAppBar.IsSticky = true;

            _item.IsPin = false;

            // First prepare the tile to be unpinned
            SecondaryTile secondaryTile = new SecondaryTile(_item.UniqueId);
            // Now make the delete request.
            bool isUnpinned = await secondaryTile.RequestDeleteAsync();

            // Show pin button in the appbar
            CheckAppBarButtons();


            this.BottomAppBar.IsSticky = false;
            ThinkDataSource.SaveLocalDataAsync();
        }

        public void copyButton_Click(object sender, RoutedEventArgs e)
        {

            DataPackage dataPackage = new DataPackage();
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText(_item.Quote + "\n" + _item.Autor);

            Clipboard.SetContent(dataPackage);
        }
        
        public void CheckAppBarButtons()
        {
            // Para habilitar/desabilitar botões de favoritos/PinToStartScreen

            // Se recuperar, ele é um livro favorito, e ativa o botao para retirar
            if (this._item.IsFavorite)
            {
                VisualStateManager.GoToState(this, "RemoverFavoritoEnabled", true);
            }
            //Se não, ativa o botão para adicionar
            else
            {
                VisualStateManager.GoToState(this, "FavoritoEnabled", true);
            }

            // ativa ou desativa o PinButton
            if (this._item.IsPin)
            {
                VisualStateManager.GoToState(this, "UnPinButtonEnabled", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "PinButtonEnabled", true);
            }
            

        }



        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            var selectedItem = _item;
            pageState["SelectedItem"] = selectedItem.UniqueId;
        }

        private void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loading) return;
            _item = (QuoteItem)this.flipView.SelectedItem;
            CheckAppBarButtons();
        }
                    
    }
}
