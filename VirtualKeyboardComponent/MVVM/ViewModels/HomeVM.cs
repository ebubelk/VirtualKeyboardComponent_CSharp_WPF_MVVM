using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualKeyboardComponent.Commands;
using VirtualKeyboardComponent.Cores;
using VirtualKeyboardComponent.Services;
using VirtualKeyboardComponent.Stores;

namespace VirtualKeyboardComponent.MVVM.ViewModels
{
    public class HomeVM : ViewModelBase
    {
        private readonly VirtualKeyboardService _virtualKeyboardService;
        private readonly TextBoxDataStore _textBoxDataStore;

        private string _data;

        public string Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        public ICommand ShowVirtualKeyboardCommand { get; }
        public ICommand HideVirtualKeyboardCommand { get; }
        public ICommand NavigateTextBoxDataCommand { get; }

        public HomeVM(VirtualKeyboardService virtualKeyboardService, INavigationService textBoxDataNavigationService, TextBoxDataStore textBoxDataStore) {
            _virtualKeyboardService = virtualKeyboardService;
            _textBoxDataStore = textBoxDataStore;

            ShowVirtualKeyboardCommand = new RelayCommand(_ => _virtualKeyboardService.ShowVirtualKeyboard());
            HideVirtualKeyboardCommand = new RelayCommand(_ => _virtualKeyboardService.HideVirtualKeyboard());
            NavigateTextBoxDataCommand = new RelayCommand(_ => ExecuteNavigateHomeTestCommand(textBoxDataNavigationService));
        }

        private void ExecuteNavigateHomeTestCommand(INavigationService viewModel)
        {
            HideVirtualKeyboardCommand.Execute(null);
            _textBoxDataStore.TextBoxData = Data;
            new NavigateCommand(viewModel).Execute(null);
        }
    }
}
