using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManager.Project.UDS.Contract;
using System.Windows.Forms;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDS.Contract
{
    class ContractXmlEditable : AbstractXmlEditable
    {
        internal ContractHandler Contract { get; private set; }
        internal ContractNodeHandler ContractNodeHandler { get; private set; }

        public ContractXmlEditable(string documentName, ContractNodeHandler ContractNodeHandler)
        {
            this.DocumentName = documentName;
            this.ContractNodeHandler = ContractNodeHandler;
            this.Contract = ContractNodeHandler.Contract;
            this.Source = ContractNodeHandler.Contract.Definition;
            this.Contract.Saved += new EventHandler(Contract_Saved);
            this.Contract.Reloaded += new EventHandler(Contract_Reloaded);
        }

        void Contract_Reloaded(object sender, EventArgs e)
        {
            OnEditorReloaded(this.Contract.Definition);
        }

        void Contract_Saved(object sender, EventArgs e)
        {
            //OnInitialEditor(this.Contract.Definition);
        }

        public override void Save()
        {
            try
            {
                XmlEditor xe = this.Editor as XmlEditor;
                string xml = xe.XmlText;
                Contract.SetDefinition(xe.Xml, this);
              
                MessageBox.Show("儲存完畢！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.OnDataSaved();
            }
            catch (Exception ex)
            {
                MessageBox.Show("儲存失敗！" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
    }
}
