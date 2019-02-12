using Windows.UI.Xaml.Controls;

using RSSController;
using System.Text;
using Windows.UI.Xaml.Navigation;
using RSSReader.Interfaces;
using System;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace RSSReader
{

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class NewsPage : Page
    {
        public NewsPage()
        {
            this.InitializeComponent();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1254");
           
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is IRSSFeedsManageable manageable)
            {
                RssFeeds.ItemsSource = await manageable.GetRssFeeds();
            }
            else
            {
                throw new ArgumentException("NavigationEvenetArgs.Parameter not implemented IRSSFeedsManageable");
            }
           
            base.OnNavigatedTo(e);
        }
    }
}
