using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT.Command
{
    public partial class CmdEditorForm : Form
    {
        internal IUDTCommand Command { get; private set;}
        internal event EventHandler Completed;

        public CmdEditorForm()
        {
            InitializeComponent();
        }

        private void CmdEditor_Load(object sender, EventArgs e)
        {
            List<string> cmds = UDTCmdProvider.ListCmdTypes();
            cboType.Items.AddRange(cmds.ToArray());
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = cboType.SelectedItem.ToString();
            Command = UDTCmdProvider.GetCmd(type);
            xmlEditor1.Text = Command.Sample.OuterXml;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlElement result = XmlHelper.ParseAsDOM(xmlEditor1.Text);
            Command.Result = result;

            if (Completed != null)
                Completed(this, EventArgs.Empty);

            this.Close();
        }
    }
}
