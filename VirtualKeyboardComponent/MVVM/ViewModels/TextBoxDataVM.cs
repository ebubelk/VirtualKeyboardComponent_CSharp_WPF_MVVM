using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class TextBoxDataVM : ViewModelBase
    {
        private readonly TextBoxDataStore _textBoxDataStore;

        private string _textBoxData;

        public string TextBoxData
        {
            get 
            {
                return _textBoxData = string.IsNullOrEmpty(_textBoxDataStore.TextBoxData)
                    ? "No Data"
                    : _textBoxDataStore.TextBoxData;
            }
            set
            {
                _textBoxData = value;
                OnPropertyChanged(nameof(TextBoxData));
            }
        }

        public ICommand NavigateHomeCommand { get; }

        public TextBoxDataVM(TextBoxDataStore textBoxDataStore, INavigationService homeNavigationService)
        {
            _textBoxDataStore = textBoxDataStore;
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
        }
    }
}
