using RSSController;
using RSSController.Interfaces;
using RSSController.Models;
using RSSReader.Interfaces;
using RSSReader.ViewModels;
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
        private IRSSFeedsRepository feedsRepository;


        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void AddFeed(object sender, EventArgs e)
        {
            var link = rssFeedTextBox.Text.CreateLink();
            
        }

        private void RssFeedTextBlockKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                feedsRepository.SetCurrentByURL(rssFeedTextBox.Text.CreateLink());

                // Removes keyboard when pressed enter
                Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.IsInputEnabled = false;
                Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.IsInputEnabled = true;   
            }
        }

        private void SelectRSSFeedsSource(object sender, SelectionChangedEventArgs e)
        {
            var link = e.AddedItems[0].ToString();
            feedsRepository.SetCurrentByURL(link);
        }

        public async void AddLinkForRssFeeds(object sender, RoutedEventArgs e)
        {
            var link = rssFeedTextBox.Text.CreateLink();      
            var feed = await feedsRepository.CreateRSSFeedFromLink(link);
            if (feed != null)
            {
                feedsRepository.AddRSSFeed(feed.Title, feed.Link);
            }
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is IRSSFeedsRepository repository)
            {
                feedsRepository = repository;
                var allRSSFeeds = repository.GetAllRSSFeeds();
                var currentRSSFeed = repository.GetCurrentRSSFeed();

                DataContext = new SettingsViewModel(allRSSFeeds, currentRSSFeed);
            }

            else
            {
                throw new ArgumentException("NavigationEvenetArgs.Parameter not implemented IRSSFeedsManageable");
            }
          
            base.OnNavigatedTo(e);
        }

        
    }
}
