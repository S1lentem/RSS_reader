using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using RSSController;
using Windows.UI.Popups;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace RSSReader
{

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string defaultRSS = "https://news.tut.by/rss/society.rss";

        public MainPage()
        {
            this.InitializeComponent();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1254");
        }

        private async void LoadButtonClick(object sender, RoutedEventArgs e)
        {
            RSSFeedController feedController = new RSSFeedController(defaultRSS);
            var feeds = await feedController.GetFeeds();
            RssFeeds.ItemsSource = feeds;
        }
    }
}
