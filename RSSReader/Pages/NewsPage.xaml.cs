using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using RSSController;
using System.Text;
using System.Collections.Generic;
using Windows.UI.Popups;
using System;
using System.Net.Http;
using System.Xml;
using Windows.System;
using Windows.UI.Xaml.Input;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace RSSReader
{

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class NewsPage : Page
    {
        private IEnumerable<RSSFeed> listRssFeeds;

        public NewsPage()
        {
            this.InitializeComponent();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1254");
        }

        private async void RssSourceKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                var link = RssFeedsLink.Text.CreateLink();

                // Removes keyboard when pressed enter
                Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.IsInputEnabled = false;
                Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.IsInputEnabled = true;

                RSSFeedController feedController = new RSSFeedController(link);

                try
                {
                    var feeds = await feedController.GetFeeds();
                    listRssFeeds = feeds;

                }
                catch (Exception ex) when (ex is UriFormatException || ex is HttpRequestException || ex is XmlException)
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
}
