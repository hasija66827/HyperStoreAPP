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

using Models;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.Playlists;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace SDKTemplate
{
    public delegate void ProductStockSelectionChangedDelegate(ProductViewModelBase productViewModelBase);
    public sealed partial class ProductInStock : Page
    {
        public event ProductStockSelectionChangedDelegate ProductStockSelectionChangedEvent;
        public static ProductInStock Current;
        private Int32 _totalProducts;
        public ProductInStock()
        {
            Current = this;
            this.InitializeComponent();
            ProductASBCC.Current.SelectedProductChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            FilterProductCC.Current.FilterProductCriteriaChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            FilterProductByTagCC.Current.TagListChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _totalProducts = await ProductDataSource.RetrieveTotalProducts();
            await UpdateMasterListViewItemSourceByFilterCriteria();
            DisableContentTransitions();
            base.OnNavigatedTo(e);
        }

        //Will Update the MasterListView by filtering out Products on the basis of specific filter criteria.
        private async Task UpdateMasterListViewItemSourceByFilterCriteria()
        {
            var selectedProduct = ProductASBCC.Current.SelectedProductInASB;
            ProductFilterCriteriaDTO pfc = new ProductFilterCriteriaDTO()
            {
                ProductId = selectedProduct?.ProductId,
                TagIds = FilterProductByTagCC.Current.SelectedTagIds,
                FilterProductQDT = FilterProductCC.Current.ProductFilterQDT
            };
            var products = await ProductDataSource.RetrieveProductDataAsync(pfc);
            if (products != null)
            {
                var items = products.Select(p => new ProductViewModelBase(p)).ToList();
                MasterListView.ItemsSource = items;
                ProductCountTB.Text = "( " + items.Count + " / " + _totalProducts + " )";
            }
        }
        
        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            // Assure we are displaying the correct item. This is necessary in certain adaptive cases.
            //MasterListView.SelectedItem = _lastSelectedItem;
        }
        private async void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (ProductViewModelBase)e.ClickedItem;
            DetailContentPresenter.Content = null;
            var priceQuotedByWholeSellerCollection = new PriceQuotedBySupplierCollection();
            var x = await AnalyticsDataSource.RetrieveLatestPriceQuotedBySupplierAsync(clickedItem.ProductId);
            priceQuotedByWholeSellerCollection.PriceQuotedBySuppliers = x.Select(tpqs => new PriceQuotedBySupplierViewModel(tpqs)).ToList();
            DetailContentPresenter.Content = priceQuotedByWholeSellerCollection;
            this.ProductStockSelectionChangedEvent?.Invoke(clickedItem);
            // Play a refresh animation when the user switches detail items.
            EnableContentTransitions();
        }

        private void EnableContentTransitions()
        {
            DetailContentPresenter.ContentTransitions.Clear();
            // just for adding the transition on the content selection.
            //DetailContentPresenter.ContentTransitions.Add(new EntranceThemeTransition());
        }

        private void DisableContentTransitions()
        {
            if (DetailContentPresenter != null)
            {
                DetailContentPresenter.ContentTransitions.Clear();
            }
        }
    }
}
