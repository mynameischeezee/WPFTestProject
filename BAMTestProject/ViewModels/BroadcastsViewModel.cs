using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows.Documents;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Models;
using Caliburn.Micro;

namespace BAMTestProject.ViewModels
{
    public class BroadcastsViewModel : Screen
    {

        //private Task<List<Broadcast>> _broadcasts;
        //public Task<List<Broadcast>> Broadcasts
        //{
        //    get => _broadcasts;
        //    set { _broadcasts = value; NotifyOfPropertyChange(()=> Broadcasts); }
        //}

        private ApplicationDbContext DbContext;
        public BroadcastsViewModel(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        
    }
}
