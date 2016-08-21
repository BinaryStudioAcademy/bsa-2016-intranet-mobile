using System;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Intranet.WindowsUWP.Views.Controls
{
    public sealed partial class MenuItemControl : UserControl
    {
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public string IconUri
        {
            get { return (string)GetValue(IconUriProperty); }
            set { SetValue(IconUriProperty, value); }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string),
              typeof(MenuItemControl), new PropertyMetadata(""));

        /// <summary>
        /// Identified the IconUri dependency property
        /// </summary>
        public static readonly DependencyProperty IconUriProperty =
            DependencyProperty.Register("IconUri", typeof(string),
              typeof(MenuItemControl), new PropertyMetadata(""));

        public MenuItemControl()
        {
            this.InitializeComponent();
        }
    }
}
