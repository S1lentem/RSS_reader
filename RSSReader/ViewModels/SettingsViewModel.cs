using RSSController.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RSSReader.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        
        private RSSFeed selectedFeed;
        
        public RSSFeed SelectedFeed
        {
            get => selectedFeed;
            set
            {
                selectedFeed = value;
                OnPropertyChaged(nameof(SelectedFeed));
            }
        }

        public ObservableCollection<RSSFeedViewModel> AllFeeds { get; }

        public SettingsViewModel(IEnumerable<RSSFeed> allFeeds, RSSFeed current)
        {
            var allFeedsVeiwModels = allFeeds.Select(item => new RSSFeedViewModel()
            {
                Title = item.Title,
                Link = item.Link
            });
            AllFeeds = new ObservableCollection<RSSFeedViewModel>(allFeedsVeiwModels);
            SelectedFeed = allFeeds.First(item => item.Link == current.Link && item.Title == current.Title);

            var we = 1;
        }

        // INotifyPropertyChanged realisation
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChaged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
