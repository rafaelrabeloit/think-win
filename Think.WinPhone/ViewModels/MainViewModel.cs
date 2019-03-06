using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using Think.Core.Domain;
using Think.WinPhone.Resources;

namespace Think.WinPhone.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<QuoteViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<QuoteViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public bool ValidateSignedRequest()
        {
            string applicationSecret = "bda0974338c86821780aa2b0b76020d3";

            string expectedSignature = "0AnEmTVw7BPHVjMQbIG/B53xQYfFyB2Cj7haqRnMYWQ=";
            string payload = "/1/users/2/userquotes?after=0&timestamp=1370675077120&apikey=975619320584110";

            // Attempt to get same hash
            var Hmac = SignWithHmac(UTF8Encoding.UTF8.GetBytes(payload), UTF8Encoding.UTF8.GetBytes(applicationSecret));
            var HmacBase64 = ToUrlBase64String(Hmac);

            return (HmacBase64 == expectedSignature);
        }


        private string ToUrlBase64String(byte[] Input)
        {
            return Convert.ToBase64String(Input).Replace("=", String.Empty)
                                                .Replace('+', '-')
                                                .Replace('/', '_');
        }

        private byte[] SignWithHmac(byte[] dataToSign, byte[] keyBody)
        {
            using (var hmacAlgorithm = new HMACSHA256(keyBody))
            {
                hmacAlgorithm.ComputeHash(dataToSign);
                return hmacAlgorithm.Hash;
            }
        }
        
        public void Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = "http://api.dev.thinkapp.me";

            /*
            client.Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey);
            request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment); // used on every request
            */

            var asyncHandler = client.ExecuteAsync<T>(request, response =>
            {
                if (response.ErrorException != null)
                {
                    const string message = "Error retrieving response.  Check inner details for more info.";
                    var twilioException = new ApplicationException(message, response.ErrorException);
                    throw twilioException;
                }
            });
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            //bda0974338c86821780aa2b0b76020d3

            //975619320584110

            //ValidateSignedRequest();


            var request = new RestRequest();
            request.Resource = "1/quotes";
            request.RootElement = "list";
            /*
            request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);
            */

            Execute< List<Quote> >(request);
            

            // Sample data; replace with real data
            this.Items.Add(new QuoteViewModel() { Text = "runtime one", AuthorFullname = "Maecenas praesent accumsan bibendum", CategoryDescription = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime two", AuthorFullname = "Dictumst eleifend facilisi faucibus", CategoryDescription = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime three", AuthorFullname = "Habitant inceptos interdum lobortis", CategoryDescription = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime four", AuthorFullname = "Nascetur pharetra placerat pulvinar", CategoryDescription = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime five", AuthorFullname = "Maecenas praesent accumsan bibendum", CategoryDescription = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime six", AuthorFullname = "Dictumst eleifend facilisi faucibus", CategoryDescription = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime seven", AuthorFullname = "Habitant inceptos interdum lobortis", CategoryDescription = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime eight", AuthorFullname = "Nascetur pharetra placerat pulvinar", CategoryDescription = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime nine", AuthorFullname = "Maecenas praesent accumsan bibendum", CategoryDescription = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime ten", AuthorFullname = "Dictumst eleifend facilisi faucibus", CategoryDescription = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime eleven", AuthorFullname = "Habitant inceptos interdum lobortis", CategoryDescription = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime twelve", AuthorFullname = "Nascetur pharetra placerat pulvinar", CategoryDescription = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime thirteen", AuthorFullname = "Maecenas praesent accumsan bibendum", CategoryDescription = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime fourteen", AuthorFullname = "Dictumst eleifend facilisi faucibus", CategoryDescription = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime fifteen", AuthorFullname = "Habitant inceptos interdum lobortis", CategoryDescription = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat" });
            this.Items.Add(new QuoteViewModel() { Text = "runtime sixteen", AuthorFullname = "Nascetur pharetra placerat pulvinar", CategoryDescription = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum" });

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}