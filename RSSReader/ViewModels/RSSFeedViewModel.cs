using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RSSReader.ViewModels
{
    class RSSFeedViewModel : INotifyPropertyChanged
    {
        private string link;
        private string title;
        
        public string Link {
            get => link;
            set
            {
                link = value;
                OnPropretyChanged(nameof(Link));    
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropretyChanged(nameof(Title));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropretyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
