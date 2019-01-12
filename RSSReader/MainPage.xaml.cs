using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using RSSController;
using System.Text;
using System.Collections.Generic;
using Windows.UI.Popups;
using System;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace RSSReader
{

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IEnumerable<RSSFeed> listRssFeeds;

        public MainPage()
        {
            this.InitializeComponent();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1254");
        }

        private async void LoadButtonClick(object sender, RoutedEventArgs e)
        {
            var link = RssFeedsLink.Text.CreateLink();

            RSSFeedController feedController = new RSSFeedController(link);

            try
            {
                var feeds = await feedController.GetFeeds();
                listRssFeeds = feeds;

            }
            catch (Exception ex) when (ex is  UriFormatException || ex is NotRssFeedsLinkException)
            {
                var dialog = new MessageDialog("Invalid link");
                dialog.ShowAsync();
            }
            finally
            {
                RssFeeds.ItemsSource = listRssFeeds;
            }
        }
    }
}
