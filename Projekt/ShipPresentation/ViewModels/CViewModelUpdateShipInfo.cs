using ShipLogic.Entities;
using ShipLogic.Utils;
using SeaSkyPresentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SeaSkyPresentation.ViewModels
{
    internal class CViewModelUpdateShipInfo : CViewModelBase
    {
        private readonly CServiceSearchShipInfo _serviceSearch;
        private readonly CServiceOfferShipInfo _serviceOffer;
        private readonly CViewModelMainShipInfo _viewModelMain;
        private ShipInfo _shipinfo;


        internal CViewModelUpdateShipInfo(CServiceSearchShipInfo serviceSearch, CServiceOfferShipInfo serviceOffer, CViewModelMainShipInfo viewModelMain )
        {
            Log.D("CViewModelUpdateFlight.Ctor()", "");
            _viewModelMain = viewModelMain;
            _serviceSearch = serviceSearch;
            _serviceOffer = serviceOffer;
            InitializeShipInfo();
        }

        public void InitializeShipInfo()
        {
            if (_viewModelMain.SelectedShipInfo == null)
            {
                _shipinfo = new ShipInfo();
            }
            else
            {
                _shipinfo = _viewModelMain.SelectedShipInfo;
            }
        }

        public ShipInfo ShipInfo
        {
            set
            {
                _shipinfo = value;
                OnPropertyChanged();
                Log.D("CViewModelUpdateFlight.setShipInfo()", value);
            }
        }
        public string Company
        {
            get { return _shipinfo.company; }
            set
            {
                _shipinfo.company = value;
                OnPropertyChanged();
            }
        }
        
        public string DateS
        {
            get { return _shipinfo.date; }
            set
            {
                _shipinfo.date = value;
                OnPropertyChanged();
            }
        }
        public string Destination
        {
            get { return _shipinfo.destination; }
            set
            {
                _shipinfo.destination = value;
                OnPropertyChanged();
            }
        }
       
        public int Duration
        {
            get { return _shipinfo.travelSpan; }
            set
            {
                _shipinfo.travelSpan = value;
                OnPropertyChanged();
            }
        }

        public string ShipType
        {
            get { return _shipinfo.shipType; }
            set
            {
                _shipinfo.shipType = value;
                OnPropertyChanged();
            }
        }

        public string Start
        {
            get { return _shipinfo.start; }
            set
            {
                _shipinfo.start = value;
                OnPropertyChanged();
            }
        }

        public string TimeS
        {
            get { return _shipinfo.depTime; }
            set
            {
                _shipinfo.depTime = value;
                OnPropertyChanged();
            }
        }

        public void Send()
        {
            _serviceOffer.UpdateShipInfo(_shipinfo);
            _viewModelMain.FoundShipInfos = _serviceSearch.GetShipInfos();
        }
    }
}
