using System.Collections.ObjectModel;
using BAMTestProject.BL.Implementation.BaseRepositories;
using BAMTestProject.DAL.Implementation.Entities;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class MarketsViewModel : Screen
    {
        private readonly IBaseRepository<MarketEntity> _marketRepository;
        private MarketEntity _selectedMarket;
        private int _marketIdDetail;
        private string _marketNameDetail;
        private string _addMarketName;

        public bool CanAddMarket => !string.IsNullOrWhiteSpace(_addMarketName);
        //TODO: Create marketVM (another one)
        public MarketsViewModel(IBaseRepository<MarketEntity> marketRepository)
        {
            _marketRepository = marketRepository;
        }

        private ObservableCollection<MarketEntity> _marketsList;

        public ObservableCollection<MarketEntity> MarketsList
        {
            get
            {
                _marketsList = _marketRepository.GetAll();
                return _marketsList;
            }
            set => Set(ref _marketsList, value, nameof(MarketsList));
        }

        public MarketEntity SelectedMarket
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
            SelectedMarket.Name = MarketNameDetail;
            _marketRepository.Edit(_selectedMarket.Id, SelectedMarket);
            NotifyOfPropertyChange(() => MarketsList);
        }

        public void AddMarket()
        {
            var marketToAdd = new MarketEntity() {Name = _addMarketName};
            _marketRepository.Insert(marketToAdd);
            AddMarketName = "";
            NotifyOfPropertyChange(() => MarketsList);
        }

        public void DeleteMarket()
        {
            _marketRepository.Delete(SelectedMarket.Id);
            NotifyOfPropertyChange(() => MarketsList);
        }
    }
}
