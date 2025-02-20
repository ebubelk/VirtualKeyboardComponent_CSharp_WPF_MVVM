using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;
using VirtualKeyboardComponent.Cores;
using VirtualKeyboardComponent.Services;
using VirtualKeyboardComponent.Stores;
using WindowsInput;
using WindowsInput.Native;

namespace VirtualKeyboardComponent.MVVM.ViewModels
{
    public enum KeyboardType
    {
        Alphabetic,
        Numeric
    }

    public class VirtualKeyboardVM : ViewModelBase
    {
        private readonly InputSimulator _inputSimulator = new();
        private readonly VirtualKeyboardStore _virtualKeyboardStore;
        private readonly VirtualKeyboardService _virtualKeyboardService;

        private DispatcherTimer _keyPressTimer;
        private string _currentKey;
        private bool _isFirstTick;

        private KeyboardType _keyboardType = KeyboardType.Alphabetic;
        public KeyboardType KeyboardType
        {
            get => _keyboardType;
            set
            {
                _keyboardType = value;
                OnPropertyChanged(nameof(KeyboardType));
            }
        }

        public bool IsVirtualKeyboardVisible
        {
            get => _virtualKeyboardStore.IsVirtualKeyboardVisible;
            set
            {
                if (_virtualKeyboardStore.IsVirtualKeyboardVisible != value) //Bu kontrol yapılmazsa değer değişmemiş olsa bile UI güncellenir.
                {
                    _virtualKeyboardStore.IsVirtualKeyboardVisible = value;
                    OnPropertyChanged(nameof(IsVirtualKeyboardVisible));
                }
            }
        }

        public ICommand KeyPressCommand { get; }
        public ICommand KeyReleaseCommand { get; }
        public ICommand HideVirtualKeyboardCommand { get; }

        public VirtualKeyboardVM(VirtualKeyboardStore virtualKeyboardStore, VirtualKeyboardService virtualKeyboardService)
        {
            _virtualKeyboardService = virtualKeyboardService;
            HideVirtualKeyboardCommand = new RelayCommand(_ => _virtualKeyboardService.HideVirtualKeyboard());
            _virtualKeyboardStore = virtualKeyboardStore;
            _virtualKeyboardStore.OnVisibilityChanged += OnVisibilityChanged;

            KeyPressCommand = new RelayCommand(p => StartKeyPress(p as string));
            KeyReleaseCommand = new RelayCommand(_ => StopKeyPress());

            _keyPressTimer = new DispatcherTimer();
            _keyPressTimer.Tick += (s, e) =>
            {
                if (_isFirstTick)
                {
                    // İlk tekrar: 300 ms sonra çalışır.
                    SimulateKeyPress();
                    _isFirstTick = false;
                    _keyPressTimer.Interval = TimeSpan.FromMilliseconds(50); // Sonraki tekrarlar 100 ms
                }
                else
                {
                    SimulateKeyPress();
                }
            };
        }

        private void OnVisibilityChanged()
        {
            OnPropertyChanged(nameof(IsVirtualKeyboardVisible));
        }

        private void StartKeyPress(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            _currentKey = key;
            SimulateKeyPress(); // İlk basımı hemen yap
            _isFirstTick = true;
            _keyPressTimer.Interval = TimeSpan.FromMilliseconds(400);
            _keyPressTimer.Start();
        }

        private void StopKeyPress()
        {
            _keyPressTimer.Stop();
        }

        private void SimulateKeyPress()
        {
            if (string.IsNullOrEmpty(_currentKey))
                return;

            if (_currentKey == "123")
            {
                KeyboardType = KeyboardType.Numeric;
                return;
            }
            if (_currentKey == "ABC")
            {
                KeyboardType = KeyboardType.Alphabetic;
                return;
            }

            switch (_currentKey)
            {
                case "SPACE":
                    _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                    break;
                case "ENTER":
                    _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                    break;
                case "BACKWARD":
                    _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
                    break;
                case "LEFT":
                    _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
                    break;
                case "RIGHT":
                    _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                    break;
                default:
                    // Normal karakter girişi:
                    _inputSimulator.Keyboard.TextEntry(_currentKey);
                    break;
            }
        }
    }
}
