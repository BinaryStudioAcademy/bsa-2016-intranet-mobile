﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MvvmCross.WindowsUWP.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Intranet.WindowsUWP.Views.News
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [MvxRegion("MainContent")]
    public sealed partial class AllNewsPage : BasePage
    {
        public AllNewsPage()
        {
            this.InitializeComponent();
        }

        private void Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tabs.SelectedIndex == 0)
            {
                RefreshCompanyNewsBtn.Visibility = Visibility.Visible;
                RefreshWeekliesBtn.Visibility = Visibility.Collapsed;
            }
            else if (Tabs.SelectedIndex == 1)
            {
                RefreshCompanyNewsBtn.Visibility = Visibility.Collapsed;
                RefreshWeekliesBtn.Visibility = Visibility.Visible;
            }
        }
    }
}
