using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboardComponent.Stores;

namespace VirtualKeyboardComponent.Services
{
    public class VirtualKeyboardService
    {
        private readonly VirtualKeyboardStore _virtualKeyboardStore;

        public VirtualKeyboardService(VirtualKeyboardStore virtualKeyboardStore)
        {
            _virtualKeyboardStore = virtualKeyboardStore;
        }

        public void ShowVirtualKeyboard()
        {
            _virtualKeyboardStore.IsVirtualKeyboardVisible = true;
        }

        public void HideVirtualKeyboard()
        {
            _virtualKeyboardStore.IsVirtualKeyboardVisible = false;
        }
    }
}
