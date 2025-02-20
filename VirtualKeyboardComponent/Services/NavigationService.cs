using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboardComponent.Cores;
using VirtualKeyboardComponent.Stores;

namespace VirtualKeyboardComponent.Services
{
    public class NavigationService<TViewModel> : INavigationService
        where TViewModel : ViewModelBase
    {
        private readonly Func<TViewModel> _createViewModel;
        private readonly NavigationStore _navigationStore;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _createViewModel = createViewModel;
            _navigationStore = navigationStore;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
