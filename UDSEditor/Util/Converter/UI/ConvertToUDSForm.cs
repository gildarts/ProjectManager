using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace ProjectManager.Util.Converter.UI
{
    public partial class ConvertToUDSForm : Form
    {
        public ConvertToUDSForm()
        {
            InitializeComponent();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files (*.xml)|*.xml";
            DialogResult dr = ofd.ShowDialog();

            if (dr != System.Windows.Forms.DialogResult.OK)
                return;

            txtFileName.Text = ofd.FileName;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(txtFileName.Text);

                XElement result = null;
                using (XmlNodeReader nodeReader = new XmlNodeReader(doc))
                {
                    // the reader must be in the Interactive state in order to
                    // Create a LINQ to XML tree from it.
                    nodeReader.MoveToContent();

                    XElement xRoot = XElement.Load(nodeReader);
                    result = DeployConverter.DeployConverter.ToUDSDeployElement(xRoot);
                }

                if (result != null)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.DefaultExt = "csml";
                    string sourcepath = txtFileName.Text;
                    FileInfo file = new FileInfo(sourcepath);
                    sfd.InitialDirectory = file.DirectoryName;
                    sfd.FileName = Path.Combine(file.DirectoryName, "new.csml");

                    DialogResult dr = sfd.ShowDialog();
                    if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        XmlDocument d = new XmlDocument();
                        d.LoadXml(result.ToString());
                        d.Save(sfd.FileName);
                        MessageBox.Show("轉換完成", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("轉換過程中發生錯誤 : \n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
