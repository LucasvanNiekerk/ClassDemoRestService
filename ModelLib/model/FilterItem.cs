using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.model
{
    public class FilterItem
    {
        private int _lowQuantity;
        private int _HighQuantity;

        public FilterItem()
        {
            
        }

        public FilterItem(int lowQuantity, int highQuantity)
        {
            _lowQuantity = lowQuantity;
            _HighQuantity = highQuantity;
        }

        public int LowQuantity
        {
            get => _lowQuantity;
            set => _lowQuantity = value;
        }

        public int HighQuantity
        {
            get => _HighQuantity;
            set => _HighQuantity = value;
        }
    }
}
