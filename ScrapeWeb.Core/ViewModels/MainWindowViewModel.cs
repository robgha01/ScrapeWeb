using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using AlphaChiTech.Virtualization;
using ScrapeWeb.Core.Annotations;
using ScrapeWeb.Core.Bakabt;
using ScrapeWeb.Core.Bakabt.Factories;

namespace ScrapeWeb.Core.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private VirtualizingObservableCollection<PostEntity> _lastPosts;
        private int _pageSize;
        private int _maxPagesInCache;
        private string _lastPostsRssUrl;
        private ICollectionView _categories;
        private ICollectionView _generes;

        public event PropertyChangedEventHandler PropertyChanged;

        public string LastPostsRssUrl
        {
            get { return _lastPostsRssUrl; }
            set
            {
                _lastPostsRssUrl = value;
                OnPropertyChanged();
                OnPropertyChanged("LastPosts");
            }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                OnPropertyChanged();
            }
        }
        
        public int MaxPagesInCache
        {
            get { return _maxPagesInCache; }
            set
            {
                _maxPagesInCache = value;
                OnPropertyChanged();
            }
        }

        public VirtualizingObservableCollection<PostEntity> LastPosts
        {
            get
            {
                if (_lastPosts == null && !string.IsNullOrEmpty(LastPostsRssUrl))
                {
                    var postUrlFactory = new LastPostsUrlFactory(new MediaManager.MediaManager());
                    var postUrls = postUrlFactory.Create(LastPostsRssUrl);
                    _lastPosts = new VirtualizingObservableCollection<PostEntity>(new PaginationManager<PostEntity>(new PostsSourceProvider(postUrls), pageSize: PageSize, maxPages: MaxPagesInCache));

                    OnPropertyChanged();
                }
                return _lastPosts;
            }
        }

        public ICollectionView Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView Generes
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            PageSize = 10;
            MaxPagesInCache = 100;
        }

        public void LoadLastPosts()
        {
            if (_lastPosts == null)
            {
                return;
            }
            _lastPosts.Clear();
            _lastPosts = null;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}