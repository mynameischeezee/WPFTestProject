using System;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly BroadcastsViewModel _broadcastsViewModel;
        private readonly ShowsViewModel _showsViewModel;
        private readonly MarketsViewModel _marketsViewModel;
        private Screen _viewsContentControl;
        private bool _showsView;
        private bool _marketsView;
        private bool _broadcastsView;

        public MainViewModel(BroadcastsViewModel broadcastsViewModel, ShowsViewModel showsViewModel,
            MarketsViewModel marketsViewModel)
        {
            _broadcastsViewModel = broadcastsViewModel;
            _marketsViewModel = marketsViewModel;
            _showsViewModel = showsViewModel;
        }

        public Screen ViewsContentControl
        {
            get => _viewsContentControl;
            set => Set(ref _viewsContentControl, value, nameof(ViewsContentControl));
        }

        public bool ShowsView
        {
            get => _showsView;
            set
            {
                ViewsContentControl = _showsViewModel;
                Set(ref _showsView, value, nameof(ShowsView));
            }
        }

        public bool MarketsView
        {
            get => _marketsView;
            set
            {
                ViewsContentControl = _marketsViewModel;
                Set(ref _marketsView, value, nameof(MarketsView));
            }
        }

        public bool BroadcastsView
        {
            get => _broadcastsView;
            set
            {
                ViewsContentControl = _broadcastsViewModel;
                Set(ref _broadcastsView, value, nameof(BroadcastsView));
            }
        }

        public void CloseWindow()
        {
            Environment.Exit(0);
        }
    }
}
