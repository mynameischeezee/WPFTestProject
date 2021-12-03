using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BAMTestProject.BL.Implementation.BaseRepositories;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Entities;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class BroadcastsViewModel : Screen
    {
        private readonly IBaseRepository<ShowEntity> _showRepository;
        private readonly IBaseRepository<MarketEntity> _marketRepository;
        private readonly IBaseRepository<BroadcastEntity> _broadcastRepository;
        private ObservableCollection<BroadcastEntity> _broadcastsList;
        private BroadcastEntity _selectedBroadcast;
        private ObservableCollection<string> _addShowsList;
        private ShowEntity _addSelectedShow;
        private ObservableCollection<string> _addMarketsList;
        private MarketEntity _addSelectedMarket;
        private DateTime _addStartDate;
        private DateTime _addEndDate;
        private string _addBroadcastViewsCount;
        private string _endDates;

        public ObservableCollection<DayOfWeekViewModel> DaysOfWeek { get; set; }

        //TODO: Create broadcastVM (another one)
        public BroadcastsViewModel(IBaseRepository<MarketEntity> marketRepository, IBaseRepository<BroadcastEntity> broadcastRepository, IBaseRepository<ShowEntity> showRepository)
        {
            _marketRepository = marketRepository;
            _broadcastRepository = broadcastRepository;
            _showRepository = showRepository;
            AddStartDate = DateTime.Today;
            AddEndDate = DateTime.Today;
            DaysOfWeek = new ObservableCollection<DayOfWeekViewModel>(CreateDayOfWeekViewModels());
            Update();
        }

        public BroadcastEntity SelectedBroadcast
        {
            get => _selectedBroadcast;
            set => Set(ref _selectedBroadcast, value, nameof(SelectedBroadcast));
        }

        public ObservableCollection<BroadcastEntity> BroadcastsList
        {
            get
            {
                _broadcastsList = _broadcastRepository.GetAll();
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
                    _addSelectedShow = _showRepository.GetAll().First(x => x.Name == value);
                }
                catch
                {
                    _addSelectedShow = new ShowEntity();
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
                    _addSelectedMarket = _marketRepository.GetAll().First(x => x.Name == value);
                }
                catch
                {
                    _addSelectedMarket = new MarketEntity();
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
            var broadcastToAdd = new BroadcastEntity
            {
                MarketId = _addSelectedMarket.Id,
                ShowId = _addSelectedShow.Id,
                ShowsAmount = Convert.ToInt32(AddBroadcastViewsCount),
                EndDate = AddEndDate,
                StartDate = AddStartDate,
                Days = DaysOfWeek.Where(x => x.IsSelected).Select(x => x.DayOfWeek).ToList()
            };
            _broadcastRepository.Insert(broadcastToAdd);
            Update();
        }

        public bool CanAddBroadcast =>
            AddSelectedShow != null && AddMarketsList != null && !string.IsNullOrWhiteSpace(AddBroadcastViewsCount) &&
            int.TryParse(AddBroadcastViewsCount, out _);

        public void DeleteBroadcast()
        {
            _broadcastRepository.Delete(SelectedBroadcast.Id);
            Update();
        }

        //TODO: fix start date edit bug
        public void EditBroadcast()
        {
            SelectedBroadcast.Days = DaysOfWeek.Where(x => x.IsSelected).Select(x => x.DayOfWeek).ToList();
            _broadcastRepository.Edit(_selectedBroadcast.Id, SelectedBroadcast);
            Update();
        }

        public void Update()
        {
            NotifyOfPropertyChange(() => BroadcastsList);
            AddShowsList = new ObservableCollection<string>(_showRepository.GetAll().Select(x => x.Name));
            AddMarketsList = new ObservableCollection<string>(_marketRepository.GetAll().Select(x => x.Name));
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
