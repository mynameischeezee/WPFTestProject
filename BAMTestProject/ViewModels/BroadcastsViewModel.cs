using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BAMTestProject.BL.Abstract.Services;
using BAMTestProject.BL.Implement.ModelServices;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Entities;
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
        private string _endDates;
        private IBroadcastEndDateCalculator _calculationService;

        public ObservableCollection<DayOfWeekViewModel> DaysOfWeek { get; set; }
        //TODO: Create broadcastVM (another one)
        public BroadcastsViewModel(ApplicationDbContext dbContext, BroadcastModelService broadcastsModelService, IBroadcastEndDateCalculator calculationService)
        {
            _dbContext = dbContext;
            _broadcastsModelService = broadcastsModelService;
            _calculationService = calculationService;
            AddShowsList = new ObservableCollection<string>(_dbContext.Shows.Select(x => x.Name));
            AddMarketsList = new ObservableCollection<string>(_dbContext.Markets.Select(x => x.Name));
            AddStartDate = DateTime.Today;
            AddEndDate = DateTime.Today;
            DaysOfWeek = new ObservableCollection<DayOfWeekViewModel>(CreateDayOfWeekViewModels());
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
                _broadcastsList = new ObservableCollection<Broadcast>();
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
                try
                {
                    _addSelectedShow = _dbContext.Shows.First(x => x.Name == value);
                }
                catch
                {
                    _addSelectedShow = new Show();
                }

                NotifyOfPropertyChange(() => AddSelectedShow);
                NotifyOfPropertyChange(() => CanAddBroadcast);
            }
        }

        public string AddSelectedMarket
        {
            get => _addSelectedMarket == null ? " " : _addSelectedMarket.Name;
            set
            {
                try
                {
                    _addSelectedMarket = _dbContext.Markets.First(x => x.Name == value);
                }
                catch
                {
                    _addSelectedMarket = new Market();
                }

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
            var broadcastToAdd = new Broadcast()
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
            AddSelectedShow != null && AddMarketsList != null && !string.IsNullOrWhiteSpace(AddBroadcastViewsCount) &&
            int.TryParse(AddBroadcastViewsCount, out _);

        public void DeleteBroadcast()
        {
            _broadcastsModelService.Delete(SelectedBroadcast.Id);
            NotifyOfPropertyChange(() => BroadcastsList);
        }

        public void Update()
        {
            NotifyOfPropertyChange(() => BroadcastsList);
            AddShowsList = new ObservableCollection<string>(_dbContext.Shows.Select(x => x.Name));
            AddMarketsList = new ObservableCollection<string>(_dbContext.Markets.Select(x => x.Name));
        }

        public IEnumerable<DayOfWeekViewModel> CreateDayOfWeekViewModels()
        {
            var days = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(DayOfWeekViewModel.Create);
            return days;
        }

        public string EndDates
        {
            get => _endDates;
            set => Set(ref _endDates, value, nameof(EndDates));
        }
    }
}
