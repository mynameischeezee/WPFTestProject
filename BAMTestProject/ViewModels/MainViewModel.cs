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

        private bool _showsView;

        public bool ShowsView
        {
            get => _showsView;
            set
            {
                ViewsContentControl = _showsViewModel;
                _showsView = value;
                NotifyOfPropertyChange(() => ShowsView);
            }
        }

        private bool _marketsView;

        public bool MarketsView
        {
            get => _marketsView;
            set
            {
                ViewsContentControl = _marketsViewModel;
                _marketsView = value;
                NotifyOfPropertyChange(() => MarketsView);
            }
        }

        private bool _broadcastsView;

        public bool BroadcastsView
        {
            get => _broadcastsView;
            set
            {
                ViewsContentControl = _broadcastsViewModel;
                _broadcastsView = value;
                NotifyOfPropertyChange(() => BroadcastsView);
            }
        }

        public void Close()
        {
            Environment.Exit(0);
        }
    }
}
