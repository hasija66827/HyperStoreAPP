//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Media.Playlists;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SDKTemplate
{
    public sealed partial class NonInventoryProductScenario : Page
    {
        private MainPage rootPage = MainPage.Current;
        public NonInventoryViewModel ViewModel { get; set; }
        public NonInventoryProductScenario()
        {
            this.InitializeComponent();
            this.ViewModel = new NonInventoryViewModel();
        }

        public class NonInventoryViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged = delegate { };
            private ObservableCollection<Product> _products = new ObservableCollection<Product>();
            public ObservableCollection<Product> Products { get { return this._products; } }
            public NonInventoryViewModel()
            {
                this._products.Add(new Product("1111", "Maggi", 12, 2, 1));
                this._products.Add(new Product("2222", "Ghee", 310, 0, 2));
                this._products.Add(new Product("4333", "shampoo", 200, 200, 50));
                this._products.Add(new Product("1111", "Maggi", 12, 2, 1));
                this._products.Add(new Product("2222", "Ghee", 310, 0, 2));
                this._products.Add(new Product("4333", "shampoo", 200, 200, 50));
                this._products.Add(new Product("1111", "Maggi", 12, 2, 1));
                this._products.Add(new Product("2222", "Ghee", 310, 0, 2));
                this._products.Add(new Product("4333", "shampoo", 200, 200, 50));
                this._products.Add(new Product("1111", "Maggi", 12, 2, 1));
                this._products.Add(new Product("2222", "Ghee", 310, 0, 2));
                this._products.Add(new Product("1111", "Maggi", 12, 2, 1));
                this._products.Add(new Product("2222", "Ghee", 310, 0, 2));
                this._products.Add(new Product("4333", "shampoo", 200, 200, 50));
                this._products.Add(new Product("4333", "shampoo", 200, 200, 50));
                this._products.Add(new Product("4333", "shampoo", 200, 200, 50));
                this._products.Add(new Product("4333", "shampoo", 200, 200, 50));
                this._products.Add(new Product("4333", "shampoolast", 200, 200, 50));
            }
            public void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                // Raise the PropertyChanged event, passing the name of the property whose value has changed.
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
