using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using RSSController;
using RSSReader.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

using RSSController.Interfaces;
using Infrastructure.Storages.EF;
using System.Linq;

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

        public MainPage()
        {
            this.InitializeComponent();
            this.cacheRepository = new SQLiteRSSFeedsCacheRepository();
            this.feedController = new RSSFeedController();
            
        }

        public async Task<IEnumerable<RSSFeedItem>> GetRssFeedAsync()
        {
            var allFeeds = await feedController.GetFeeds() ?? new List<RSSFeedItem>();
            cacheRepository.PushInCache(allFeeds.ToArray());
            return allFeeds;
        }

        public void SetNewsSource(string url) => feedController.URL = url;
        

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
                contentFrame.Navigate(typeof(SettingsPage), this);
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
