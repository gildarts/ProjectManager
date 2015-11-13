using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManager.ActionHandler.UDS.Service;
using ProjectManager.Project.UDS.Service.Variable;
using System.Xml;
using FISCA.DSAClient;
using ProjectManager.Util;
using ProjectManager.ActionHandler.UDS.Service.Editable.Set;
using ProjectManager.Project.UDS.Service.Converter;
using ProjectManager.ActionHandler.UDS.Service.Editable.Javascript;

namespace ProjectManager.Project.UDS.Service
{
    internal class ServiceEntity
    {
        internal string Name { get; private set; }
        internal bool Enabled { get; set; }
        internal ServiceAction Action { get; private set; }
        internal string SQLTemplate { get; set; }
        internal string ResponseRecordElement { get; set; }
        internal string RequestRecordElement { get; set; }
        internal FieldList FieldList { get; private set; }
        internal ConditionList ConditionList { get; private set; }
        internal List<IVariable> Variables { get; private set; }
        internal OrderList Orders { get; private set; }
        internal Pagination Pagination { get; private set; }
        internal List<Preprocess> Preprocesses { get; private set; }
        internal List<IConverter> Converters { get; private set; }

        protected ServiceEntity(string name, string tableName)
        {
            this.Variables = new List<IVariable>();
            this.Preprocesses = new List<Preprocess>();
            this.Converters = new List<IConverter>();
        }

        private ServiceEntity(string name, string targetTable, ServiceAction action)
        {
            this.Variables = new List<IVariable>();
            this.Preprocesses = new List<Preprocess>();
            this.Converters = new List<IConverter>();

            this.FieldList = new FieldList(string.Empty, string.Empty);
            this.Action = action;
            this.Name = name;
            this.Enabled = true;

            if (!MainForm.CurrentUDT.ExistsInAllTables(targetTable))
                throw new Exception("資料表不存在 : " + targetTable);

            string tableName = targetTable.StartsWith("$") ? targetTable.Substring(1) : targetTable;

            if (Action == ServiceAction.Select)
            {
                this.SQLTemplate = "SELECT @@FieldList FROM " + targetTable + " WHERE @@Condition @@Order";
                this.ResponseRecordElement = "Response/" + StringUtil.ConvertToDisplayName(tableName);
                LoadFields(targetTable);
                LoadConditions(targetTable);
                LoadOrders();
                LoadPagination();
            }
            else if (Action == ServiceAction.Insert)
            {
                this.SQLTemplate = "INSERT INTO " + targetTable + " @@FieldList";
                this.RequestRecordElement = StringUtil.ConvertToDisplayName(tableName);
                LoadFields(targetTable);
            }
            else if (Action == ServiceAction.Update)
            {
                this.SQLTemplate = "UPDATE " + targetTable + " SET @@FieldList  WHERE @@Condition";
                this.RequestRecordElement = StringUtil.ConvertToDisplayName(tableName);
                LoadFields(targetTable);
                LoadConditions(targetTable);
            }
            else if (Action == ServiceAction.Delete)
            {
                this.SQLTemplate = "DELETE FROM " + targetTable + " WHERE @@Condition";
                this.RequestRecordElement = StringUtil.ConvertToDisplayName(tableName);
                LoadConditions(targetTable);
            }
        }

        private ServiceEntity(XmlElement definition)
        {
            XmlHelper h = new XmlHelper(definition);
            this.SQLTemplate = h.GetText("SQLTemplate");
            ResponseRecordElement = h.GetText("ResponseRecordElement");
            RequestRecordElement = h.GetText("RequestRecordElement");
            FieldList = new FieldList(h.GetElement("FieldList"));
            ConditionList = new ConditionList(h.GetElement("Conditions"));
            Orders = new OrderList(h.GetElement("Orders"));
            Pagination = new Pagination(h.GetElement("Pagination"));
           
            ServiceAction action = ServiceAction.Select;
            if (!Enum.TryParse<ServiceAction>(h.GetText("Action"), true, out action))
                action = ServiceAction.Select;
            Action = action;

            this.Variables = new List<IVariable>();
            foreach (XmlElement varElement in h.GetElements("InternalVariable/Variable"))
            {
                IVariable v = VariableFactory.Parse(varElement);
                if (v != null)
                    Variables.Add(v);
            }

            this.Converters = new List<IConverter>();
            foreach (XmlElement cvElement in h.GetElements("Converters/Converter"))
            {
                string type = cvElement.GetAttribute("Type");
                IConverter c = ConverterProvider.CreateConverter(type);
                c.Load(cvElement);

                this.Converters.Add(c);
            }

            this.Preprocesses = new List<Preprocess>();
            foreach (XmlElement preElement in h.GetElements("Preprocesses/Preprocess"))
            {
                Preprocess p = Preprocess.Parse(preElement);
                this.Preprocesses.Add(p);
            }
        }

