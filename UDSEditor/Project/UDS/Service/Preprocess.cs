using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project.UDS.Service
{
    class Preprocess
    {
        public string Name { get; private set; }
        public PreprocessType Type { get; private set; }
        public string SQL { get; private set; }
        public string InvalidMessage { get; private set; }

        private Preprocess(XmlElement source)
        {
            this.Name = source.GetAttribute("Name");
            this.SQL = source.InnerText;
            this.InvalidMessage = source.GetAttribute("InvalidMessage");

            PreprocessType type ;
            if (!Enum.TryParse<PreprocessType>(source.GetAttribute("Type"), true, out type))
                type = PreprocessType.Variable;
            this.Type = type;
        }

        private Preprocess(string name, PreprocessType type, string sql, string invalidMessage)
        {
            this.Init(name, type, sql, invalidMessage);
        }

        private void Init(string name, PreprocessType type, string sql, string invalidMessage)
        {
            this.Name = name;
            this.Type = type;
            this.SQL = sql;
            this.InvalidMessage = invalidMessage;
        }

        public XmlElement GetXml()
        {
            XmlElement h = XmlHelper.ParseAsDOM("<Preprocess/>");
            h.SetAttribute("Name", this.Name);
            h.SetAttribute("Type", this.Type.ToString());
            h.SetAttribute("InvalidMessage", this.InvalidMessage);
            XmlNode node = h.OwnerDocument.CreateCDataSection(this.SQL);
            h.AppendChild(node);

            return h;
        }

        public static Preprocess Parse(XmlElement source)
        {
            return new Preprocess(source);
        }

        public static Preprocess Create(string name, PreprocessType type, string sql, string invalidMessage)
        {
            return new Preprocess(name, type, sql, invalidMessage);
        }
    }

    enum PreprocessType
    {
        Validate, Variable, Update
    }
}
