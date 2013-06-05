using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace ProjectManager.Editor
{
    public partial class SQLText : UserControl
    {
        public new event EventHandler TextChanged;

        public SQLText()
        {
            InitializeComponent();
            this.syntaxEditor1.Document.LoadLanguageFromXml(Assembly.GetExecutingAssembly().GetManifestResourceStream("ProjectManager.ActiproSoftware.SQL.xml"), 0);
        }

        public override string Text
        {
            get
            {
                return syntaxEditor1.Text;
            }
            set
            {
                this.syntaxEditor1.Text = value.Trim();                
            }
        }

        private void syntaxEditor1_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
                TextChanged.Invoke(this, e);
        }
    }
}
