using System;
using System.Collections.Generic;
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
        private bool _addMondayBroadcast;
        private bool _addTuesdayBroadcast;
        private bool _addWednesdayBroadcast;
        private bool _addThursdayBroadcast;
        private bool _addFridayBroadcast;
        private bool _addSaturdayBroadcast;
        private bool _addSundayBroadcast;
        private string _addBroadcastViewsCount;
        private readonly List<DayOfWeek> _broadcastDays;

        public BroadcastsViewModel(ApplicationDbContext dbContext, BroadcastModelService broadcastsModelService)
        {
            _dbContext = dbContext;
            _broadcastsModelService = broadcastsModelService;
            AddShowsList = new ObservableCollection<string>(dbContext.Shows.Select(x => x.Name));
            AddMarketsList = new ObservableCollection<string>(dbContext.Markets.Select(x => x.Name));
            _broadcastDays = new List<DayOfWeek>();
            AddStartDate = DateTime.Today;
            AddEndDate = DateTime.Today;
        }
        public Broadcast SelectedBroadcast
        {
            get => _selectedBroadcast;
            set
            {
                _selectedBroadcast = value;
                NotifyOfPropertyChange(() => SelectedBroadcast);
            }
        }

        public ObservableCollection<Broadcast> BroadcastsList
        {
            get
            {
                _broadcastsList = new ObservableCollection<Broadcast>(_dbContext.Broadcasts);
                return _broadcastsList;
            }
            set
            {
                _broadcastsList = value;
                NotifyOfPropertyChange(() => BroadcastsList);
            }
        }

        public ObservableCollection<string> AddShowsList
        {
            get => _addShowsList;
            set
            {
                _addShowsList = value;
                NotifyOfPropertyChange(() => AddShowsList);
            }
        }

        public ObservableCollection<string> AddMarketsList
        {
            get => _addMarketsList;
            set
            {
                _addMarketsList = value;
                NotifyOfPropertyChange(() => AddMarketsList);
            }
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
            set
            {
                _addStartDate = value;
                NotifyOfPropertyChange(() => AddStartDate);
            }
        }

        public DateTime AddEndDate
        {
            get => _addEndDate;
            set
            {
                _addEndDate = value;
                NotifyOfPropertyChange(() => AddEndDate);
            }
        }

        public bool AddMondayBroadcast
        {
            get => _addMondayBroadcast;
            set
            {
                _addMondayBroadcast = value;
                NotifyOfPropertyChange(() => AddMondayBroadcast);
                if (_addMondayBroadcast)
                {
                    _broadcastDays.Add(DayOfWeek.Monday);
                }
                else
                {
                    _broadcastDays.Remove(DayOfWeek.Monday);
                }
            }
        }

        public bool AddTuesdayBroadcast
        {
            get => _addTuesdayBroadcast;
            set
            {
                _addTuesdayBroadcast = value;
                NotifyOfPropertyChange(() => AddTuesdayBroadcast);
                if (_addTuesdayBroadcast)
                {
                    _broadcastDays.Add(DayOfWeek.Tuesday);
                }
                else
                {
                    _broadcastDays.Remove(DayOfWeek.Tuesday);
                }
            }
        }

        public bool AddWednesdayBroadcast
        {
            get => _addWednesdayBroadcast;
            set
            {
                _addWednesdayBroadcast = value;
                NotifyOfPropertyChange(() => AddWednesdayBroadcast);
                if (_addWednesdayBroadcast)
                {
                    _broadcastDays.Add(DayOfWeek.Wednesday);
                }
                else
                {
                    _broadcastDays.Remove(DayOfWeek.Wednesday);
                }
            }
        }

        public bool AddThursdayBroadcast
        {
            get => _addThursdayBroadcast;
            set
            {
                _addThursdayBroadcast = value;
                NotifyOfPropertyChange(() => AddThursdayBroadcast);
                if (_addThursdayBroadcast)
                {
                    _broadcastDays.Add(DayOfWeek.Thursday);
                }
                else
                {
                    _broadcastDays.Remove(DayOfWeek.Thursday);
                }
            }
        }

        public bool AddFridayBroadcast
        {
            get => _addFridayBroadcast;
            set
            {
                _addFridayBroadcast = value;
                NotifyOfPropertyChange(() => AddFridayBroadcast);
                if (_addFridayBroadcast)
                {
                    _broadcastDays.Add(DayOfWeek.Friday);
                }
                else
                {
                    _broadcastDays.Remove(DayOfWeek.Friday);
                }
            }
        }

        public bool AddSaturdayBroadcast
        {
            get => _addSaturdayBroadcast;
            set
            {
                _addSaturdayBroadcast = value;
                NotifyOfPropertyChange(() => AddSaturdayBroadcast);
                if (_addSaturdayBroadcast)
                {
                    _broadcastDays.Add(DayOfWeek.Saturday);
                }
                else
                {
                    _broadcastDays.Remove(DayOfWeek.Saturday);
                }
            }
        }

        public bool AddSundayBroadcast
        {
            get => _addSundayBroadcast;
            set
            {
                _addSundayBroadcast = value;
                NotifyOfPropertyChange(() => AddSundayBroadcast);
                if (_addSundayBroadcast)
                {
                    _broadcastDays.Add(DayOfWeek.Sunday);
                }
                else
                {
                    _broadcastDays.Remove(DayOfWeek.Sunday);
                }
            }
        }

        public string AddBroadcastViewsCount
        {
            get => Convert.ToString(_addBroadcastViewsCount);
            set
            {
                _addBroadcastViewsCount = value;
                NotifyOfPropertyChange(() => AddBroadcastViewsCount);
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
                Days = _broadcastDays
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
