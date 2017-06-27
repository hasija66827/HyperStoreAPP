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
using Windows.Media.Playlists;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace SDKTemplate
{
    public sealed partial class ProductInStock : Page
    {
        public PriceQuotedByWholeSellerCollection PriceQuotedByWholeSellerCollection { get; set; }
        public static ProductInStock Current;
        public ProductInStock()
        {
            Current = this;
            this.InitializeComponent();
            ProductASBCC.Current.SelectedProductChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            FilterProductCC.Current.FilterProductCriteriaChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            UpdateMasterListViewItemSourceByFilterCriteria();
            AddToCartBtn.Click += AddToCartBtn_Click;
            GoToCartBtn.Click += GoToCartBtn_Click;
        }

        //Will Update the MasterListView by filtering out Products on the basis of specific filter criteria.
        private void UpdateMasterListViewItemSourceByFilterCriteria()
        {
            var selectedProduct = ProductASBCC.Current.SelectedProductInASB;
            var productId = selectedProduct?.ProductId;
            var filterProductCriteria = FilterProductCC.Current.FilterProductCriteria;
            var items = ProductDataSource.GetFilteredProducts(filterProductCriteria, productId);
            MasterListView.ItemsSource = items;
            var totalResults = items.Count;
            ProductCountTB.Text = "(" + totalResults.ToString() + "/" + ProductDataSource.Products.Count.ToString() + ")";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateForVisualState(AdaptiveStates.CurrentState);
            // Don't play a content transition for first item load.
            // Sometimes, this content will be animated as part of the page transition.
            DisableContentTransitions();
        }

        private void GoToCartBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (ProductViewModelBase)MasterListView.SelectedItem;
            try
            {
                //If product has not been added to cart already
                if (selectedProduct.WholeSellerId == null)
                    throw new Exception("Go To Cart Button should have been disabled");
                this.Frame.Navigate(typeof(ProductListToPurhcaseCC));
            }
            catch (Exception exception)
            {

            }
        }

        private void AddToCartBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (ProductViewModelBase)MasterListView.SelectedItem;
            if (selectedProduct == null)
            {
                MainPage.Current.NotifyUser("Please select the product", NotifyType.ErrorMessage);
                return;
            }
            var selectedWholeSeller = PriceQuotedByWholeSellerViewModel.selectedWholeSeller;
            if (selectedWholeSeller == null)
            {
                MainPage.Current.NotifyUser("Please select the whole seller", NotifyType.ErrorMessage);
                return;
            }
            try
            {
                //if Product has been added to cart already.
                if (selectedProduct.WholeSellerId != null)
                    throw new Exception("AddToCart Button should have been disabled");
                ProductDataSource.UpdateWholSellerIdForProduct(selectedProduct.ProductId, selectedWholeSeller.WholeSellerId);
                ((ProductViewModelBase)MasterListView.SelectedItem).WholeSellerId = selectedWholeSeller.WholeSellerId;
                AddToCartBtn.IsEnabled = false;
            }
            catch (Exception exception)
            {

            }
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }

        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            var isNarrow = newState == NarrowState;
            EntranceNavigationTransitionInfo.SetIsTargetElement(MasterListView, isNarrow);
            if (DetailContentPresenter != null)
            {
                EntranceNavigationTransitionInfo.SetIsTargetElement(DetailContentPresenter, !isNarrow);
            }
        }
        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            // Assure we are displaying the correct item. This is necessary in certain adaptive cases.
            //MasterListView.SelectedItem = _lastSelectedItem;
        }
        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (ProductViewModelBase)e.ClickedItem;
            if (clickedItem.WholeSellerId != null)
                AddToCartBtnVisibility(false);
            else
                AddToCartBtnVisibility(true);

            this.PriceQuotedByWholeSellerCollection =
                new PriceQuotedByWholeSellerCollection(AnalyticsDataSource.GetWholeSellersForProduct(clickedItem.ProductId));
            DetailContentPresenter.Content = this.PriceQuotedByWholeSellerCollection;
            // Play a refresh animation when the user switches detail items.
            EnableContentTransitions();
        }

        private void AddToCartBtnVisibility(bool visibilty)
        {
            if (visibilty == true)
            {
                AddToCartBtn.Visibility = Visibility.Visible;
                GoToCartBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                AddToCartBtn.Visibility = Visibility.Collapsed;
                GoToCartBtn.Visibility = Visibility.Visible;
            }
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
