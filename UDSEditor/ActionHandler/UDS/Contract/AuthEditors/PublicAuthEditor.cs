using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDS.Contract.AuthEditors
{
    public partial class PublicAuthEditor : UserControl, IContractEditor, IResultGetter
    {
        public PublicAuthEditor()
        {
            InitializeComponent();
        }
        
        #region IResultGetter 成員

        public System.Xml.XmlElement GetAuthElement()
        {
            XmlHelper h = new XmlHelper("<Authentication />");
            h.AddElement(".", "Public");
            h.SetAttribute("Public", "Enabled", "true");
            return h.GetElement(".");
        }

        #endregion

        #region IResultGetter 成員

        public void SetDefinition(System.Xml.XmlElement definition)
        {
            
        }

        #endregion

        #region IContractEditor 成員

        public bool Valid
        {
            get { return true; }
        }

        public void Save()
        {
            if (DataChanged != null)
                DataChanged.Invoke(this, EventArgs.Empty);

            if (ChangeRecovered != null)
                ChangeRecovered.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler DataChanged;

        public event EventHandler ChangeRecovered;

        #endregion
    }
}
