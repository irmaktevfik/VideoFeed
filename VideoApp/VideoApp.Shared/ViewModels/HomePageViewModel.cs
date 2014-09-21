using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using VideoApp.Common;
using VideoAppModels;
using VideoApp.Business.Helpers;

namespace VideoApp.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        #region change handling
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            //Fire the PropertyChanged event in case somebody subscribed to it
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private ObservableCollection<VideoFeeds> _videoFeed;
        public ObservableCollection<VideoFeeds> VideoFeed
        {
            get
            {
                return this._videoFeed;
            }
        }

        private RelayCommand _searchVideos;

        public HomePageViewModel()
        {
            _videoFeed = new ObservableCollection<VideoFeeds>();
            GetData();
        }

        private async Task GetData()
        {
            var data = await Business.VideoManager.GetAll();
            if (data != null)
                _videoFeed = data.ToObservableCollection();
        }

        #region Commands
        public RelayCommand SearchVideos
        {
            get
            {
                if (_searchVideos == null)
                {
                    _searchVideos = new RelayCommand(() => this.Search(), () => this.CanSearch());
                }
                return _searchVideos;
            }
            set
            {
                _searchVideos = value;
            }
        }
        #endregion

        private bool CanSearch()
        {
            //any logic  to prevent Searching
            return true;
        }

        private async void Search()
        {
            var data = await Business.VideoManager.GetAll();
            if (data != null)
            {
                _videoFeed = data.ToObservableCollection();
                this.OnPropertyChanged("VideoFeed");
            }
        }
    }
}
