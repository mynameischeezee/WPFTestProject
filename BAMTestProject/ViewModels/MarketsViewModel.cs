﻿using System.Collections.ObjectModel;
using BAMTestProject.BL.Implement.ModelServices;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Entities;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class MarketsViewModel : Screen
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MarketModelService _marketModelService;
        private Market _selectedMarket;
        private int _marketIdDetail;
        private string _marketNameDetail;
        private string _addMarketName;

        public bool CanAddMarket => !string.IsNullOrWhiteSpace(_addMarketName);
        //TODO: Create marketVM (another one)
        public MarketsViewModel(ApplicationDbContext dbContext, MarketModelService marketModelService)
        {
            _dbContext = dbContext;
            _marketModelService = marketModelService;
        }

        private ObservableCollection<Market> _marketsList;

        public ObservableCollection<Market> MarketsList
        {
            get
            {
                _marketsList = new ObservableCollection<Market>(_dbContext.Markets);
                return _marketsList;
            }
            set => Set(ref _marketsList, value, nameof(MarketsList));
        }

        public Market SelectedMarket
        {
            get => _selectedMarket;
            set
            {
                if (value != null)
                {
                    MarketNameDetail = value.Name;
                    MarketIdDetail = value.Id;
                    Set(ref _selectedMarket, value, nameof(SelectedMarket));
                }
            }
        }

        public int MarketIdDetail
        {
            get => _marketIdDetail;
            set => Set(ref _marketIdDetail, value, nameof(MarketIdDetail));
        }

        public string MarketNameDetail
        {
            get => _marketNameDetail;
            set
            {
                Set(ref _marketNameDetail, value, nameof(MarketNameDetail));
                NotifyOfPropertyChange(() => CanEditMarket);
            }
        }

        public string AddMarketName
        {
            get => _addMarketName;
            set
            {
                Set(ref _addMarketName, value, nameof(AddMarketName));
                NotifyOfPropertyChange(() => CanAddMarket);
            }
        }

        public bool CanEditMarket => !string.IsNullOrWhiteSpace(_marketNameDetail);

        public void EditMarket()
        {
            var marketToEdit = new Market() {Name = _marketNameDetail};
            _marketModelService.Edit(_selectedMarket.Id, marketToEdit);
            NotifyOfPropertyChange(() => MarketsList);
        }

        public void AddMarket()
        {
            var marketToAdd = new Market() {Name = _addMarketName};
            _marketModelService.Insert(marketToAdd);
            AddMarketName = "";
            NotifyOfPropertyChange(() => MarketsList);
        }

        public void DeleteMarket()
        {
            _marketModelService.Delete(SelectedMarket.Id);
            NotifyOfPropertyChange(() => MarketsList);
        }
    }
}
