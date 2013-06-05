using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.ActionHandler.UDS.Contract
{
    class ContractUIEditable : AbstractUIEditable
    {
        
        private ContractNodeHandler contractNodeHandler;

        public ContractUIEditable(string documentName, ContractNodeHandler contractNodeHandler)
        {
            this.DocumentName = documentName;
            this.contractNodeHandler = contractNodeHandler;
            this.contractNodeHandler.Contract.Saved += new EventHandler(Contract_Saved);
            this.contractNodeHandler.Contract.Reloaded += new EventHandler(Contract_Reloaded);
        }

        void Contract_Reloaded(object sender, EventArgs e)
        {
            OnEditorReloaded();
        }

        void Contract_Saved(object sender, EventArgs e)
        {
            if (object.ReferenceEquals(this, sender)) return;
            this.OnInitialEditor();
        }

        protected override void OnInitialEditor()
        {
            ContractEditor contract = new ContractEditor(contractNodeHandler, this);
            _editorInstance = contract;
            contract.DataChanged += new EventHandler(contract_DataChanged);
            contract.ChangeRecovered += new EventHandler(contract_ChangeRecovered);
        }

        void contract_ChangeRecovered(object sender, EventArgs e)
        {
            OnChangeRecovered();
        }

        void contract_DataChanged(object sender, EventArgs e)
        {
            OnDataChange();
        }

        public override bool Valid
        {
            get
            {
                ContractEditor edit = Editor as ContractEditor;
                return edit.Valid;
            }
        }

        public override void Save()
        {
            ContractEditor edit = Editor as ContractEditor;
            edit.Save();            
        }
    }
}
