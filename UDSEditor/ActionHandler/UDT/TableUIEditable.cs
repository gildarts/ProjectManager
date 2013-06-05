using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager.ActionHandler.UDT
{
    class TableUIEditable : AbstractUIEditable
    {
        private TableNodeHandler _tableNode;

        public TableUIEditable(string documentName, TableNodeHandler tableNode)
        {
            this.DocumentName = documentName;
            _tableNode = tableNode;
            _tableNode.Table.Saved += new EventHandler(Table_Saved);
        }

        void Table_Saved(object sender, EventArgs e)
        {
            if (object.ReferenceEquals(this, sender)) return;
            OnInitialEditor();
        }

        protected override void OnInitialEditor()
        {
            TableEditor table = new TableEditor(_tableNode);
            base._editorInstance = table;

            table.DataChanged += new EventHandler(table_DataChanged);
            table.ChangeRecovered += new EventHandler(table_ChangeRecovered);
        }

        void table_ChangeRecovered(object sender, EventArgs e)
        {
            OnChangeRecovered();
        }

        void table_DataChanged(object sender, EventArgs e)
        {
            OnDataChange();
        }

        public override bool Valid
        {
            get 
            {
               TableEditor table = this.Editor as TableEditor;
               return table.Valid;
            }
        }

        public override void Save()
        {
            if (!Valid)
            {
                MessageBox.Show("文件內容有誤, 請修正後再行儲存", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
          
            TableEditor table = this.Editor as TableEditor;
            table.Save();
            this._tableNode.Table.OnSave(this);
        }
    }
}
