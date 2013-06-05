using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ProjectManager.Util;

namespace ProjectManager.ActionHandler.UDT
{
    public partial class BatchImportForm : Form
    {
        public event EventHandler Completed;

        private List<XmlElement> _records;
        
        public BatchImportForm(List<XmlElement> records)
        {
            InitializeComponent();
            _records = records;
        }

        private void BatchImportForm_Load(object sender, EventArgs e)
        {
            BatchHelper h = new BatchHelper(MainForm.CurrentProject, _records, "UDTService.DML.Command");
            h.Size = 200;
            this.progressBar1.Maximum = h.BatchCount;
            this.progressBar1.Value = 0;
            h.CompleteOne += new EventHandler(h_CompleteOne);
            h.CompleteAll += new EventHandler(h_CompleteAll);
            h.OccuredError += new System.IO.ErrorEventHandler(h_OccuredError);
            h.Start();
        }

        void h_OccuredError(object sender, System.IO.ErrorEventArgs e)
        {
            lblInfo.Text = "批次發生錯誤 !!";
            Application.DoEvents();

            MessageBox.Show("匯入時發生錯誤 : \n" + e.GetException().Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);

            lblInfo.Text = "取消作業中 .....";
            Application.DoEvents();

            BatchHelper h = sender as BatchHelper;
            h.Cancel();

            lblInfo.Text = "取消完畢 !!";
            Application.DoEvents();

            this.Close();
        }

        void h_CompleteAll(object sender, EventArgs e)
        {        
            this.progressBar1.Value = this.progressBar1.Maximum;
            lblInfo.Text = "所有批次已完成, 執行中 ......";

            Application.DoEvents();

            BatchHelper h = sender as BatchHelper;
            h.Commit();

            lblInfo.Text = "執行完畢 !!";
            Application.DoEvents();

            if (Completed != null)
                Completed.Invoke(this, EventArgs.Empty);

            this.Close();
        }

        void h_CompleteOne(object sender, EventArgs e)
        {
            int value = this.progressBar1.Value + 1;
            
            if(value <= this.progressBar1.Maximum)
                this.progressBar1.Value = value;

            lblInfo.Text = "已完成批次 " + this.progressBar1.Value + "/" + this.progressBar1.Maximum;

            Application.DoEvents();
        }
    }
}
