using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using RSSController;
using System.Collections.Generic;
using System.Threading.Tasks;

using RSSController.Interfaces;
using Infrastructure.Storages.EF;
using System.Linq;
using RSSReader.Source;
using RSSController.Models;
using System;
using RSSReader.Interfaces;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace RSSReader.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page, IRSSFeedsManageable
    {
        private readonly RSSFeedController feedController;

        private readonly IRSSFeedsCacheRepository cacheRepository;
        private readonly IRSSFeedsRepository feedsRepository;

        public MainPage()
        {
            this.InitializeComponent();
            this.cacheRepository = new SQLiteRSSFeedsCacheRepository();
            this.feedController = new RSSFeedController();
            this.feedsRepository = new SQLiteRSSFeedsRepository();
        }

      

        public async Task<IEnumerable<RSSFeedItem>> GetRSSFeedItems()
        {
            switch (ConnectionManager.GetCurrentConnection())
            {
                case TypeConnection.NotСonnection:
                    return cacheRepository.GetFromCache();
                case TypeConnection.Mobile:
                    // TO DO: Add logic 
                case TypeConnection.WiFi:
                    var rssFeed = feedsRepository.GetCurrentRSSFeed();
                    var url = rssFeed?.Link;
                    if (url != null)
                    {
                        feedController.URL = url;
                    }
                    var allFeeds = await feedController.GetFeeds() ?? new List<RSSFeedItem>();
                    cacheRepository.ClearCache();
                    cacheRepository.PushInCache(allFeeds.ToArray());
                    return allFeeds;
            }
            return null;
        }

        
        private void SelectItemFromMenu(object sender, SelectionChangedEventArgs e)
        {
            if (homeItem.IsSelected)
            {
                contentFrame.Navigate(typeof(HomePage));
            } 
            else if (newsItem.IsSelected)
            {
                contentFrame.Navigate(typeof(NewsPage), this);
            }
            else if (settingsItem.IsSelected)
            {
                contentFrame.Navigate(typeof(SettingsPage), feedsRepository);
            }
            else if (aboutItem.IsSelected)
            {
                contentFrame.Navigate(typeof(AboutPage));
            }
            menu.IsPaneOpen = false;
        }

        private void ShowMenuClick(object sender, RoutedEventArgs e) => menu.IsPaneOpen = !menu.IsPaneOpen;
    }
}
