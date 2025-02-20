using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboardComponent.Stores
{
    public class TextBoxDataStore
    {
		private string _textBoxData;

		public string TextBoxData
		{
			get => _textBoxData;
            set
			{
				if(_textBoxData != value)
				{
                    _textBoxData = value;
                }
			}
		}
	}
}
