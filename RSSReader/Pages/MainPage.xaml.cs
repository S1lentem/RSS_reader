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

using RSSReader.Pages;
using RSSController;
// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace RSSReader.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly RSSFeedController feedController;

        public MainPage()
        {
            this.InitializeComponent();
            this.feedController = new RSSFeedController();
        }

        private void SelectItemFromMenu(object sender, SelectionChangedEventArgs e)
        {
            if (homeItem.IsSelected)
            {
                contentFrame.Navigate(typeof(HomePage));
            
            } 
            else if (newsItem.IsSelected)
            {
                contentFrame.Navigate(typeof(NewsPage), feedController);
            }
            else if (settingsItem.IsSelected)
            {
                contentFrame.Navigate(typeof(SettingsPage), feedController);
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
