using System.Collections.ObjectModel;
using BAMTestProject.BL.Implementation.BaseRepositories;
using BAMTestProject.DAL.Implementation.Entities;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class ShowsViewModel : Screen
    {
        private readonly IBaseRepository<ShowEntity> _showRepository;
        private ObservableCollection<ShowEntity> _showsList;
        private ShowEntity _selectedShow;
        private int _showIdDetail;
        private string _showNameDetail;
        private string _addShowName;

        public bool CanEditShow => !string.IsNullOrWhiteSpace(_showNameDetail);

        //TODO: Create showVM (another one)
        public ShowsViewModel(IBaseRepository<ShowEntity> showRepository)
        {
            _showRepository = showRepository;
        }

        public ObservableCollection<ShowEntity> ShowsList
        {
            get
            {
                _showsList = _showRepository.GetAll();
                return _showsList;
            }
            set => Set(ref _showsList, value, nameof(ShowsList));
        }

        public ShowEntity SelectedShow
        {
            get => _selectedShow;
            set
            {
                if (value != null)
                {
                    ShowNameDetail = value.Name;
                    ShowIdDetail = value.Id;
                    Set(ref _selectedShow, value, nameof(SelectedShow));
                }
            }
        }

        public int ShowIdDetail
        {
            get => _showIdDetail;
            set => Set(ref _showIdDetail, value, nameof(ShowIdDetail));
        }

        public string ShowNameDetail
        {
            get => _showNameDetail;
            set
            {
                Set(ref _showNameDetail, value, nameof(ShowNameDetail));
                NotifyOfPropertyChange(() => CanEditShow);
            }
        }

        public string AddShowName
        {
            get => _addShowName;
            set
            {
                Set(ref _addShowName, value, nameof(AddShowName));
                NotifyOfPropertyChange(() => CanAddShow);
            }
        }

        public void EditShow()
        {
            SelectedShow.Name = ShowNameDetail;
            _showRepository.Edit(_selectedShow.Id, SelectedShow);
            Update();
        }

        public bool CanAddShow => !string.IsNullOrWhiteSpace(_addShowName);

        public void AddShow()
        {
            var showToAdd = new ShowEntity {Name = _addShowName};
            _showRepository.Insert(showToAdd);
            CleanUp();
        }

        public void DeleteShow()
        {
            _showRepository.Delete(SelectedShow.Id);
            Update();
        }

        public void CleanUp()
        {
            AddShowName = "";
            Update();
        }

        public void Update()
        {
            NotifyOfPropertyChange(() => ShowsList);
        }
    }
}
