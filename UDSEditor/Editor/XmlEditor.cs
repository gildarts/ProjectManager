using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;

namespace ProjectManager.Editor
{
    public partial class XmlEditor : UserControl
    {
        public event KeyEventHandler UserKeyUp;
        public XmlEditor()
        {
            InitializeComponent();

            xmlSyntaxLanguage1.IsUpdating = true;
            xmlSyntaxLanguage1.LexicalMacros.Remove(xmlSyntaxLanguage1.LexicalMacros["TagNameMacro"]);
            xmlSyntaxLanguage1.LexicalMacros.Add(new ActiproSoftware.SyntaxEditor.Addons.Dynamic.LexicalMacro("TagNameMacro", @"[a-zA-Z_0-9\-:\.\u2F00-\u9FFF]"));
            xmlSyntaxLanguage1.IsUpdating = false;
        }

        public override string Text
        {
            get
            {
                return syntaxEditor1.Text;
            }
            set
            {
                syntaxEditor1.Text = value;
                FormatXml();
            }
        }

        public void FormatXml()
        {
            xmlSyntaxLanguage1.FormatDocument(syntaxEditor1.Document);
        }

        private void syntaxEditor1_KeyTyped(object sender, ActiproSoftware.SyntaxEditor.KeyTypedEventArgs e)
        {
            if (e.KeyData.HasFlag(Keys.Shift) && e.KeyData.HasFlag(Keys.Control) && e.KeyData.HasFlag(Keys.F))
            {
                FormatXml();
            }
        }

        private void syntaxEditor1_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.UserKeyUp != null)
                this.UserKeyUp.Invoke(this, e);
        }

        public SyntaxEditor Editor { get { return this.syntaxEditor1; } }
    }
}
