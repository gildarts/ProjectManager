using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDS.Service.Converter;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDS.Service
{
    public partial class EditConverterForm : Form
    {
        public event EventHandler Completed;

        private IConverter _converter;

        public EditConverterForm()
        {
            InitializeComponent();
        }

        public EditConverterForm(IConverter converter)
        {
            _converter = converter;
            InitializeComponent();
        }

        private void EditConverterForm_Load(object sender, EventArgs e)
        {
            cboType.Items.Clear();
            foreach (string type in ConverterProvider.ConverterTypes)
                cboType.Items.Add(type);
            cboType.SelectedIndex = 0;

            if (_converter != null)
            {
                txtName.Text = _converter.Name;                
                txtContent.Text = _converter.Output().OuterXml;
                txtContent.FormatXml();
                //cboType.SelectedValue = _converter.Type;
                int index = 0;
                for(int i=0;i<cboType.Items.Count;i++)
                {
                    string item = cboType.Items[i] + string.Empty;
                    if (item == _converter.Type)
                        index = i;
                }
                cboType.SelectedIndex = index;
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {            
            string type = cboType.Text;

            if (_converter != null && type == _converter.Type)
            {
                txtContent.Text = _converter.Output().OuterXml;
            }
            else
            {
                IConverter converter = ConverterProvider.CreateConverter(type);
                txtContent.Text = converter.GetSample().OuterXml;                
            }
            txtContent.FormatXml();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            IConverter c = ConverterProvider.CreateConverter(cboType.Text);            
            XmlElement element =XmlHelper.ParseAsDOM(txtContent.Text);
            element.SetAttribute("Name", txtName.Text);
            element.SetAttribute("Type", cboType.SelectedItem + string.Empty);
            c.Load(element);

            _converter = c;
            Completed.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        public IConverter GetResult()
        {
            return _converter;
        }
    }
}
