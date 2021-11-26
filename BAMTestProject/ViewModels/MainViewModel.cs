using System;
using System.ComponentModel;
using System.Linq;
using BAMTestProject.DAL.Implementation;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class MainViewModel : Screen
    {
        public ApplicationDbContext DbContext { get; }
        private readonly BroadcastsViewModel _broadcastsViewModel;
        private readonly ShowsViewModel _showsViewModel;
        private readonly MarketsViewModel _marketsViewModel;

        public MainViewModel(BroadcastsViewModel broadcastsViewModel, ShowsViewModel showsViewModel,
            MarketsViewModel marketsViewModel, ApplicationDbContext dbContext)
        {
            _broadcastsViewModel = broadcastsViewModel;
            _marketsViewModel = marketsViewModel;
            _showsViewModel = showsViewModel;
            DbContext = dbContext;
        }

        private Screen _viewsContentControl;

        public Screen ViewsContentControl
        {
            get => _viewsContentControl;
            set
            {
                _viewsContentControl = value;
                NotifyOfPropertyChange(() => ViewsContentControl);
            }
        }

        private bool _broadcastsView;

        public bool BroadcastsView
        {
            get
            {
                ViewsContentControl = _broadcastsViewModel;
                return _broadcastsView;
            }
            set
            {
                _broadcastsView = value;
                ViewsContentControl = _broadcastsViewModel;
                NotifyOfPropertyChange(() => BroadcastsView);
            }
        }


        private bool _showsView;

        public bool ShowsView
        {
            get
            {
                ViewsContentControl = _showsViewModel;
                return _showsView;
            }
            
            set
            {
                _showsView = value;
                ViewsContentControl = _showsViewModel;
                NotifyOfPropertyChange(() => ShowsView);
            }
        }

        private bool _marketsView;

        public bool MarketsView
        {
            get
            {
                ViewsContentControl = _marketsViewModel;
                return _marketsView;
            }
            set
            {
                _marketsView = value;
                ViewsContentControl = _marketsViewModel;
                NotifyOfPropertyChange(() => MarketsView);
            }
        }

        public void Close()
        {
            Environment.Exit(0);
        }
    }
}
