using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ProjectManager.Util;

namespace ProjectManager.ActionHandler.UDT
{
    public partial class DecompressImportForm : Form
    {
        internal event EventHandler<ExportEventArgs> Completed;
        private string _tableName;

        public DecompressImportForm(string tableName)
        {
            InitializeComponent();
            _tableName = tableName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog sd = new OpenFileDialog();
            sd.Filter = "*.tcbk|*.tcbk";

            if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFileName.Text = sd.FileName;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.Enabled = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtFileName.Text))
            {
                MessageBox.Show("檔案不存在", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string content;
            try
            {
                if (checkBox1.Checked)
                    content = CompressUtil.DecompressStringFromFile(txtFileName.Text, txtPassword.Text);
                else
                    content = CompressUtil.DecompressStringFromFile(txtFileName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("檔案還原失敗 ! " + ex.Message, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Completed != null)
                Completed.Invoke(this, new ExportEventArgs(content));

            this.Close();
        }
    }
}
