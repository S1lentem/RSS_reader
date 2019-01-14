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
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace RSSReader
{

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class NewsPage : Page
    {
        private RSSFeedController rssFeedController;

        public NewsPage()
        {
            this.InitializeComponent();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1254");
           
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.rssFeedController = e.Parameter as RSSFeedController;
            RssFeeds.ItemsSource = await rssFeedController.GetFeeds();

            base.OnNavigatedTo(e);
        }
    }
}
