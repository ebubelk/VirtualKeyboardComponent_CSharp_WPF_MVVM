using System.Windows.Input;
using VirtualKeyboardComponent.Cores;
using VirtualKeyboardComponent.Stores;

namespace VirtualKeyboardComponent.MVVM.ViewModels
{
    public class MainVM : ViewModelBase
    {
        public VirtualKeyboardVM VirtualKeyboardVM { get; }
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainVM(NavigationStore navigationStore, VirtualKeyboardVM virtualKeyboardVM)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            VirtualKeyboardVM = virtualKeyboardVM;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
