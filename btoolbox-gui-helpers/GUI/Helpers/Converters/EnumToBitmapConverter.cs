using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BToolbox.GUI.Helpers.Converters
{
    public class EnumToBitmapConverter<TEnum> : EnumConverter<TEnum, Bitmap>
    {
        public EnumToBitmapConverter(Dictionary<TEnum, Bitmap> convertedValues = null) : base(convertedValues)
        { }
    }
}
