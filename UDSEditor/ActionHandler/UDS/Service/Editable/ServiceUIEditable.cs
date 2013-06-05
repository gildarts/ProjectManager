using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ProjectManager.ActionHandler.UDS.Service.DAL;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDS.Service.Editable
{
    class ServiceUIEditable : AbstractUIEditable
    {
        private ServiceNodeHandler _serviceNode;
        private IServiceUI _ui;

        public ServiceUIEditable(string documentName, ServiceNodeHandler serviceNode)
        {
            this.DocumentName = documentName;
            _serviceNode = serviceNode;
        }

        protected override void OnInitialEditor()
        {
            XmlElement definition = ServiceDAL.GetServiceDefinition(_serviceNode.ContractName, _serviceNode.PackageName, _serviceNode.ServiceName);

            //ServiceEditor service = new ServiceEditor(source, this);
            //base._editorInstance = service;

            XmlHelper h = new XmlHelper(definition);
            string action = h.GetText("Action");
            _ui = ServiceFactory.CreateServiceUI(action);

            string serviceFullName = string.Join(".", _serviceNode.PackageName, _serviceNode.ServiceName);

            _ui.Initialize(serviceFullName, definition);

            base.OnEditorChanged(_ui.Editor);

            _ui.DataChanged += new EventHandler(table_DataChanged);
            _ui.ChangeRecovered += new EventHandler(table_ChangeRecovered);
        }

        void table_ChangeRecovered(object sender, EventArgs e)
        {
            OnChangeRecovered();
        }

        void table_DataChanged(object sender, EventArgs e)
        {
            OnDataChange();
        }

        public override bool Valid
        {
            get
            {
                return _ui.Valid;                
            }
        }

        public override void Save()
        {
            XmlElement definition;
            try
            {
                definition = _ui.GetResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show("產生 Service Definition 時發生錯誤 : " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ServiceDAL.SetDefinition(this._serviceNode.ContractName, this._serviceNode.PackageName, this._serviceNode.ServiceName, definition);
            }
            catch (Exception ex)
            {
                MessageBox.Show("儲存 Service Definition 時發生錯誤 : " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            this.OnInitialEditor();
            OnChangeRecovered();
        }

        public override void OnStartEditing()
        {            
            this.OnInitialEditor();
        }
    }
}
