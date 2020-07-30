using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SeaSkyPresentation.Services;
using ShipLogic.Entities;
using ShipLogic.Utils;

namespace SeaSkyPresentation.ViewModels
{
    internal class CViewModelMainShipInfo : CViewModelBase
    {
        private ShipInfo _selectedShip;
        private ObservableCollection<ShipInfo> _foundShipInfos;

        public ShipInfo SelectedShipInfo
        {
            get => _selectedShip;
            set => _selectedShip = value;
        }

        public ObservableCollection<ShipInfo> FoundShipInfos
        {
            get => _foundShipInfos;
            set => _foundShipInfos = value;
        }

        internal CViewModelMainShipInfo(CServiceSearchShipInfo serviceSearchShipInfo)
        {
            Log.D("CViewModelMainShipInfo.Ctor()", "");
            FoundShipInfos = serviceSearchShipInfo.GetShipInfos();

        }
    }
}
