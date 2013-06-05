using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Project.UDS.Service
{
    class ConverterType
    {
        internal static readonly List<string> Converters;
        internal readonly string Converter;

        static ConverterType()
        {
            Converters = new List<string>();
            Converters.Add("");
            Converters.Add("Boolean");
            Converters.Add("Date");
            Converters.Add("DateTime");
            Converters.Add("Decode");
            Converters.Add("Encode");
            Converters.Add("HTMLDecode");
            Converters.Add("HTMLEncode");
            Converters.Add("Lower");
            Converters.Add("MD5");
            Converters.Add("SHA1");
            Converters.Add("Time");
            Converters.Add("Upper");
        }

        internal static ConverterType Empty { get { return new ConverterType(""); } }
        internal static ConverterType DateTime { get { return new ConverterType("DateTime"); } }
        internal static ConverterType Date { get { return new ConverterType("Date"); } }
        internal static ConverterType Time { get { return new ConverterType("Time"); } }
        internal static ConverterType Boolean { get { return new ConverterType("Boolean"); } }
        internal static ConverterType SHA1 { get { return new ConverterType("SHA1"); } }
        internal static ConverterType MD5 { get { return new ConverterType("MD5"); } }
        internal static ConverterType Encode { get { return new ConverterType("Encode"); } }
        internal static ConverterType Decode { get { return new ConverterType("Decode"); } }
        internal static ConverterType HTMLEncode { get { return new ConverterType("HTMLEncode"); } }
        internal static ConverterType HTMLDecode { get { return new ConverterType("HTMLDecode"); } }
        internal static ConverterType Upper { get { return new ConverterType("Upper"); } }
        internal static ConverterType Lower { get { return new ConverterType("Lower"); } }

        internal static ConverterType Parse(string type)
        {
            return new ConverterType(type);
        }

        private ConverterType(string type)
        {
            foreach (string t in Converters)
            {
                if (t.Equals(type, StringComparison.CurrentCultureIgnoreCase))
                    Converter = type;
            }

            if (Converter == null)
                Converter = type;
        }

        internal bool Equals(ConverterType other)
        {
            if (other.ToString().Equals(Converter, StringComparison.CurrentCultureIgnoreCase))
                return true;
            return false;
        }

        public override string ToString()
        {
            return Converter;
        }
    }
}