        private void LoadFields(string targetTable)
        {
            List<string> fields = MainForm.CurrentUDT.ListFields(targetTable);
            this.FieldList = new FieldList("FieldList", "Field");
            foreach (string field in fields)
                this.FieldList.Fields.Add(Field.CreateNew(field));
        }

        private void LoadConditions(string targetTable)
        {
            List<string> fields = MainForm.CurrentUDT.ListFields(targetTable);
            this.ConditionList = new ConditionList("Condition", "Condition", false);
            foreach (string field in fields)
                this.ConditionList.Conditions.Add(Condition.CreateNew(field));
        }

        private void LoadOrders()
        {
            this.Orders = new OrderList("Order", "Order");
        }

        private void LoadPagination()
        {
            this.Pagination = new Pagination(true, 0);
        }

        public static ServiceEntity Parse(XmlElement definition)
        {
            return new ServiceEntity(definition);
        }

        public static ServiceEntity CreateNew(string name, string targetTable, ServiceAction action)
        {
            if (action == ServiceAction.Set)
                return new SetServiceEntity(name, targetTable);
            else if (action == ServiceAction.Javascript)
                return new JSServiceEntity(name);

            return new ServiceEntity(name, targetTable, action);
        }


        /// <summary>
        /// 將內容還原成 XmlElement
        /// </summary>
        /// <returns></returns>
        internal XmlElement GetServiceDefinition()
        {
            XmlHelper h = new XmlHelper(GetServiceXml());
            return h.GetElement("Definition");
        }

        internal virtual XmlElement GetServiceXml()
        {
            XmlHelper h = new XmlHelper("<Service/>");
            h.SetAttribute(".", "Enabled", "true");
            h.SetAttribute(".", "Name", this.Name);

            h.AddElement(".", "Definition");
            h.SetAttribute("Definition", "Type", "dbhelper");

            h.AddElement("Definition", "Action", this.Action.ToString());

            XmlElement sqlElement = h.AddElement("Definition", "SQLTemplate");
            XmlCDataSection section = sqlElement.OwnerDocument.CreateCDataSection(this.SQLTemplate.Trim());
            sqlElement.AppendChild(section);

            if (Action == ServiceAction.Select)
            {
                h.AddElement("Definition", "ResponseRecordElement", this.ResponseRecordElement.Trim());
                h.AddElement("Definition", this.FieldList.GetXml(this.Action));

                if (this.ConditionList.Conditions.Count > 0)
                    h.AddElement("Definition", this.ConditionList.GetXml());

                if (this.Orders.Orders.Count > 0 || (!string.IsNullOrWhiteSpace(this.Orders.Name) && this.SQLTemplate.Contains("@@" + this.Orders.Name)))
                    h.AddElement("Definition", this.Orders.GetXml());

                h.AddElement("Definition", this.Pagination.GetXml());
            }
            else if (Action == ServiceAction.Insert)
            {
                h.AddElement("Definition", "RequestRecordElement", this.RequestRecordElement);
                h.AddElement("Definition", this.FieldList.GetXml(this.Action));
            }
            else if (Action == ServiceAction.Update)
            {
                h.AddElement("Definition", "RequestRecordElement", this.RequestRecordElement);
                h.AddElement("Definition", this.FieldList.GetXml(this.Action));
                if (this.ConditionList.Conditions.Count > 0)
                    h.AddElement("Definition", this.ConditionList.GetXml());
            }
            else if (Action == ServiceAction.Delete)
            {
                h.AddElement("Definition", "RequestRecordElement", this.RequestRecordElement);
                if (this.ConditionList.Conditions.Count > 0)
                    h.AddElement("Definition", this.ConditionList.GetXml());
            }

            if (Variables.Count > 0)
            {
                h.AddElement("Definition", "InternalVariable");

                foreach (IVariable v in Variables)
                    h.AddElement("Definition/InternalVariable", v.GetXml());
            }

            return h.GetElement(".");
        }
    }
}
