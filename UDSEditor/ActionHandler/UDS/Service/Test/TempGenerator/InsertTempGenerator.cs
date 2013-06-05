using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;
using ProjectManager.Project.UDS.Service;

namespace ProjectManager.ActionHandler.UDS.Service.Test.TempGenerator
{
    class InsertTempGenerator : ITempGenerator
    {
        #region ITempGenerator 成員

        public XmlElement Generate(XmlElement serviceDefinition)
        {
            ServiceEntity service = ServiceEntity.Parse(serviceDefinition);

            XmlHelper h = new XmlHelper("<Request/>");
            XmlElement reqElement = h.GetElement(".");
                        
            XmlElement recElement = h.AddElement(".", service.RequestRecordElement);
            XmlHelper recHelper = new XmlHelper(recElement);
            XmlElement fieldElement = recHelper.GetElement(".");
            if (!string.IsNullOrWhiteSpace(service.FieldList.Source))
                fieldElement = recHelper.AddElement(".", service.FieldList.Source);

            XmlHelper fieldHelper = new XmlHelper(fieldElement);
            List<Field> _requires = new List<Field>();
            List<Field> _notRequries = new List<Field>();

            foreach (Field field in service.FieldList.Fields)
            {
                if (field.SourceType != SourceType.Request) continue;
                if (field.AutoNumber) continue;

                if (field.InputType == IOType.Attribute)
                    fieldElement.SetAttribute(field.Source, string.Empty);
                else if (field.Required)
                    _requires.Add(field);
                else
                    _notRequries.Add(field);
            }

            if (_requires.Count > 0)
            {
                XmlNode node = reqElement.OwnerDocument.CreateComment("以下為必要欄位");
                fieldElement.AppendChild(node);

                foreach (Field field in _requires)
                {
                    string value = string.Empty;
                    if (field.InputType == IOType.Xml)
                        value = "xml";
                    fieldHelper.AddElement(".", field.Source, value);
                }
            }

            if (_notRequries.Count > 0)
            {
                XmlNode node = reqElement.OwnerDocument.CreateComment("以下為非必要欄位");
                fieldElement.AppendChild(node);

                foreach (Field field in _notRequries)
                {
                    string value = string.Empty;
                    if (field.InputType == IOType.Xml)
                        value = "xml";
                    fieldHelper.AddElement(".", field.Source, value);
                }
            }
           
            return reqElement;
        }

        #endregion
    }
}
