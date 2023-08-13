using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BToolbox.GUI.Helpers.Converters
{
    public class EnumToStringConverter<TEnum> : EnumConverter<TEnum, string>
    {
        public EnumToStringConverter(Dictionary<TEnum, string> convertedValues = null) : base(convertedValues)
        {
        }
    }
}
