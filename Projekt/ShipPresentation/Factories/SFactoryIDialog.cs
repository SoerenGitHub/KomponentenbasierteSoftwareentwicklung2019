using FlightLogic;
using SeaSkyPresentation.Services;
using SeaSkyPresentation.ViewModels;
using SeaSkyPresentation.Views;
using SeaSkyPresentation.Views.Pages;
using ShipLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaSkyPresentation.Factories
{
    public static class SFactoryIDialog
    {

        //    private static IContainer container;

        public static FIDialog CreateInstance(FILogicQueries logicQueriesFlight, FILogicCmds logicCommandsFlight, ISLogicQueries logicQueriesShipInfo, ISLogicCommands logicCommandsShipInfo)
        {

            // Dependency Injection
            //Flights
            CServiceOfferFlight serviveOfferFlight = new CServiceOfferFlight(logicCommandsFlight);
            CServiceSearchFlight serviceSearchFlight = new CServiceSearchFlight(logicQueriesFlight);
            //Shipping
            CServiceOfferShipInfo serviveOfferShipInfo = new CServiceOfferShipInfo(logicCommandsShipInfo);
            CServiceSearchShipInfo serviceSearchShipInfo = new CServiceSearchShipInfo(logicQueriesShipInfo);

            //Flights
            CViewModelMainFlight viewModelMainFlight = new CViewModelMainFlight(serviceSearchFlight);
            CViewModelFilterFlight viewModelFilterFlight = new CViewModelFilterFlight(serviceSearchFlight, viewModelMainFlight);
            CViewModelUpdateFlight viewModelUpdateFlight = new CViewModelUpdateFlight(serviceSearchFlight, serviveOfferFlight, viewModelMainFlight);
            CViewModelAdministrationFlight viewModelAdministrationFlight = new CViewModelAdministrationFlight(serviveOfferFlight, viewModelMainFlight, serviceSearchFlight, viewModelUpdateFlight);
            //Shipping
            CViewModelMainShipInfo viewModelMainShipInfo = new CViewModelMainShipInfo(serviceSearchShipInfo);
            CViewModelFilterShipInfo viewModelFilterShipInfo = new CViewModelFilterShipInfo(serviceSearchShipInfo, viewModelMainShipInfo);
            CViewModelUpdateShipInfo viewModelUpdateShipInfo = new CViewModelUpdateShipInfo(serviceSearchShipInfo, serviveOfferShipInfo, viewModelMainShipInfo);
            CViewModelAdministrationShipInfo viewModelAdministrationShipInfo = new CViewModelAdministrationShipInfo(serviveOfferShipInfo, viewModelMainShipInfo, serviceSearchShipInfo, viewModelUpdateShipInfo);
            
            //Flights
            CViewUpdateFlight viewUpdateFlight = new CViewUpdateFlight(viewModelUpdateFlight);
            CViewFilterFlight viewFilterFlight = new CViewFilterFlight(viewModelFilterFlight);
            CViewAdministrationFlight viewAdministrationFlight = new CViewAdministrationFlight(viewModelAdministrationFlight, viewUpdateFlight);
            //Shipping
            CViewUpdateShipInfo viewUpdateShipInfo = new CViewUpdateShipInfo(viewModelUpdateShipInfo);
            CViewFilterShipInfo viewFilterShipInfo = new CViewFilterShipInfo(viewModelFilterShipInfo);
            CViewAdministrationShipInfo viewAdministrationShipInfo = new CViewAdministrationShipInfo(viewModelAdministrationShipInfo, viewUpdateShipInfo);

            CViewMain viewMain = new CViewMain(viewModelMainFlight, viewModelMainShipInfo, viewFilterFlight, 
                viewAdministrationFlight, viewAdministrationShipInfo, viewFilterShipInfo, viewUpdateFlight, viewUpdateShipInfo); ;
            return viewMain;

            //CBootstrapper bootstrapper = new CBootstrapper();
            //IContainer container = bootstrapper.InitContainer( logicQueries, logicCommands );
            //IDialog dialog = container.Resolve<IDialog>();
            //return dialog;
        }
    }
}
