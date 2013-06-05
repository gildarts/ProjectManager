using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager.ActionHandler.UDS.Package
{
    public partial class AddPackageForm : Form
    {
        public event EventHandler Completed;

        internal string PackageName { get; private set; }

        public AddPackageForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Completed != null)
            {
                this.PackageName = this.textBox1.Text;
                Completed.Invoke(this, e);
            }
            this.Close();
        }
    }
}
