namespace ProjectManager.ActionHandler.UDS.Service
{
    partial class EditFieldForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFieldForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTarget = new System.Windows.Forms.ComboBox();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.chkQuote = new System.Windows.Forms.CheckBox();
            this.chkRequired = new System.Windows.Forms.CheckBox();
            this.chkAutoNumber = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboInputConverter = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboOutputConverter = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboInputType = new System.Windows.Forms.ComboBox();
            this.cboOutputType = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chkMandatory = new System.Windows.Forms.CheckBox();
            this.cboSourceType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lnkEditVariable = new System.Windows.Forms.LinkLabel();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtSource = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Target";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Source";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Alias";
            // 
            // cboTarget
            // 
            this.cboTarget.FormattingEnabled = true;
            this.cboTarget.Location = new System.Drawing.Point(116, 69);
            this.cboTarget.Name = "cboTarget";
            this.cboTarget.Size = new System.Drawing.Size(262, 24);
            this.cboTarget.TabIndex = 2;
            this.cboTarget.TextChanged += new System.EventHandler(this.cboTarget_TextChanged);
            this.cboTarget.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboTarget_KeyUp);
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(116, 131);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(262, 23);
            this.txtAlias.TabIndex = 4;
            // 
            // chkQuote
            // 
            this.chkQuote.AutoSize = true;
            this.chkQuote.Location = new System.Drawing.Point(116, 170);
            this.chkQuote.Name = "chkQuote";
            this.chkQuote.Size = new System.Drawing.Size(63, 20);
            this.chkQuote.TabIndex = 5;
            this.chkQuote.Text = "Quote";
            this.chkQuote.UseVisualStyleBackColor = true;
            // 
            // chkRequired
            // 
            this.chkRequired.AutoSize = true;
            this.chkRequired.Location = new System.Drawing.Point(212, 170);
            this.chkRequired.Name = "chkRequired";
            this.chkRequired.Size = new System.Drawing.Size(79, 20);
            this.chkRequired.TabIndex = 6;
            this.chkRequired.Text = "Required";
            this.chkRequired.UseVisualStyleBackColor = true;
            // 
            // chkAutoNumber
            // 
            this.chkAutoNumber.AutoSize = true;
            this.chkAutoNumber.Location = new System.Drawing.Point(212, 196);
            this.chkAutoNumber.Name = "chkAutoNumber";
            this.chkAutoNumber.Size = new System.Drawing.Size(101, 20);
            this.chkAutoNumber.TabIndex = 8;
            this.chkAutoNumber.Text = "AutoNumber";
            this.chkAutoNumber.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "InputConverter";
            // 
            // cboInputConverter
            // 
            this.cboInputConverter.FormattingEnabled = true;
            this.cboInputConverter.Location = new System.Drawing.Point(116, 225);
            this.cboInputConverter.Name = "cboInputConverter";
            this.cboInputConverter.Size = new System.Drawing.Size(262, 24);
            this.cboInputConverter.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 258);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "OutputConverter";
            // 
            // cboOutputConverter
            // 
            this.cboOutputConverter.FormattingEnabled = true;
            this.cboOutputConverter.Location = new System.Drawing.Point(116, 255);
            this.cboOutputConverter.Name = "cboOutputConverter";
            this.cboOutputConverter.Size = new System.Drawing.Size(262, 24);
            this.cboOutputConverter.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 289);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "InputType";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 319);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "OutputType";
            // 
            // cboInputType
            // 
            this.cboInputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInputType.FormattingEnabled = true;
            this.cboInputType.Location = new System.Drawing.Point(116, 286);
            this.cboInputType.Name = "cboInputType";
            this.cboInputType.Size = new System.Drawing.Size(262, 24);
            this.cboInputType.TabIndex = 11;
            // 
            // cboOutputType
            // 
            this.cboOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOutputType.FormattingEnabled = true;
            this.cboOutputType.Location = new System.Drawing.Point(116, 316);
            this.cboOutputType.Name = "cboOutputType";
            this.cboOutputType.Size = new System.Drawing.Size(262, 24);
            this.cboOutputType.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(303, 349);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "確定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkMandatory
            // 
            this.chkMandatory.AutoSize = true;
            this.chkMandatory.Location = new System.Drawing.Point(116, 196);
            this.chkMandatory.Name = "chkMandatory";
            this.chkMandatory.Size = new System.Drawing.Size(90, 20);
            this.chkMandatory.TabIndex = 7;
            this.chkMandatory.Text = "Mandatory";
            this.chkMandatory.UseVisualStyleBackColor = true;
            // 
            // cboSourceType
            // 
            this.cboSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSourceType.FormattingEnabled = true;
            this.cboSourceType.Location = new System.Drawing.Point(116, 12);
            this.cboSourceType.Name = "cboSourceType";
            this.cboSourceType.Size = new System.Drawing.Size(262, 24);
            this.cboSourceType.TabIndex = 0;
            this.cboSourceType.SelectedIndexChanged += new System.EventHandler(this.cboSourceType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "SourceType";
            // 
            // lnkEditVariable
            // 
            this.lnkEditVariable.AutoSize = true;
            this.lnkEditVariable.Location = new System.Drawing.Point(254, 39);
            this.lnkEditVariable.Name = "lnkEditVariable";
            this.lnkEditVariable.Size = new System.Drawing.Size(124, 16);
            this.lnkEditVariable.TabIndex = 1;
            this.lnkEditVariable.TabStop = true;
            this.lnkEditVariable.Text = "新增 InternalVariable";
            this.lnkEditVariable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEditVariable_LinkClicked);
            // 
            // err
            // 
            this.err.ContainerControl = this;
            // 
            // txtSource
            // 
            this.txtSource.FormattingEnabled = true;
            this.txtSource.Location = new System.Drawing.Point(116, 99);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(262, 24);
            this.txtSource.TabIndex = 3;
            // 
            // EditFieldForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(420, 399);
            this.Controls.Add(this.lnkEditVariable);
            this.Controls.Add(this.cboSourceType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkMandatory);
            this.Controls.Add(this.chkAutoNumber);
            this.Controls.Add(this.chkRequired);
            this.Controls.Add(this.chkQuote);
            this.Controls.Add(this.txtAlias);
            this.Controls.Add(this.cboOutputType);
            this.Controls.Add(this.cboInputType);
            this.Controls.Add(this.cboOutputConverter);
            this.Controls.Add(this.cboInputConverter);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.cboTarget);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditFieldForm";
            this.Text = "編輯欄位";
            this.Load += new System.EventHandler(this.EditFieldForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboTarget;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.CheckBox chkQuote;
        private System.Windows.Forms.CheckBox chkRequired;
        private System.Windows.Forms.CheckBox chkAutoNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboInputConverter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboOutputConverter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboInputType;
        private System.Windows.Forms.ComboBox cboOutputType;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkMandatory;
        private System.Windows.Forms.ComboBox cboSourceType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lnkEditVariable;
        private System.Windows.Forms.ErrorProvider err;
        private System.Windows.Forms.ComboBox txtSource;
    }
}