using System.Collections.ObjectModel;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Entities;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class ShowsViewModel : Screen
    {
        private readonly ApplicationDbContext _dbContext;
        private ObservableCollection<ShowEntity> _showsList;
        private ShowEntity _selectedShow;
        private int _showIdDetail;
        private string _showNameDetail;
        private string _addShowName;

        public bool CanEditShow => !string.IsNullOrWhiteSpace(_showNameDetail);
        //TODO: Create showVM (another one)
        public ShowsViewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ObservableCollection<ShowEntity> ShowsList
        {
            get
            {
                _showsList = new ObservableCollection<ShowEntity>(_dbContext.Shows);
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
            ShowEntity showToEdit = new ShowEntity() {Name = _showNameDetail};
            //TODO: put it back _showModelService.Edit(_selectedShow.Id, showToEdit);
            NotifyOfPropertyChange(() => ShowsList);
        }

        public bool CanAddShow => !string.IsNullOrWhiteSpace(_addShowName);

        public void AddShow()
        {
            ShowEntity showToAdd = new ShowEntity() {Name = _addShowName};
            //TODO: put it back _showModelService.Insert(showToAdd);
            AddShowName = "";
            NotifyOfPropertyChange(() => ShowsList);
        }

        public void DeleteShow()
        {

            //TODO: put it back _showModelService.Delete(SelectedShow.Id);
            NotifyOfPropertyChange(() => ShowsList);
        }
    }
}
