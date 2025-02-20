using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboardComponent.Cores;

namespace VirtualKeyboardComponent.Stores
{
    public interface INavigationStore
    {
        ViewModelBase CurrentViewModel { set; }
    }
}
