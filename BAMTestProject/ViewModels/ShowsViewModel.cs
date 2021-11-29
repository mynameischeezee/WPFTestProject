using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Documents;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Models;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class ShowsViewModel : Screen
    {
        private ApplicationDbContext _dbContext;
        public ShowsViewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            ShowsList = new ObservableCollection<Show>(dbContext.Shows);
            SelectedShow = ShowsList[0];
        }

        private ObservableCollection<Show> _showsList;
        public ObservableCollection<Show> ShowsList
        {
            get => _showsList;
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
                NotifyOfPropertyChange(()=> SelectedShow);
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
            }

        }

    }
}
