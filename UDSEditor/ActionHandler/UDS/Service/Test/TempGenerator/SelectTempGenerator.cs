using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;
using ProjectManager.Project.UDS.Service;

namespace ProjectManager.ActionHandler.UDS.Service.Test.TempGenerator
{
    class SelectTempGenerator:ITempGenerator
    {
        #region ITempGenerator 成員

        public System.Xml.XmlElement Generate(XmlElement serviceDefinition)
        {
            ServiceEntity service = ServiceEntity.Parse(serviceDefinition);

            XmlHelper h = new XmlHelper("<Request/>");
            XmlElement reqElement = h.GetElement(".");

            XmlElement fieldElement = h.GetElement(".");
            if (!string.IsNullOrWhiteSpace(service.FieldList.Source))
                fieldElement = h.AddElement(".", service.FieldList.Source);

            XmlHelper fieldHelper = new XmlHelper(fieldElement);
            fieldHelper.AddElement(".", "All");
            foreach (Field field in service.FieldList.Fields)
            {
                if (field.Mandatory) continue;
                if (field.SourceType != SourceType.Request) continue;

                fieldHelper.AddElement(".", field.Source);
            }

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

            if (service.Orders.Orders.Count > 0)
            {
                XmlElement orderElement = h.GetElement(".");
                if (!string.IsNullOrWhiteSpace(service.Orders.Source))
                    orderElement = h.AddElement(".", service.Orders.Source);
                XmlHelper orderHelper = new XmlHelper(orderElement);
                foreach (Order order in service.Orders.Orders)
                {
                    orderHelper.AddElement(".", order.Source, "ASC_DESC");
                }
            }

            if (service.Pagination.AllowPagination)
            {
                XmlHelper ph = new XmlHelper("<Pagination/>");
                ph.AddElement(".", "StartPage", "1");
                ph.AddElement(".", "PageSize", service.Pagination.MaxPageSize.ToString());

                XmlNode node = reqElement.OwnerDocument.CreateComment(ph.XmlString);
                reqElement.AppendChild(node);
            }
            return reqElement;
        }

        #endregion
    }
}
