using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboardComponent.Stores
{
    public class VirtualKeyboardStore
    {
        private bool _isVirtualKeyboardVisible;
        public bool IsVirtualKeyboardVisible
        {
            get => _isVirtualKeyboardVisible;
            set
            {
                if (_isVirtualKeyboardVisible != value)
                {
                    _isVirtualKeyboardVisible = value;
                    OnVisibilityChanged?.Invoke();
                }
            }
        }

        public VirtualKeyboardStore()
        {
            _isVirtualKeyboardVisible = false;
        }

        public event Action OnVisibilityChanged;
    }
}
