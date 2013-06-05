using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManager.Project.UDS.Service;
using ProjectManager.ActionHandler.UDS.Package;
using System.Windows.Forms;
using ProjectManager.ActionHandler.UDS.Service.DAL;
using System.Xml;

namespace ProjectManager.ActionHandler.UDS.Service
{
    class ServiceXmlEditable : AbstractXmlEditable
    {
        internal ServiceNodeHandler ServiceNodeHandler { get; private set; }

        public ServiceXmlEditable(string documentName, ServiceNodeHandler serviceNode)
        {
            this.DocumentName = documentName;
            this.ServiceNodeHandler = serviceNode;          
        }
            
        public override void Save()
        {
            XmlEditor xe = this.Editor as XmlEditor;
                        
            ServiceDAL.SetDefinition(this.ServiceNodeHandler.ContractName, this.ServiceNodeHandler.PackageName, this.ServiceNodeHandler.ServiceName, xe.Xml);

            this.OnDataSaved();
        }

        public override void OnStartEditing()
        {
            XmlElement source = ServiceDAL.GetServiceDefinition(ServiceNodeHandler.ContractName, ServiceNodeHandler.PackageName, ServiceNodeHandler.ServiceName);
            
            this.OnInitialEditor(source);
        }
    }
}
