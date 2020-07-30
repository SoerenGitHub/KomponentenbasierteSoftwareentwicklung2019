using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SeaSkyPresentation.ViewModels
{

    internal class CViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
