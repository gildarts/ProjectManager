using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Project.UDS.Service.Converter
{
    class ConverterProvider
    {
        private static List<string> _types;

        static ConverterProvider()
        {
            _types = new List<string>();
            _types.Add(MappingConverter.ConverterType);
            _types.Add(DateFormatConverter.ConverterType);
            _types.Add(EncodeConverter.ConverterType);
            _types.Add(DecodeConverter.ConverterType);
        }

        public static IConverter CreateConverter(string name)
        {
            switch (name)
            {
                case MappingConverter.ConverterType:
                    return new MappingConverter();
                case DateFormatConverter.ConverterType:
                    return new DateFormatConverter();
                case EncodeConverter.ConverterType:
                    return new EncodeConverter();
                case DecodeConverter.ConverterType:
                    return new DecodeConverter();
                default:
                    return new MappingConverter();
            }
        }

        public static IList<string> ConverterTypes
        {
            get
            {
                return new List<string>(_types);
            }
        }
    }
}
