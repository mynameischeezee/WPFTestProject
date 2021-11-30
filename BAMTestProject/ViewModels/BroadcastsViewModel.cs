using System;
using System.Collections.ObjectModel;
using System.Linq;
using BAMTestProject.BL.Implement.ModelServices;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Models;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class BroadcastsViewModel : Screen
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly BroadcastModelService _broadcastsModelService;
        private ObservableCollection<Broadcast> _broadcastsList;
        private Broadcast _selectedBroadcast;
        private ObservableCollection<string> _addShowsList;
        private Show _addSelectedShow;
        private ObservableCollection<string> _addMarketsList;
        private Market _addSelectedMarket;
        private DateTime _addStartDate;
        private DateTime _addEndDate;
        private string _addBroadcastViewsCount;

        public ObservableCollection<DayOfWeekViewModel> DaysOfWeek { get; set; }

        public BroadcastsViewModel(ApplicationDbContext dbContext, BroadcastModelService broadcastsModelService)
        {
            _dbContext = dbContext;
            _broadcastsModelService = broadcastsModelService;
            AddShowsList = new ObservableCollection<string>(dbContext.Shows.Select(x => x.Name));
            AddMarketsList = new ObservableCollection<string>(dbContext.Markets.Select(x => x.Name));
            AddStartDate = DateTime.Today;
            AddEndDate = DateTime.Today;
            var days = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(x => new DayOfWeekViewModel() { DayOfWeek = x });
            DaysOfWeek = new ObservableCollection<DayOfWeekViewModel>(days);
        }
        public Broadcast SelectedBroadcast
        {
            get => _selectedBroadcast;
            set => Set(ref _selectedBroadcast, value, nameof(SelectedBroadcast));
        }

        public ObservableCollection<Broadcast> BroadcastsList
        {
            get
            {
                _broadcastsList = new ObservableCollection<Broadcast>(_dbContext.Broadcasts);
                return _broadcastsList;
            }
            set => Set(ref _broadcastsList, value, nameof(BroadcastsList));
        }

        public ObservableCollection<string> AddShowsList
        {
            get => _addShowsList;
            set => Set(ref _addShowsList, value, nameof(AddShowsList));
        }

        public ObservableCollection<string> AddMarketsList
        {
            get => _addMarketsList;
            set => Set(ref _addMarketsList, value, nameof(AddMarketsList));
        }

        public string AddSelectedShow
        {
            get => _addSelectedShow == null ? " " : _addSelectedShow.Name;
            set
            {
                _addSelectedShow = _dbContext.Shows.First(x => x.Name == value);
                NotifyOfPropertyChange(() => AddSelectedShow);
                NotifyOfPropertyChange(() => CanAddBroadcast);
            }
        }

        public string AddSelectedMarket
        {
            get => _addSelectedMarket == null ? " " : _addSelectedMarket.Name;
            set
            {
                _addSelectedMarket = _dbContext.Markets.First(x => x.Name == value);
                NotifyOfPropertyChange(() => AddSelectedMarket);
                NotifyOfPropertyChange(() => CanAddBroadcast);
            }
        }

        public DateTime AddStartDate
        {
            get => _addStartDate;
            set => Set(ref _addStartDate, value, nameof(AddStartDate));
        }

        public DateTime AddEndDate
        {
            get => _addEndDate;
            set => Set(ref _addEndDate, value, nameof(AddEndDate));
        }

        public string AddBroadcastViewsCount
        {
            get => Convert.ToString(_addBroadcastViewsCount);
            set
            {
                Set(ref _addBroadcastViewsCount, value, nameof(AddBroadcastViewsCount));
                NotifyOfPropertyChange(() => CanAddBroadcast);
            }
        }

        public void AddBroadcast()
        {
            Broadcast broadcastToAdd = new Broadcast()
            {
                MarketId = _addSelectedMarket.Id,
                ShowId = _addSelectedShow.Id,
                ShowsAmount = Convert.ToInt32(AddBroadcastViewsCount),
                EndDate = AddEndDate,
                StartDate = AddStartDate,
                Days = DaysOfWeek.Where(x => x.IsSelected).Select(x => x.DayOfWeek).ToList()
            };
            _broadcastsModelService.Insert(broadcastToAdd);
            NotifyOfPropertyChange(() => BroadcastsList);
        }

        public bool CanAddBroadcast =>
            AddSelectedShow != null && AddMarketsList != null &&
            !string.IsNullOrWhiteSpace(AddBroadcastViewsCount) &&
            int.TryParse(AddBroadcastViewsCount, out _);
    }
}
