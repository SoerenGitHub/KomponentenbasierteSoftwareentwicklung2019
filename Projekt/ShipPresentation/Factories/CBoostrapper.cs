using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

using FlightLogic;
using FlightLogic.Utils;
using SeaSkyPresentation.Services;
using SeaSkyPresentation.ViewModels;
using SeaSkyPresentation.Views;

namespace SeaSkyPresentation.Factories
{
    public class CBoostrapper
    {
        private IContainer container;

        public IContainer InitContainer(FILogicQueries logicQueries, FILogicCmds logicCommands)
        {

            ContainerBuilder containerBuilder = new ContainerBuilder();

            // Service Layer
            containerBuilder
              .RegisterType<CServiceSearchFlight>()
              .AsSelf()
              .WithParameter("logicQueries", logicQueries)
              .SingleInstance();

            containerBuilder
               .RegisterType<CServiceOfferFlight>()
               .AsSelf()
               .WithParameter("logicCommands", logicCommands)
               .SingleInstance();

            // ModelView Layer
            containerBuilder
               .RegisterType<CViewModelMainFlight>()
               .AsSelf()
               .SingleInstance();

            // View Layer

            containerBuilder
               .RegisterType<CViewMain>()
               .As<FIDialog>()
               .SingleInstance();



            container = containerBuilder.Build();

            return container;
        }
    }
}
