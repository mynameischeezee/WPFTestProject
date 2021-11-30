using System;
using System.Collections.Generic;
using System.Windows;
using Autofac;
using BAMTestProject.BL.Implement.ModelServices;
using BAMTestProject.DAL.Implementation;
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
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<ShowsViewModel>().AsSelf();
            builder.RegisterType<MarketsViewModel>().AsSelf();
            builder.RegisterType<BroadcastsViewModel>().AsSelf();
            builder.RegisterType<ShowModelService>().AsSelf();
            builder.RegisterType<MarketModelService>().AsSelf();
            builder.RegisterType<BroadcastModelService>().AsSelf();
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
                if (_container.IsRegistered(service))
                    return _container.Resolve(service);
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
