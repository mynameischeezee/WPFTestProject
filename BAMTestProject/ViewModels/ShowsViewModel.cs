using System.Collections.ObjectModel;
using BAMTestProject.BL.Implement.ModelServices;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Models;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class ShowsViewModel : Screen
    {
        private ApplicationDbContext _dbContext;
        private readonly ShowModelService _showModelService;
        public ShowsViewModel(ApplicationDbContext dbContext,ShowModelService showModelService)
        {
            _dbContext = dbContext;
            _showModelService = showModelService;
            SelectedShow = ShowsList[0];
        }
        // TODO: fix list updating on editing
        private ObservableCollection<Show> _showsList;
        public ObservableCollection<Show> ShowsList
        {
            get
            {
                _showsList = new ObservableCollection<Show>(_dbContext.Shows);
                return _showsList;
            }
            set
            {
                _showsList = value;
                NotifyOfPropertyChange(()=> ShowsList);
            }
        }

        private Show _selectedShow;
        public Show SelectedShow
        {
            get => _selectedShow;
            set
            {
                _selectedShow = value;
                ShowNameDetail = value.Name;
                ShowIdDetail = value.Id;
                NotifyOfPropertyChange(()=> SelectedShow);
            }
        }

        private int _showIdDetail;

        public int Number
        {
            get => _number;
            set => _number = value;
        }

        public int ShowIdDetail
        {
            get => _showIdDetail;
            set
            {
                    _showIdDetail = value;
                    NotifyOfPropertyChange(() => ShowIdDetail);
            }
        }
        private string _showNameDetail;
        public string ShowNameDetail 
        { 
            get => _showNameDetail;
            set
            {
                _showNameDetail = value;
                NotifyOfPropertyChange(()=> ShowNameDetail);
                NotifyOfPropertyChange(()=> CanEditShow);
            }

        }

        private string _addShowName;
        private int _number;

        public string AddShowName
        {
            get => _addShowName;
            set
            {
                _addShowName = value;
                NotifyOfPropertyChange(()=> AddShowName);
                NotifyOfPropertyChange(()=> CanAddShow);
            }
        }
        public bool CanEditShow => !string.IsNullOrWhiteSpace(_showNameDetail);

        public void EditShow()
        {
            Show showToEdit = new Show() { Name = _showNameDetail };
            _showModelService.Edit(_selectedShow.Id, showToEdit);
            NotifyOfPropertyChange(() => ShowsList);
        }
        public bool CanAddShow => !string.IsNullOrWhiteSpace(_addShowName);

        public void AddShow()
        {
            Show showToAdd = new Show() { Name = _addShowName };
            _showModelService.Insert(showToAdd);
            AddShowName = "";
            NotifyOfPropertyChange(()=> ShowsList);
        }

    }
}
