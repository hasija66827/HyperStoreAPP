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

    public sealed partial class Scenario5_Clear : Page
    {
        public Scenario5_Clear()
        {
            this.InitializeComponent();
            FilterAppBarButton.Click += FilterAppBarButton_Click;
            ApplyFilter.Click += ApplyFilter_Click;
        }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IRange<float> discounPerRange = new IRange<float>(Convert.ToSingle(DiscountPerLB.Text), Convert.ToSingle(DiscountPerUB.Text));
                IRange<Int32> quantityRange = new IRange<Int32>(Convert.ToInt32(QuantityLB.Text), Convert.ToInt32(QuantityUB.Text));
                FilterProductCriteria filterProductCriteria = new FilterProductCriteria(discounPerRange, quantityRange, ShowDeficientItemsOnly.IsChecked);
                UpdateMasterListViewItemSource(filterProductCriteria);
            }
            catch (Exception )
            {
                Console.Write(e.ToString());
                //TODO: Invalid Percentage
            }
            MasterListView.Visibility = Visibility.Visible;
            FilterPanel.Visibility = Visibility.Collapsed;
            FilterAppBarButton.Visibility = Visibility.Visible;
            ClearAppBarButton.Visibility = Visibility.Collapsed;
        }

        private void FilterAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            FilterAppBarButton.Visibility = Visibility.Collapsed;
            ClearAppBarButton.Visibility = Visibility.Visible;
            MasterListView.Visibility = Visibility.Collapsed;
            FilterPanel.Visibility = Visibility.Visible;
        }
        //Will Update the MasterListView by filtering out Products on the basis of specific filter criteria.
        private void UpdateMasterListViewItemSource(FilterProductCriteria filterProductCriteria = null)
        {
            //If filterProductCriteria is null then return whole list.
            if (filterProductCriteria == null)
                MasterListView.ItemsSource = ProductDataSource.Products;
            else
            {
                var items = ProductDataSource.GetProducts(filterProductCriteria);
                MasterListView.ItemsSource = items;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            /*#perf: Highly Database intensive operation and is called eevery time when we navigate to this page*/
            //#Optimization can be, if no customer has been recently added, then don't refetch again. Or with database updation we can update our all the data source as well, i.e our cache.
            // Hence we can call this function one time only in the constructor, instead of calling it everytime on page navigation. 
            //Called every time on navigation
            ProductDataSource.RetrieveProductDataAsync();
            MasterListView.ItemsSource = ProductDataSource.Products;
            UpdateForVisualState(AdaptiveStates.CurrentState);
            // Don't play a content transition for first item load.
            // Sometimes, this content will be animated as part of the page transition.
            DisableContentTransitions();
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
            var clickedItem = (ProductViewModel)e.ClickedItem;
            
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
