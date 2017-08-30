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
        public PriceQuotedByWholeSellerCollection PriceQuotedByWholeSellerCollection { get; set; }
        public static ProductInStock Current;
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
            base.OnNavigatedTo(e);
            UpdateForVisualState(AdaptiveStates.CurrentState);
            await UpdateMasterListViewItemSourceByFilterCriteria();
            DisableContentTransitions();
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
            var items = products.Select(p => new ProductViewModelBase(p)).ToList();
            MasterListView.ItemsSource = items;
            var totalResults = items;
            //TODO: get total quantity
            ProductCountTB.Text = "(" + totalResults.ToString() + "/" + "xxxx" + ")";
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


            this.PriceQuotedByWholeSellerCollection =
                new PriceQuotedByWholeSellerCollection(AnalyticsDataSource.GetWholeSellersForProduct(clickedItem.ProductId));
            DetailContentPresenter.Content = this.PriceQuotedByWholeSellerCollection;
            MainPage.Current.NavigateNewsFeedFrame(typeof(ProductFormCC), clickedItem);

            this.ProductStockSelectionChangedEvent?.Invoke(clickedItem);

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
