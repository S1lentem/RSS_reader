using RSSController;
using RSSReader.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace RSSReader.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private IRSSFeedsManageable manageable;
        private IEnumerable<string> allLinks;

        public SettingsPage()
        {
            this.InitializeComponent();
            allRSSFeedsSources.ItemsSource = allLinks;
        }

        private void RssFeedTextBlockKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                manageable.SetNewsSource(rssFeedTextBox.Text.CreateLink());

                // Removes keyboard when pressed enter
                Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.IsInputEnabled = false;
                Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.IsInputEnabled = true;   
            }
        }

        private void SelectRSSFeedsSource(object sender, SelectionChangedEventArgs e)
        {
            var source = e.AddedItems[0].ToString();
            manageable.SetNewsSource(source);
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is IRSSFeedsManageable manageable)
            {
                this.manageable = manageable;
                var url = manageable.GetCurrentNewsSource();
                if (url != null)
                {
                    rssFeedTextBox.Text = url;
                }

                allLinks = manageable.GetAllRSSFeeds()
                    ?.Select(item => item.Link);
               
            }
            else
            {
                throw new ArgumentException("NavigationEvenetArgs.Parameter not implemented IRSSFeedsManageable");
            }
            base.OnNavigatedTo(e);
        }

        
    }
}
