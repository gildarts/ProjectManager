using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager.Project.UDS.Service.Variable
{
    public partial class UUIDVariableEditor : UserControl
    {
        public UUIDVariableEditor()
        {
            InitializeComponent();
        }

        internal string VariableName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        internal string VariableKey
        {
            get { return string.Empty; }        
        }
    }
}
