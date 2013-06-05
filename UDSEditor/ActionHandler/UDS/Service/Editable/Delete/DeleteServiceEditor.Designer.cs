namespace ProjectManager.ActionHandler.UDS.Service.Editable.Delete
{
    partial class DeleteServiceEditor
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            ActiproSoftware.SyntaxEditor.Document document2 = new ActiproSoftware.SyntaxEditor.Document();
            this.chkRequired = new System.Windows.Forms.CheckBox();
            this.tab = new System.Windows.Forms.TabControl();
            this.tpBasic = new System.Windows.Forms.TabPage();
            this.txtSQLTemplate = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.txtRequestElement = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tpCondition = new System.Windows.Forms.TabPage();
            this.txtConditionSource = new System.Windows.Forms.TextBox();
            this.txtConditionName = new System.Windows.Forms.TextBox();
            this.btnAddCondition = new System.Windows.Forms.Button();
            this.btnEditCondition = new System.Windows.Forms.Button();
            this.btnDeleteCondition = new System.Windows.Forms.Button();
            this.dgConditions = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tpConverter = new System.Windows.Forms.TabPage();
            this.btnDeleteConverter = new System.Windows.Forms.Button();
            this.btnEditConverter = new System.Windows.Forms.Button();
            this.btnAddConverter = new System.Windows.Forms.Button();
            this.dgConverters = new System.Windows.Forms.DataGridView();
            this.colConverterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConverterType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpOther = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgVariables = new System.Windows.Forms.DataGridView();
            this.colVarName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVarSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOthers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemoveVariable = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btnAddVariable = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgProcessor = new System.Windows.Forms.DataGridView();
            this.colProcessorType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProcessorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProcessorSQL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProcessorInvalidMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemoveProcessor = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.btnEditPreprocess = new System.Windows.Forms.Button();
            this.btnAddProcessor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.tab.SuspendLayout();
            this.tpBasic.SuspendLayout();
            this.tpCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgConditions)).BeginInit();
            this.tpConverter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgConverters)).BeginInit();
            this.tpOther.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVariables)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProcessor)).BeginInit();
            this.SuspendLayout();
            // 
            // chkRequired
            // 
            this.chkRequired.AutoSize = true;
            this.chkRequired.Location = new System.Drawing.Point(435, 31);
            this.chkRequired.Name = "chkRequired";
            this.chkRequired.Size = new System.Drawing.Size(120, 16);
            this.chkRequired.TabIndex = 2;
            this.chkRequired.Text = "至少需要一個條件";
            this.chkRequired.UseVisualStyleBackColor = true;
            this.chkRequired.CheckedChanged += new System.EventHandler(this.OnDataChanged);
            // 
            // tab
            // 
            this.tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab.Controls.Add(this.tpBasic);
            this.tab.Controls.Add(this.tpCondition);
            this.tab.Controls.Add(this.tpConverter);
            this.tab.Controls.Add(this.tpOther);
            this.tab.Location = new System.Drawing.Point(28, 74);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(598, 463);
            this.tab.TabIndex = 5;
            // 
            // tpBasic
            // 
            this.tpBasic.Controls.Add(this.txtSQLTemplate);
            this.tpBasic.Controls.Add(this.txtRequestElement);
            this.tpBasic.Controls.Add(this.label4);
            this.tpBasic.Controls.Add(this.label2);
            this.tpBasic.Location = new System.Drawing.Point(4, 25);
            this.tpBasic.Name = "tpBasic";
            this.tpBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tpBasic.Size = new System.Drawing.Size(590, 434);
            this.tpBasic.TabIndex = 0;
            this.tpBasic.Text = "基礎設定";
            this.tpBasic.UseVisualStyleBackColor = true;
            // 
            // txtSQLTemplate
            // 
            this.txtSQLTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSQLTemplate.Document = document2;
            this.txtSQLTemplate.Location = new System.Drawing.Point(103, 44);
            this.txtSQLTemplate.Name = "txtSQLTemplate";
            this.txtSQLTemplate.Size = new System.Drawing.Size(438, 358);
            this.txtSQLTemplate.TabIndex = 2;
            this.txtSQLTemplate.TextChanged += new System.EventHandler(this.OnDataChanged);
            // 
            // txtRequestElement
            // 
            this.txtRequestElement.Location = new System.Drawing.Point(103, 10);
            this.txtRequestElement.Name = "txtRequestElement";
            this.txtRequestElement.Size = new System.Drawing.Size(438, 23);
            this.txtRequestElement.TabIndex = 0;
            this.txtRequestElement.TextChanged += new System.EventHandler(this.OnDataChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "SQL 範本";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "資料節點名稱";
            // 
            // tpCondition
            // 
            this.tpCondition.Controls.Add(this.chkRequired);
            this.tpCondition.Controls.Add(this.txtConditionSource);
            this.tpCondition.Controls.Add(this.txtConditionName);
            this.tpCondition.Controls.Add(this.btnAddCondition);
            this.tpCondition.Controls.Add(this.btnEditCondition);
            this.tpCondition.Controls.Add(this.btnDeleteCondition);
            this.tpCondition.Controls.Add(this.dgConditions);
            this.tpCondition.Controls.Add(this.label7);
            this.tpCondition.Controls.Add(this.label8);
            this.tpCondition.Location = new System.Drawing.Point(4, 22);
            this.tpCondition.Name = "tpCondition";
            this.tpCondition.Padding = new System.Windows.Forms.Padding(3);
            this.tpCondition.Size = new System.Drawing.Size(590, 437);
            this.tpCondition.TabIndex = 2;
            this.tpCondition.Text = "條件設定";
            this.tpCondition.UseVisualStyleBackColor = true;
            // 
            // txtConditionSource
            // 
            this.txtConditionSource.Location = new System.Drawing.Point(187, 28);
            this.txtConditionSource.Name = "txtConditionSource";
            this.txtConditionSource.Size = new System.Drawing.Size(74, 23);
            this.txtConditionSource.TabIndex = 1;
            this.txtConditionSource.Text = "Condition";
            this.txtConditionSource.TextChanged += new System.EventHandler(this.OnDataChanged);
            // 
            // txtConditionName
            // 
            this.txtConditionName.Location = new System.Drawing.Point(62, 28);
            this.txtConditionName.Name = "txtConditionName";
            this.txtConditionName.Size = new System.Drawing.Size(74, 23);
            this.txtConditionName.TabIndex = 0;
            this.txtConditionName.Text = "Condition";
            this.txtConditionName.TextChanged += new System.EventHandler(this.OnDataChanged);
            // 
            // btnAddCondition
            // 
            this.btnAddCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCondition.Location = new System.Drawing.Point(411, 387);
            this.btnAddCondition.Name = "btnAddCondition";
            this.btnAddCondition.Size = new System.Drawing.Size(45, 23);
            this.btnAddCondition.TabIndex = 4;
            this.btnAddCondition.Text = "新增";
            this.btnAddCondition.UseVisualStyleBackColor = true;
            this.btnAddCondition.Click += new System.EventHandler(this.btnAddCondition_Click);
            // 
            // btnEditCondition
            // 
            this.btnEditCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditCondition.Location = new System.Drawing.Point(462, 387);
            this.btnEditCondition.Name = "btnEditCondition";
            this.btnEditCondition.Size = new System.Drawing.Size(45, 23);
            this.btnEditCondition.TabIndex = 5;
            this.btnEditCondition.Text = "編輯";
            this.btnEditCondition.UseVisualStyleBackColor = true;
            this.btnEditCondition.Click += new System.EventHandler(this.btnEditCondition_Click);
            // 
            // btnDeleteCondition
            // 
            this.btnDeleteCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteCondition.Location = new System.Drawing.Point(513, 387);
            this.btnDeleteCondition.Name = "btnDeleteCondition";
            this.btnDeleteCondition.Size = new System.Drawing.Size(45, 23);
            this.btnDeleteCondition.TabIndex = 6;
            this.btnDeleteCondition.Text = "刪除";
            this.btnDeleteCondition.UseVisualStyleBackColor = true;
            this.btnDeleteCondition.Click += new System.EventHandler(this.btnDeleteCondition_Click);
            // 
            // dgConditions
            // 
            this.dgConditions.AllowUserToAddRows = false;
            this.dgConditions.AllowUserToDeleteRows = false;
            this.dgConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgConditions.BackgroundColor = System.Drawing.Color.White;
            this.dgConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgConditions.Location = new System.Drawing.Point(27, 57);
            this.dgConditions.Name = "dgConditions";
            this.dgConditions.ReadOnly = true;
            this.dgConditions.RowTemplate.Height = 24;
            this.dgConditions.Size = new System.Drawing.Size(531, 324);
            this.dgConditions.TabIndex = 3;
            this.dgConditions.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgConditions_RowHeaderMouseDoubleClick);
            this.dgConditions.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.OnDataChanged);
            this.dgConditions.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.OnDataChanged);
            this.dgConditions.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgConditions_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(149, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "來源";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 16);
            this.label8.TabIndex = 6;
            this.label8.Text = "名稱";
            // 
            // tpConverter
            // 
            this.tpConverter.Controls.Add(this.btnDeleteConverter);
            this.tpConverter.Controls.Add(this.btnEditConverter);
            this.tpConverter.Controls.Add(this.btnAddConverter);
            this.tpConverter.Controls.Add(this.dgConverters);
            this.tpConverter.Location = new System.Drawing.Point(4, 22);
            this.tpConverter.Name = "tpConverter";
            this.tpConverter.Padding = new System.Windows.Forms.Padding(3);
            this.tpConverter.Size = new System.Drawing.Size(590, 437);
            this.tpConverter.TabIndex = 5;
            this.tpConverter.Text = "Converter";
            this.tpConverter.UseVisualStyleBackColor = true;
            // 
            // btnDeleteConverter
            // 
            this.btnDeleteConverter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteConverter.Location = new System.Drawing.Point(507, 26);
            this.btnDeleteConverter.Name = "btnDeleteConverter";
            this.btnDeleteConverter.Size = new System.Drawing.Size(57, 23);
            this.btnDeleteConverter.TabIndex = 10;
            this.btnDeleteConverter.Text = "刪除";
            this.btnDeleteConverter.UseVisualStyleBackColor = true;
            this.btnDeleteConverter.Click += new System.EventHandler(this.btnDeleteConverter_Click);
            // 
            // btnEditConverter
            // 
            this.btnEditConverter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditConverter.Location = new System.Drawing.Point(444, 26);
            this.btnEditConverter.Name = "btnEditConverter";
            this.btnEditConverter.Size = new System.Drawing.Size(57, 23);
            this.btnEditConverter.TabIndex = 9;
            this.btnEditConverter.Text = "編輯";
            this.btnEditConverter.UseVisualStyleBackColor = true;
            this.btnEditConverter.Click += new System.EventHandler(this.btnEditConverter_Click);
            // 
            // btnAddConverter
            // 
            this.btnAddConverter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddConverter.Location = new System.Drawing.Point(381, 26);
            this.btnAddConverter.Name = "btnAddConverter";
            this.btnAddConverter.Size = new System.Drawing.Size(57, 23);
            this.btnAddConverter.TabIndex = 8;
            this.btnAddConverter.Text = "新增";
            this.btnAddConverter.UseVisualStyleBackColor = true;
            this.btnAddConverter.Click += new System.EventHandler(this.btnAddConverter_Click);
            // 
            // dgConverters
            // 
            this.dgConverters.AllowUserToAddRows = false;
            this.dgConverters.AllowUserToDeleteRows = false;
            this.dgConverters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgConverters.BackgroundColor = System.Drawing.Color.White;
            this.dgConverters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgConverters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colConverterName,
            this.colConverterType});
            this.dgConverters.Location = new System.Drawing.Point(30, 55);
            this.dgConverters.Name = "dgConverters";
            this.dgConverters.ReadOnly = true;
            this.dgConverters.RowTemplate.Height = 24;
            this.dgConverters.Size = new System.Drawing.Size(534, 313);
            this.dgConverters.TabIndex = 11;
            // 
            // colConverterName
            // 
            this.colConverterName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colConverterName.HeaderText = "Converter Name";
            this.colConverterName.Name = "colConverterName";
            this.colConverterName.ReadOnly = true;
            // 
            // colConverterType
            // 
            this.colConverterType.HeaderText = "Type";
            this.colConverterType.Name = "colConverterType";
            this.colConverterType.ReadOnly = true;
            // 
            // tpOther
            // 
            this.tpOther.Controls.Add(this.tableLayoutPanel1);
            this.tpOther.Location = new System.Drawing.Point(4, 22);
            this.tpOther.Name = "tpOther";
            this.tpOther.Padding = new System.Windows.Forms.Padding(3);
            this.tpOther.Size = new System.Drawing.Size(590, 437);
            this.tpOther.TabIndex = 4;
            this.tpOther.Text = "進階設定";
            this.tpOther.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 431);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgVariables);
            this.panel1.Controls.Add(this.btnRemoveVariable);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.btnAddVariable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 209);
            this.panel1.TabIndex = 3;
            // 
            // dgVariables
            // 
            this.dgVariables.AllowUserToAddRows = false;
            this.dgVariables.AllowUserToDeleteRows = false;
            this.dgVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgVariables.BackgroundColor = System.Drawing.Color.White;
            this.dgVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVariables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVarName,
            this.colVarSource,
            this.colOthers});
            this.dgVariables.Location = new System.Drawing.Point(21, 39);
            this.dgVariables.Name = "dgVariables";
            this.dgVariables.RowTemplate.Height = 24;
            this.dgVariables.Size = new System.Drawing.Size(543, 155);
            this.dgVariables.TabIndex = 2;
            this.dgVariables.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.OnDataChanged);
            this.dgVariables.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.OnDataChanged);
            // 
            // colVarName
            // 
            this.colVarName.HeaderText = "Name";
            this.colVarName.Name = "colVarName";
            // 
            // colVarSource
            // 
            this.colVarSource.HeaderText = "Source";
            this.colVarSource.Name = "colVarSource";
            // 
            // colOthers
            // 
            this.colOthers.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colOthers.HeaderText = "Others";
            this.colOthers.Name = "colOthers";
            // 
            // btnRemoveVariable
            // 
            this.btnRemoveVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveVariable.Location = new System.Drawing.Point(520, 10);
            this.btnRemoveVariable.Name = "btnRemoveVariable";
            this.btnRemoveVariable.Size = new System.Drawing.Size(45, 23);
            this.btnRemoveVariable.TabIndex = 1;
            this.btnRemoveVariable.Text = "刪除";
            this.btnRemoveVariable.UseVisualStyleBackColor = true;
            this.btnRemoveVariable.Click += new System.EventHandler(this.btnRemoveVariable_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(124, 16);
            this.label13.TabIndex = 14;
            this.label13.Text = "InternalVariable 設定";
            // 
            // btnAddVariable
            // 
            this.btnAddVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddVariable.Location = new System.Drawing.Point(469, 10);
            this.btnAddVariable.Name = "btnAddVariable";
            this.btnAddVariable.Size = new System.Drawing.Size(45, 23);
            this.btnAddVariable.TabIndex = 0;
            this.btnAddVariable.Text = "新增";
            this.btnAddVariable.UseVisualStyleBackColor = true;
            this.btnAddVariable.Click += new System.EventHandler(this.btnAddVariable_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgProcessor);
            this.panel2.Controls.Add(this.btnRemoveProcessor);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.btnEditPreprocess);
            this.panel2.Controls.Add(this.btnAddProcessor);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 218);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(578, 210);
            this.panel2.TabIndex = 4;
            // 
            // dgProcessor
            // 
            this.dgProcessor.AllowUserToAddRows = false;
            this.dgProcessor.AllowUserToDeleteRows = false;
            this.dgProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgProcessor.BackgroundColor = System.Drawing.Color.White;
            this.dgProcessor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProcessor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProcessorType,
            this.colProcessorName,
            this.colProcessorSQL,
            this.colProcessorInvalidMessage});
            this.dgProcessor.Location = new System.Drawing.Point(19, 42);
            this.dgProcessor.Name = "dgProcessor";
            this.dgProcessor.RowTemplate.Height = 24;
            this.dgProcessor.Size = new System.Drawing.Size(543, 156);
            this.dgProcessor.TabIndex = 17;
            this.dgProcessor.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgProcessor_RowHeaderMouseDoubleClick);
            this.dgProcessor.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.OnDataChanged);
            this.dgProcessor.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.OnDataChanged);
            this.dgProcessor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgProcessor_KeyUp);
            // 
            // colProcessorType
            // 
            this.colProcessorType.HeaderText = "Type";
            this.colProcessorType.Name = "colProcessorType";
            // 
            // colProcessorName
            // 
            this.colProcessorName.HeaderText = "Name";
            this.colProcessorName.Name = "colProcessorName";
            // 
            // colProcessorSQL
            // 
            this.colProcessorSQL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProcessorSQL.HeaderText = "SQL";
            this.colProcessorSQL.Name = "colProcessorSQL";
            // 
            // colProcessorInvalidMessage
            // 
            this.colProcessorInvalidMessage.HeaderText = "InvalidMessage";
            this.colProcessorInvalidMessage.Name = "colProcessorInvalidMessage";
            // 
            // btnRemoveProcessor
            // 
            this.btnRemoveProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveProcessor.Location = new System.Drawing.Point(518, 13);
            this.btnRemoveProcessor.Name = "btnRemoveProcessor";
            this.btnRemoveProcessor.Size = new System.Drawing.Size(45, 23);
            this.btnRemoveProcessor.TabIndex = 16;
            this.btnRemoveProcessor.Text = "刪除";
            this.btnRemoveProcessor.UseVisualStyleBackColor = true;
            this.btnRemoveProcessor.Click += new System.EventHandler(this.btnRemoveProcessor_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(96, 16);
            this.label15.TabIndex = 18;
            this.label15.Text = "Preprocess 設定";
            // 
            // btnEditPreprocess
            // 
            this.btnEditPreprocess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditPreprocess.Location = new System.Drawing.Point(467, 13);
            this.btnEditPreprocess.Name = "btnEditPreprocess";
            this.btnEditPreprocess.Size = new System.Drawing.Size(45, 23);
            this.btnEditPreprocess.TabIndex = 15;
            this.btnEditPreprocess.Text = "編輯";
            this.btnEditPreprocess.UseVisualStyleBackColor = true;
            this.btnEditPreprocess.Click += new System.EventHandler(this.btnEditPreprocess_Click);
            // 
            // btnAddProcessor
            // 
            this.btnAddProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddProcessor.Location = new System.Drawing.Point(416, 13);
            this.btnAddProcessor.Name = "btnAddProcessor";
            this.btnAddProcessor.Size = new System.Drawing.Size(45, 23);
            this.btnAddProcessor.TabIndex = 15;
            this.btnAddProcessor.Text = "新增";
            this.btnAddProcessor.UseVisualStyleBackColor = true;
            this.btnAddProcessor.Click += new System.EventHandler(this.btnAddProcessor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Service 名稱";
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(118, 45);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.ReadOnly = true;
            this.txtServiceName.Size = new System.Drawing.Size(484, 23);
            this.txtServiceName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "服務類別 : 刪除 ( Delete )";
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Location = new System.Drawing.Point(509, 543);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(113, 23);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "Service XML 預覽";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // DeleteServiceEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tab);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServiceName);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DeleteServiceEditor";
            this.Size = new System.Drawing.Size(645, 577);
            this.tab.ResumeLayout(false);
            this.tpBasic.ResumeLayout(false);
            this.tpBasic.PerformLayout();
            this.tpCondition.ResumeLayout(false);
            this.tpCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgConditions)).EndInit();
            this.tpConverter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgConverters)).EndInit();
            this.tpOther.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVariables)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProcessor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkRequired;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tpBasic;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor txtSQLTemplate;
        private System.Windows.Forms.TextBox txtRequestElement;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tpCondition;
        private System.Windows.Forms.TextBox txtConditionSource;
        private System.Windows.Forms.TextBox txtConditionName;
        private System.Windows.Forms.Button btnAddCondition;
        private System.Windows.Forms.Button btnEditCondition;
        private System.Windows.Forms.Button btnDeleteCondition;
        private System.Windows.Forms.DataGridView dgConditions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tpOther;
        private System.Windows.Forms.Button btnAddVariable;
        private System.Windows.Forms.Button btnRemoveVariable;
        private System.Windows.Forms.DataGridView dgVariables;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVarName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVarSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOthers;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgProcessor;
        private System.Windows.Forms.Button btnRemoveProcessor;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnAddProcessor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessorType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessorSQL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessorInvalidMessage;
        private System.Windows.Forms.Button btnEditPreprocess;
        private System.Windows.Forms.TabPage tpConverter;
        private System.Windows.Forms.Button btnDeleteConverter;
        private System.Windows.Forms.Button btnEditConverter;
        private System.Windows.Forms.Button btnAddConverter;
        private System.Windows.Forms.DataGridView dgConverters;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConverterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConverterType;

    }
}
