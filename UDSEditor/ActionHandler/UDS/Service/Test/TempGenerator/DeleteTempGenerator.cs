using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;
using ProjectManager.Project.UDS.Service;

namespace ProjectManager.ActionHandler.UDS.Service.Test.TempGenerator
{
    class DeleteTempGenerator : ITempGenerator
    {
        #region ITempGenerator 成員

        public System.Xml.XmlElement Generate(XmlElement serviceDefinition)
        {
            ServiceEntity service = ServiceEntity.Parse(serviceDefinition);

            XmlHelper h2 = new XmlHelper("<Request/>");
            XmlElement e = h2.AddElement(".", service.RequestRecordElement);
            XmlHelper h = new XmlHelper(e);

            XmlElement reqElement = h.GetElement(".");
            
            XmlElement conditionElement = h.GetElement(".");
            if (!string.IsNullOrWhiteSpace(service.ConditionList.Source))
                conditionElement = h.AddElement(".", service.ConditionList.Source);

            XmlHelper conditionHelper = new XmlHelper(conditionElement);
            List<Condition> _requires = new List<Condition>();
            List<Condition> _notRequires = new List<Condition>();

            foreach (Condition condition in service.ConditionList.Conditions)
            {
                if (condition.SourceType != SourceType.Request) continue;
                if (condition.Required)
                    _requires.Add(condition);
                else
                    _notRequires.Add(condition);
            }

            if (_requires.Count > 0)
            {
                XmlNode comment = conditionElement.OwnerDocument.CreateComment("以下為必要條件");
                conditionElement.AppendChild(comment);

                foreach (Condition con in _requires)
                {
                    conditionHelper.AddElement(".", con.Source);
                }
            }

            if (_notRequires.Count > 0)
            {
                XmlNode comment = conditionElement.OwnerDocument.CreateComment("以下為非必要條件");
                conditionElement.AppendChild(comment);

                foreach (Condition con in _notRequires)
                {
                    conditionHelper.AddElement(".", con.Source);
                }
            }          
            return h2.GetElement(".");
        }

        #endregion
    }
}
