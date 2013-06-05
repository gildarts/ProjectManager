namespace ProjectManager.ActionHandler.UDS.Service
{
    partial class EditConditionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditConditionForm));
            this.lnkEditVariable = new System.Windows.Forms.LinkLabel();
            this.cboSourceType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chkRequired = new System.Windows.Forms.CheckBox();
            this.chkQuote = new System.Windows.Forms.CheckBox();
            this.cboInputConverter = new System.Windows.Forms.ComboBox();
            this.cboTarget = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboComparer = new System.Windows.Forms.ComboBox();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtSource = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            this.SuspendLayout();
            // 
            // lnkEditVariable
            // 
            this.lnkEditVariable.AutoSize = true;
            this.lnkEditVariable.Location = new System.Drawing.Point(251, 39);
            this.lnkEditVariable.Name = "lnkEditVariable";
            this.lnkEditVariable.Size = new System.Drawing.Size(124, 16);
            this.lnkEditVariable.TabIndex = 1;
            this.lnkEditVariable.TabStop = true;
            this.lnkEditVariable.Text = "新增 InternalVariable";
            this.lnkEditVariable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEditVariable_LinkClicked);
            // 
            // cboSourceType
            // 
            this.cboSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSourceType.FormattingEnabled = true;
            this.cboSourceType.Location = new System.Drawing.Point(113, 12);
            this.cboSourceType.Name = "cboSourceType";
            this.cboSourceType.Size = new System.Drawing.Size(262, 24);
            this.cboSourceType.TabIndex = 0;
            this.cboSourceType.SelectedIndexChanged += new System.EventHandler(this.cboSourceType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "SourceType";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(290, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "確定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkRequired
            // 
            this.chkRequired.AutoSize = true;
            this.chkRequired.Location = new System.Drawing.Point(286, 142);
            this.chkRequired.Name = "chkRequired";
            this.chkRequired.Size = new System.Drawing.Size(79, 20);
            this.chkRequired.TabIndex = 6;
            this.chkRequired.Text = "Required";
            this.chkRequired.UseVisualStyleBackColor = true;
            // 
            // chkQuote
            // 
            this.chkQuote.AutoSize = true;
            this.chkQuote.Location = new System.Drawing.Point(217, 142);
            this.chkQuote.Name = "chkQuote";
            this.chkQuote.Size = new System.Drawing.Size(63, 20);
            this.chkQuote.TabIndex = 5;
            this.chkQuote.Text = "Quote";
            this.chkQuote.UseVisualStyleBackColor = true;
            // 
            // cboInputConverter
            // 
            this.cboInputConverter.FormattingEnabled = true;
            this.cboInputConverter.Location = new System.Drawing.Point(103, 168);
            this.cboInputConverter.Name = "cboInputConverter";
            this.cboInputConverter.Size = new System.Drawing.Size(262, 24);
            this.cboInputConverter.TabIndex = 7;
            // 
            // cboTarget
            // 
            this.cboTarget.FormattingEnabled = true;
            this.cboTarget.Location = new System.Drawing.Point(103, 76);
            this.cboTarget.Name = "cboTarget";
            this.cboTarget.Size = new System.Drawing.Size(262, 24);
            this.cboTarget.TabIndex = 2;
            this.cboTarget.TextChanged += new System.EventHandler(this.cboTarget_TextChanged);
            this.cboTarget.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboTarget_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Source";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "InputConverter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Target";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 16);
            this.label3.TabIndex = 29;
            this.label3.Text = "Comparer";
            // 
            // cboComparer
            // 
            this.cboComparer.FormattingEnabled = true;
            this.cboComparer.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<",
            "<=",
            "IN",
            "Like",
            "CONTAINS",
            "StartWith",
            "EndWith",
            "~*",
            "ILIKE",
            "ICONTAINS",
            "ISTARTWITH",
            "IENDWITH"});
            this.cboComparer.Location = new System.Drawing.Point(103, 138);
            this.cboComparer.Name = "cboComparer";
            this.cboComparer.Size = new System.Drawing.Size(92, 24);
            this.cboComparer.TabIndex = 4;
            this.cboComparer.Text = "=";
            // 
            // err
            // 
            this.err.ContainerControl = this;
            // 
            // txtSource
            // 
            this.txtSource.FormattingEnabled = true;
            this.txtSource.Location = new System.Drawing.Point(103, 108);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(262, 24);
            this.txtSource.TabIndex = 2;
            // 
            // EditConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 262);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lnkEditVariable);
            this.Controls.Add(this.cboSourceType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkRequired);
            this.Controls.Add(this.chkQuote);
            this.Controls.Add(this.cboInputConverter);
            this.Controls.Add(this.cboComparer);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.cboTarget);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditConditionForm";
            this.Text = "編輯條件";
            this.Load += new System.EventHandler(this.EditConditionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkEditVariable;
        private System.Windows.Forms.ComboBox cboSourceType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkRequired;
        private System.Windows.Forms.CheckBox chkQuote;
        private System.Windows.Forms.ComboBox cboInputConverter;
        private System.Windows.Forms.ComboBox cboTarget;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboComparer;
        private System.Windows.Forms.ErrorProvider err;
        private System.Windows.Forms.ComboBox txtSource;

    }
}