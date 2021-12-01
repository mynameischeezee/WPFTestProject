using System.Collections.ObjectModel;
using BAMTestProject.BL.Implement.ModelServices;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Models;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class ShowsViewModel : Screen
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ShowModelService _showModelService;
        private ObservableCollection<Show> _showsList;
        private Show _selectedShow;
        private int _showIdDetail;
        private string _showNameDetail;
        private string _addShowName;

        public bool CanEditShow => !string.IsNullOrWhiteSpace(_showNameDetail);

        public ShowsViewModel(ApplicationDbContext dbContext, ShowModelService showModelService)
        {
            _dbContext = dbContext;
            _showModelService = showModelService;
            SelectedShow = ShowsList[0];
        }

        public ObservableCollection<Show> ShowsList
        {
            get
            {
                _showsList = new ObservableCollection<Show>(_dbContext.Shows);
                return _showsList;
            }
            set => Set(ref _showsList, value, nameof(ShowsList));
        }

        public Show SelectedShow
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
            Show showToEdit = new Show() {Name = _showNameDetail};
            _showModelService.Edit(_selectedShow.Id, showToEdit);
            NotifyOfPropertyChange(() => ShowsList);
        }

        public bool CanAddShow => !string.IsNullOrWhiteSpace(_addShowName);

        public void AddShow()
        {
            Show showToAdd = new Show() {Name = _addShowName};
            _showModelService.Insert(showToAdd);
            AddShowName = "";
            NotifyOfPropertyChange(() => ShowsList);
        }

        public void DeleteShow()
        {

            _showModelService.Delete(SelectedShow.Id);
            NotifyOfPropertyChange(() => ShowsList);
        }
    }
}
