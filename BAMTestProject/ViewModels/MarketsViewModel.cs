using System.Collections.ObjectModel;
using BAMTestProject.BL.Implement.ModelServices;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Models;
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

        public MarketsViewModel(ApplicationDbContext dbContext, MarketModelService marketModelService)
        {
            _dbContext = dbContext;
            _marketModelService = marketModelService;
            SelectedMarket = MarketsList[0];
        }
        // TODO: fix list updating on editing

        private ObservableCollection<Market> _marketsList;

        public ObservableCollection<Market> MarketsList
        {
            get
            {
                _marketsList = new ObservableCollection<Market>(_dbContext.Markets);
                return _marketsList;
            }
            set
            {
                _marketsList = value;
                NotifyOfPropertyChange(() => MarketsList);
            }
        }

        public Market SelectedMarket
        {
            get => _selectedMarket;
            set
            {
                _selectedMarket = value;
                MarketNameDetail = value.Name;
                MarketIdDetail = value.Id;
                NotifyOfPropertyChange(() => SelectedMarket);
            }
        }

        public int MarketIdDetail
        {
            get => _marketIdDetail;
            set
            {
                _marketIdDetail = value;
                NotifyOfPropertyChange(() => MarketIdDetail);
            }
        }

        public string MarketNameDetail
        {
            get => _marketNameDetail;
            set
            {
                _marketNameDetail = value;
                NotifyOfPropertyChange(() => MarketNameDetail);
                NotifyOfPropertyChange(() => CanEditMarket);
            }
        }

        public string AddMarketName
        {
            get => _addMarketName;
            set
            {
                _addMarketName = value;
                NotifyOfPropertyChange(() => AddMarketName);
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
    }
}
