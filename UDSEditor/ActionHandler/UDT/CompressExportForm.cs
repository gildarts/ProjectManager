using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Util;
using System.IO;
using System.IO.Compression;

namespace ProjectManager.ActionHandler.UDT
{
    public partial class CompressExportForm : Form
    {
        public event EventHandler Completed;
        private string _tableName;

        public CompressExportForm(string tableName)
        {
            InitializeComponent();
            _tableName = tableName;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = checkBox1.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "*.tcbk|*.tcbk";
            sd.FileName = _tableName + ".tcbk";
            sd.AddExtension = true;
            if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFileName.Text = sd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            err.Clear();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtFileName.Text))
            {
                err.SetError(button2, "請選擇存檔位置");
                valid = false;
            }
            if (checkBox1.Checked)
            {
                if (txtCrmPassword.Text != txtPassword.Text)
                {
                    err.SetError(txtCrmPassword, "密碼不相符");
                    valid = false;
                }
            }

            if (!valid)
                return;

            ExportHelper eh = new ExportHelper(_tableName);
            eh.OccurError += new EventHandler<ErrorEventArgs>(eh_OccurError);
            eh.Completed += new EventHandler<ExportEventArgs>(eh_Completed);
            eh.Export();
        }

        void eh_OccurError(object sender, ErrorEventArgs e)
        {
            MessageBox.Show("匯出時發生錯誤! \n" + e.GetException(), "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void eh_Completed(object sender, ExportEventArgs e)
        {            
            this.Text = "匯出壓縮檔";
            string content = e.Result;

            if (checkBox1.Checked)
                CompressUtil.CompressStringToFile(txtFileName.Text, e.Result, txtPassword.Text);    
            else
                CompressUtil.CompressStringToFile(txtFileName.Text, e.Result);    

            if (Completed != null)
                Completed.Invoke(this, EventArgs.Empty);

            this.Close();
        }            
    }
}
