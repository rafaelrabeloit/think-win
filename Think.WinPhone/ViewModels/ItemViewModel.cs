using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Think.Core.Domain;

namespace Think.WinPhone.ViewModels
{
    public class QuoteViewModel : Quote, INotifyPropertyChanged
    {
        private string _text;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public override string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (value != _text)
                {
                    _text = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }

        private string _authorFullname;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public override string AuthorFullname
        {
            get
            {
                return _authorFullname;
            }
            set
            {
                if (value != _authorFullname)
                {
                    _authorFullname = value;
                    NotifyPropertyChanged("AuthorFullname");
                }
            }
        }

        private string _categoryDescription;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string CategoryDescription
        {
            get
            {
                return _categoryDescription;
            }
            set
            {
                if (value != _categoryDescription)
                {
                    _categoryDescription = value;
                    NotifyPropertyChanged("CategoryDescription");
                }
            }
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