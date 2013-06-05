using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManager.Project.UDS.Service;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDS.Service.Test.TempGenerator
{
    class UpdateTempGenerator : ITempGenerator
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

                string fieldName = field.Source;
                
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

            XmlElement conditionElement = recHelper.GetElement(".");
            if (!string.IsNullOrWhiteSpace(service.ConditionList.Source))
                conditionElement = recHelper.AddElement(".", service.ConditionList.Source);

            XmlHelper conditionHelper = new XmlHelper(conditionElement);
            List<Condition> _reqConditions = new List<Condition>();
            List<Condition> _notReqConditions = new List<Condition>();
            foreach (Condition condition in service.ConditionList.Conditions)
            {
                if (condition.SourceType != SourceType.Request) continue;
                if (condition.Required)
                    _reqConditions.Add(condition);
                else
                    _notReqConditions.Add(condition);
            }

            if (_reqConditions.Count > 0)
            {
                XmlNode comment = conditionElement.OwnerDocument.CreateComment("以下為必要條件");
                conditionElement.AppendChild(comment);

                foreach (Condition con in _reqConditions)
                {
                    conditionHelper.AddElement(".", con.Source);
                }
            }

            if (_notReqConditions.Count > 0)
            {
                XmlNode comment = conditionElement.OwnerDocument.CreateComment("以下為非必要條件");
                conditionElement.AppendChild(comment);

                foreach (Condition con in _notReqConditions)
                {
                    conditionHelper.AddElement(".", con.Source);
                }
            }
            return reqElement;
        }

        #endregion
    }
}
