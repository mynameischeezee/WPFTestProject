using System;
using System.Collections.Generic;
using System.Windows;
using Autofac;
using BAMTestProject.BL.Implement.Repositories;
using BAMTestProject.BL.Implementation.BaseRepositories;
using BAMTestProject.BL.Implementation.BaseServices;
using BAMTestProject.BL.Implementation.DTO;
using BAMTestProject.BL.Implementation.Factory;
using BAMTestProject.BL.Implementation.Services;
using BAMTestProject.DAL.Implementation;
using BAMTestProject.DAL.Implementation.Entities;
using BAMTestProject.ViewModels;
using Caliburn.Micro;

namespace BAMTestProject.Bootstrapper
{
    public class MainBootstrapper : BootstrapperBase
    {
        private static IContainer _container;

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }

        public MainBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<WindowManager>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<EventAggregator>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ApplicationDbContext>().AsSelf().SingleInstance();
            builder.RegisterType<BroadcastsViewModel>().AsSelf();
            builder.RegisterType<MarketRepository>().AsSelf();
            builder.RegisterType<ShowsViewModel>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<ShowsViewModel>().AsSelf();
            builder.RegisterType<MarketsViewModel>().AsSelf();
            builder.RegisterType<BroadcastsViewModel>().AsSelf();
            builder.RegisterType<ShowRepository>().As<IBaseRepository<ShowEntity>>();
            builder.RegisterType<MarketRepository>().As<IBaseRepository<MarketEntity>>();
            builder.RegisterType<BroadcastRepository>().As<IBaseRepository<BroadcastEntity>>();
            builder.RegisterType<BroadcastEndDateCalculator>().As<IBroadcastEndDateCalculator>();
            builder.RegisterType<BroadcastDto>().AsSelf();
            builder.RegisterType<BroadcastDtoFactory>().AsSelf();
            _container = builder.Build();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                if (_container.IsRegistered(service)) return _container.Resolve(service);
            }
            else
            {
                if (_container.IsRegisteredWithKey(key, service))
                {
                    return _container.ResolveKeyed(key, service);
                }
            }

            throw new Exception($"Could not locate any instances of contract {key ?? service.Name}.");
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }
    }
}
