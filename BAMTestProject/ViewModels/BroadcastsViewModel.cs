using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows.Documents;
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
        public BroadcastsViewModel(ApplicationDbContext dbContext, BroadcastModelService broadcastsModelService)
        {
            _dbContext = dbContext;
            _broadcastsModelService = broadcastsModelService;
        }
        private ObservableCollection<Broadcast> _broadcastsList;
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

        private Broadcast _selectedBroadcast;

        public Broadcast SelectedBroadcast
        {
            get => _selectedBroadcast;
            set
            {
                _selectedBroadcast = value;
                NotifyOfPropertyChange(() => SelectedBroadcast);
            }
        }

    }
}
