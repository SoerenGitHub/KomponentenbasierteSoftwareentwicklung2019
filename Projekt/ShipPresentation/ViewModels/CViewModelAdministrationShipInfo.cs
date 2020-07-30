using ShipLogic.Entities;
using ShipLogic.Utils;
using SeaSkyPresentation.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaSkyPresentation.ViewModels
{
    internal class CViewModelAdministrationShipInfo : CViewModelBase
    {
        private readonly CServiceOfferShipInfo _serviceOffer;
        private ShipInfo _shipinfo;
        private CViewModelMainShipInfo _viewModelMain;
        private CServiceSearchShipInfo _serviceSearch;
        private readonly CViewModelUpdateShipInfo _viewModelUpdate;
        public ObservableCollection<ShipInfo> ShipInfos { get => _viewModelMain.FoundShipInfos; }

        internal CViewModelAdministrationShipInfo(CServiceOfferShipInfo serviceOffer, CViewModelMainShipInfo viewModelMain, CServiceSearchShipInfo serviceSearch, CViewModelUpdateShipInfo viewModelUpdate)
        {
            Log.D("CViewModelCreateshipinfo.Ctor()", "");
            _viewModelMain = viewModelMain;
            _serviceOffer = serviceOffer;
            _serviceSearch = serviceSearch;
            _viewModelUpdate = viewModelUpdate;
            _shipinfo = new ShipInfo();

        }

        public ShipInfo ShipInfo
        {
            set
            {
                _shipinfo = value;
                Log.D("CViewModelCreateshipinfo.setshipinfo()", value);
            }
        }

        public string Company
        {
            get => _shipinfo.company;
            set
            {
                _shipinfo.company = value;
                Log.D("CViewModelCreateshipinfo.setAirline()", value);
                this.OnPropertyChanged();
            }
        }
       
        public string DateS
        {
            get => _shipinfo.date;
            set
            {
                if (value == "")
                {
                    value = null;
                }
                _shipinfo.date = value;
                Log.D("CViewModelCreateshipinfo.setDateS()", value);
                this.OnPropertyChanged();
            }
        }
        public string Destination
        {
            get => _shipinfo.destination;
            set
            {
                _shipinfo.destination = value;
                Log.D("CViewModelCreateshipinfo.setDestination()", value);
                this.OnPropertyChanged();
            }
        }
        
        public int Duration
        {
            get => _shipinfo.travelSpan;
            set
            {
                _shipinfo.travelSpan = value;
                Log.D("CViewModelCreateshipinfo.setDuration()", value);
                this.OnPropertyChanged();
            }
        }

        public string ShipType
        {
            get => _shipinfo.shipType;
            set
            {
                _shipinfo.shipType = value;
                Log.D("CViewModelCreateshipinfo.setSeats()", value);
                this.OnPropertyChanged();
            }
        }
        public string Start
        {
            get => _shipinfo.start;
            set
            {
                _shipinfo.start = value;
                Log.D("CViewModelCreateshipinfo.setStart()", value);
                this.OnPropertyChanged();
            }
        }

        public string TimeS
        {
            get => _shipinfo.depTime;
            set
            {
                _shipinfo.depTime = value;
                Log.D("CViewModelCreateshipinfo.setTimeS()", value);
                this.OnPropertyChanged();
            }
        }

        public void Clear()
        {
            _shipinfo = new ShipInfo();
        }

        public void create()
        {
            

            _serviceOffer.CreateShipInfo(_shipinfo);
            this.Clear();
            _viewModelMain.FoundShipInfos = _serviceSearch.GetShipInfos();
        }

        public void delete(string id)
        {
            Log.D("CViewModelCreateshipinfo.delete()", "ID: " + id);
            ShipInfo shipinfo = _serviceSearch.GetShipInfo(id);
            if (shipinfo != null)
            {
                _serviceOffer.DeleteShipInfo(shipinfo);
                _viewModelMain.FoundShipInfos = _serviceSearch.GetShipInfos();
            }
        }

        public void update(string id)
        {
            Log.D("CViewModelCreateShipinfo.delete()", "ID: " + id);
            ShipInfo shipinfo = _serviceSearch.GetShipInfo(id);
            if (shipinfo != null)
            {
                _viewModelMain.SelectedShipInfo = shipinfo;
                _viewModelUpdate.InitializeShipInfo();
            }
        }
    }
}
