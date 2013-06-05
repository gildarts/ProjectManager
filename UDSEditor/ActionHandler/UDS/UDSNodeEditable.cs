using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManager.ActionHandler.UDS.Contract;

namespace ProjectManager.ActionHandler.UDS
{
    class UDSNodeEditable : AbstractUIEditable
    {
        //public override event EventHandler DataChanged;
        //public override event EventHandler ChangeRecovered;

        private UDSNodeHandler _udsNodeHandler;
        internal UDSNodeEditable(UDSNodeHandler udsNodeHandler)
        {
            _udsNodeHandler = udsNodeHandler;
        }

        protected override void OnInitialEditor()
        {            
            UDSUIEditor uds = new UDSUIEditor(_udsNodeHandler);
            base._editorInstance = uds;

            uds.DataChanged += new EventHandler(uds_DataChanged);
            uds.ChangeRecovered += new EventHandler(uds_ChangeRecovered);
        }

        void uds_ChangeRecovered(object sender, EventArgs e)
        {
            OnChangeRecovered();
        }

        void uds_DataChanged(object sender, EventArgs e)
        {
            OnDataChange();
        }

        public override string DocumentTitle
        {
            get { return "專案 Contract 管理"; }
        }

        public override bool Valid
        {
            get { return true; }
        }

        public override void Save()
        {
            IContractEditor edit = this.Editor as IContractEditor;
            edit.Save();           
        }
    }
}
