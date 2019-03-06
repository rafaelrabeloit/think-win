using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Storage;
using Think.WinStore.Common;
using Windows.Data.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.UI.Xaml;
using Windows.Data.Xml.Dom;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using Windows.ApplicationModel.Resources;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace Think.WinStore.Data
{
    /// <summary>
    /// Quote item data model.
    /// </summary>
    [DataContract]
    public class QuoteItem : BindableBase
    {
        public QuoteItem() {

            SColor = QuoteItem.GREEN;
        }

        public QuoteItem(String uniqueId, String autor, String quote, DateTime received, QuoteGroup group)
        {
            this._group = group;

            this._uniqueId = uniqueId;
            this._autor = autor;
            this._quote = quote;
            this._received = received;
            SColor = QuoteItem.GREEN;
        }

        public static string BLUE = "#FF00a79d";
        public static string GREEN = "#FF3cb549";
        public static string YELLOW = "#FFf7931d";
        public static string ORANGE = "#FFf05a28";
        public static string RED = "#FFbe1e2d";
        public static string PURPLE = "#FF9e1e62";

        [DataMember]
        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        [DataMember]
        private string _autor = string.Empty;
        public string Autor
        {
            get { return this._autor; }
            set { this.SetProperty(ref this._autor, value); }
        }

        [DataMember]
        private string _quote = string.Empty;
        public string Quote
        {
            get { return this._quote; }
            set { this.SetProperty(ref this._quote, value); }
        }

        [DataMember]
        private DateTime _received = DateTime.Now;
        public DateTime Received
        {
            get { return this._received; }
            set { this.SetProperty(ref this._received, value); }
        }

        [DataMember]
        private QuoteGroup _group;
        public QuoteGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }


        [DataMember]
        private string _scolor;
        public string SColor
        {
            get { return this._scolor; }
            set { this.SetProperty(ref this._scolor, value); }
        }

        [DataMember]
        private int _wSize = 4;
        public int WSize
        {
            get { return this._wSize; }
            set { this.SetProperty(ref this._wSize, value); }
        }


        [DataMember]
        private int _hSize = 1;
        public int HSize
        {
            get { return this._hSize; }
            set { this.SetProperty(ref this._hSize, value); }
        }


        [DataMember]
        private bool _isPin = false;
        public bool IsPin
        {
            get { return this._isPin; }
            set { this.SetProperty(ref this._isPin, value); }
        }


        [DataMember]
        private bool _isFavorite = false;
        public bool IsFavorite
        {
            get { return this._isFavorite; }
            set { this.SetProperty(ref this._isFavorite, value); }
        }

        [DataMember]
        private bool _isRecent = false;
        public bool IsRecent
        {
            get { return this._isRecent; }
            set { this.SetProperty(ref this._isRecent, value); }
        }

        public QuoteItem Clone()
        {
            //COPIA SOMENTE O NECESSARIO: O QUE O USUARIO PODE VER!
            QuoteItem newitem = new QuoteItem();

            newitem.WSize = this.WSize;
            newitem.HSize = this.HSize;
            newitem.SColor = this.SColor;

            newitem.Group = this.Group;

            newitem.Quote = this.Quote;
            newitem.Autor = this.Autor;
            newitem.Received = this.Received;

            newitem.UniqueId = this.UniqueId;

            return newitem;
        }

        #region sobrecarregando operadores para HashSet de Items
        public override string ToString()
        {
            return Quote;
        }

        public bool Equals(QuoteItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Received == Received;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(QuoteItem)) return false;
            return Equals((QuoteItem)obj);
        }

        public override int GetHashCode()
        {
            return Received.GetHashCode();
        }

        public static bool operator ==(QuoteItem left, QuoteItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(QuoteItem left, QuoteItem right)
        {
            return !Equals(left, right);
        }
        #endregion
    }

    /// <summary>
    /// Quote group data model.
    /// </summary>
    [DataContract]
    public class QuoteGroup : Think.WinStore.Common.BindableBase
    {
        QuoteGroup()
        {
            Items.CollectionChanged += ItemsCollectionChanged;
        }

        public QuoteGroup(String uniqueId, String title)
        {
            this._uniqueId = uniqueId;
            this._title = title;

            Items.CollectionChanged += ItemsCollectionChanged;
        }

        [DataMember]
        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        [DataMember]
        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }


        [DataMember]
        private int _topnumber = 6;
        public int TopNumber
        {
            get { return this._topnumber; }
            set { this.SetProperty(ref this._topnumber, value); }
        }
		
        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            //
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < TopNumber)
                    {
                        TopItems.Insert(e.NewStartingIndex,Items[e.NewStartingIndex]);
                        if (TopItems.Count > TopNumber)
                        {
                            TopItems.RemoveAt(TopNumber);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < TopNumber && e.NewStartingIndex < TopNumber)
                    {
                        TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < TopNumber)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        TopItems.Add(Items[TopNumber-1]);
                    }
                    else if (e.NewStartingIndex < TopNumber)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        TopItems.RemoveAt(TopNumber);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < TopNumber)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        if (Items.Count >= TopNumber)
                        {
                            TopItems.Add(Items[TopNumber-1]);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < TopNumber)
                    {
                        TopItems[e.OldStartingIndex] = Items[e.OldStartingIndex];
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    TopItems.Clear();
                    while (TopItems.Count < Items.Count && TopItems.Count < TopNumber)
                    {
                        TopItems.Add(Items[TopItems.Count]);
                    }
                    break;
            }
        }

		
        private ObservableCollection<QuoteItem> _items = new ObservableCollection<QuoteItem>();
        public ObservableCollection<QuoteItem> Items
        {
            get { return this._items; }
        }


        private ObservableCollection<QuoteItem> _topItems = new ObservableCollection<QuoteItem>();
        public ObservableCollection<QuoteItem> TopItems
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            //
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed
            get { return _topItems; }
        }
        
    }



    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// </summary>
    public sealed class ThinkDataSource : BindableBase
    {
        private static ThinkDataSource _thinkDataSource = new ThinkDataSource();
        public static ThinkDataSource Instance
        {
            get { return _thinkDataSource; }
        }

        //Lista de Grupos para Binding. Não persiste.
        private ObservableCollection<QuoteGroup> _allGroups;
        public ObservableCollection<QuoteGroup> AllGroups
        {
            get { return this._allGroups; }
            set { this.SetProperty(ref this._allGroups, value); }
        }

        //Lista de TODOS as Citações. Persiste
        private HashSet<QuoteItem> _quoteList;
        public HashSet<QuoteItem> QuoteList
        {
            get { return this._quoteList; }
        }


        static int statusLoad = 0;

        public static int XMLCorruptedError = -2;
        public static int InternetError = -1;
        public static int Loaded = 1;
        public static int Loading = 0;

        private static object _lockerQuoteList = new object();


        public ThinkDataSource()
        {
            XMLCorruptedError = -2;
            InternetError = -1;
            Loaded = 1;
            Loading = 0;

            _lockerQuoteList = new object();

            Source();
        }

        private async void Source()
        {
            ThinkDataSource.statusLoad = Loading;

            await LoadLocalDataAsync();

            await LoadNewRemoteDataAsync();


            if (ThinkDataSource.statusLoad == Loading)
                ThinkDataSource.statusLoad = Loaded;


            SaveLocalDataAsync();
        }

        public static IEnumerable<QuoteGroup> GetGroups(string uniqueId)
        {
            //if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");
            
            return _thinkDataSource.AllGroups;
        }

        public static QuoteGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            try
            {
                if (_thinkDataSource.AllGroups == null) return null;

                var matches = _thinkDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
                if (matches.Count() == 1) return matches.First();
                else return null;
            }
            catch (ArgumentNullException e)
            {
                Log("GetGroup", e.Message);
                return null;
            }
        }
        
        public static QuoteItem GetItem(string uniqueId)
        {
            if (_thinkDataSource.QuoteList == null) return null;

            // Simple linear search is acceptable for small data sets
            var matches = _thinkDataSource.QuoteList.SingleOrDefault((item) => item.UniqueId.Equals(uniqueId));
            return matches;
        }



        #region Operações de Load and Save

        public static void SaveLocalDataAsync()
        {
            lock (_lockerQuoteList)
            {
                ObjectPersister.Save("data_quotes", ObjectPersisterLocation.Local, ThinkDataSource.Instance.QuoteList.ToArray());
            }
        }

        public async Task<AsyncVoidMethodBuilder> LoadLocalDataAsync()
        {
                if (App.first)
                {
                    AllGroups = new ObservableCollection<QuoteGroup>();
                    _quoteList = new HashSet<QuoteItem>();
                    return AsyncVoidMethodBuilder.Create();
                }

                QuoteItem[] list = null;
                try
                {
                    list = await ObjectPersister.Load<QuoteItem[]>("data_quotes", ObjectPersisterLocation.Local);
                }
                catch (Exception e)
                {
                    Log("LoadLocalDataAsync", e.Message);
                    ThinkDataSource.statusLoad = ThinkDataSource.XMLCorruptedError;
                    list = null;
                }
                finally
                {
                    //
                    lock (_lockerQuoteList)
                    {
                        if (list == null)
                            _quoteList = new HashSet<QuoteItem>();
                        else
                            _quoteList = new HashSet<QuoteItem>(list);

                        AllGroups = new ObservableCollection<QuoteGroup>();

                        MountGroups();
                    }

                }
            return AsyncVoidMethodBuilder.Create();
        }

        public async Task<AsyncVoidMethodBuilder> LoadNewRemoteDataAsync()
        {
            string ret = "";

            try
            {
                ret = await GetAsync(App.ReturnUrlUnread());
            }
            catch (HttpRequestException e)
            {
                statusLoad = InternetError;
                Log("LoadNewRemoteDataAsync", e.Message);
                return AsyncVoidMethodBuilder.Create();
            }

            MountQuotes(ret);

            return AsyncVoidMethodBuilder.Create();
        }

        public async Task<AsyncVoidMethodBuilder> LoadAllRemoteDataAsync()
        {
            string ret = "";

            try
            {
                ret = await GetAsync(App.ReturnUrlAll() );
            }
            catch (HttpRequestException e)
            {
                statusLoad = InternetError;
                Log("LoadAllRemoteDataAsync", e.Message);
                return AsyncVoidMethodBuilder.Create();
            }

            MountQuotes(ret);

            return AsyncVoidMethodBuilder.Create();
        }

        public async Task<AsyncVoidMethodBuilder> LoadOneRemoteDataAsync()
        {
            string ret = "";

            try
            {
                ret = await GetAsync(App.ReturnUrlQuote());                
            }
            catch (HttpRequestException e)
            {
                statusLoad = InternetError;
                Log("LoadOneRemoteDataAsync", e.Message);
                return AsyncVoidMethodBuilder.Create();
            }

            MountQuotes(ret);

            return AsyncVoidMethodBuilder.Create();
        }

        #endregion

        private void MountQuotes(string str)
        {
            //
            lock (_lockerQuoteList)
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.LoadXml(str);

                    XmlElement root = doc.DocumentElement;


                    foreach (XmlElement item in root.ChildNodes)
                    {
                        QuoteItem quoteitem = new QuoteItem();

                        quoteitem.Autor = item.GetAttribute("autor");
                        quoteitem.UniqueId = item.GetAttribute("id");
                        quoteitem.Received = DateTime.Parse(item.GetAttribute("time"));

                        quoteitem.Quote = item.GetElementsByTagName("texto").First().InnerText;

                        string gname = item.GetElementsByTagName("grupo").First().InnerText;
                        string gid = (item.GetElementsByTagName("grupo").First() as XmlElement).GetAttribute("id");


                        _quoteList.Add(quoteitem);

                        quoteitem.IsRecent = true;

                        quoteitem.SColor = App.ReturnQuoteColor();

                        AddItemToRecentGroup(quoteitem);

                        var g = AddItemToGroup(quoteitem, gid, gname);
                    }
                }
                catch (Exception e)
                {
                    Log("MountQuote", e.Message + "\n" + str);
                }
            }
        }


        #region Operações com Grupos

        public void MountGroups()
        {
            foreach (var item in _quoteList)
            {
                AddItemToGroup(item, item.Group.UniqueId, item.Group.Title);
                if (item.IsFavorite)
                {
                    AddItemToFavoriteGroup(item);
                }
                if (item.IsRecent)
                {
                    AddItemToRecentGroup(item);
                }
            }
        }
        
        public QuoteGroup AddItemToGroup(QuoteItem i, string id, string title, bool insert_begin = true)
        {
            QuoteGroup group = null;
            
            group = AllGroups.SingleOrDefault(s => s.UniqueId == id);
            if (group == null)
            {
                group = new QuoteGroup(id, title);

                _allGroups.Add(group);
                i.Group = group;
            }
            else
            {
                i.Group = group;
            }

            if(insert_begin)
                group.Items.Insert(0,i);
            else
                group.Items.Add(i);
            
            return group;
        }

        public QuoteGroup AddGroup(string id, string title)
        {
            QuoteGroup group = AllGroups.SingleOrDefault(s => s.UniqueId == id);
            if (group == null)
            {
                group = new QuoteGroup(id, title);
                _allGroups.Add(group);
            }

            return group;
        }


        public QuoteGroup ReturnFavoriteGroup()
        {
            QuoteGroup group = AllGroups.SingleOrDefault(s => s.UniqueId == "-1");
            return group;
        }

        public QuoteGroup AddFavoriteGroup()
        {
            QuoteGroup group = ReturnFavoriteGroup();
            
            if (group == null)
            {
                group = new QuoteGroup("-1", App.loader.GetString("Favorites"));

                if (ReturnRecentGroup() != null)
                {
                    _allGroups.Insert(1, group);
                }
                else
                {
                    _allGroups.Insert(0, group);
                }
            }
            return group;
        }

        public void AddItemToFavoriteGroup(QuoteItem item, bool insert_begin = true)
        {            
            var group = AddFavoriteGroup();
            var newitem = item.Clone();
            newitem.WSize = 2;
            newitem.HSize = 2;
            
            if (insert_begin)
                group.Items.Insert(0, newitem);
            else
                group.Items.Add(newitem);
            
            item.IsFavorite = true;
        }

        public void RemoveItemFromFavoriteGroup(QuoteItem item)
        {
            var group = AddFavoriteGroup();
            var newitem = group.Items.SingleOrDefault( v => v.UniqueId == item.UniqueId);

            if (newitem == null) return;
            group.Items.Remove(newitem);
            if (group.Items.Count == 0) _allGroups.Remove(group);

            
            item.IsFavorite = false;
        }

        
        public QuoteGroup ReturnRecentGroup()
        {
            QuoteGroup group = AllGroups.SingleOrDefault(s => s.UniqueId == "-2");
            return group;
        }

        public QuoteGroup AddRecentGroup()
        {
            QuoteGroup group = ReturnRecentGroup();
            if (group == null)
            {
                group = new QuoteGroup("-2", App.loader.GetString("Recents"));

                _allGroups.Insert(0, group);
            }

            return group;
        }

        public void AddItemToRecentGroup(QuoteItem item, bool insert_begin = true)
        {
            var group = AddRecentGroup();

            if (insert_begin)
                group.Items.Insert(0, item);
            else
                group.Items.Add(item);
            
            item.IsRecent = true;
        }

        public void ClearAllRecentGroup()
        {
            var group = AddRecentGroup();
            if (group != null)
            {                
                foreach (var i in group.Items)
                    i.IsRecent = false;

                ThinkDataSource.Instance.AllGroups.Remove(group);
            }
        }

        #endregion

        public static void StartLoadingStatus()
        {
            statusLoad = ThinkDataSource.Loading;
        }
        public static void StopLoadingStatus()
        {
            if (ThinkDataSource.statusLoad == Loading)
                ThinkDataSource.statusLoad = Loaded;
        }

        public static int GetStatusLoad()
        {
            return statusLoad;
        }

        public async Task<string> GetAsync(string uri)
        {
            var httpClient = new HttpClient();
            
            var response = await httpClient.GetAsync(uri);

            //will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public static void Log(string where, string what)
        {
            System.Diagnostics.Debug.WriteLine(where);
            System.Diagnostics.Debug.WriteLine(what);
        }

    }
}
