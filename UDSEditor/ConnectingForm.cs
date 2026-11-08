using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectManager
{
    /// <summary>
    /// 背景工作進行中顯示的等待視窗，可以取消。
    /// 工作完成 → DialogResult.OK；按取消 → DialogResult.Cancel（背景工作無法中斷，結果直接丟掉）。
    /// </summary>
    internal class ConnectingForm : Form
    {
        private readonly Task _task;

        internal ConnectingForm(string message, Task task)
        {
            _task = task;

            this.Text = "連線中";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(380, 110);
            this.Font = new Font("微軟正黑體", 9.75F);

            Label lbl = new Label();
            lbl.AutoSize = false;
            lbl.Location = new Point(15, 15);
            lbl.Size = new Size(350, 45);
            lbl.Text = message;

            Button btnCancel = new Button();
            btnCancel.Text = "取消";
            btnCancel.Size = new Size(85, 30);
            btnCancel.Location = new Point(280, 68);
            btnCancel.DialogResult = DialogResult.Cancel;

            this.Controls.Add(lbl);
            this.Controls.Add(btnCancel);
            this.CancelButton = btnCancel;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            _task.ContinueWith(t =>
            {
                if (!this.IsDisposed && this.Visible)
                    this.DialogResult = DialogResult.OK;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
