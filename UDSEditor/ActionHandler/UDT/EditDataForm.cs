using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDT;
using FISCA.DSAClient;
using System.Xml;
using System.IO;
using ProjectManager.Util;

namespace ProjectManager.ActionHandler.UDT
{
    public partial class EditDataForm : Form
    {
        internal UDTTable Table { get; private set; }
        internal QueryFilter Filters { get; private set; }
        internal FilterConditionForm _filterForm;
        private XmlHelper _source;

        internal EditDataForm(UDTTable udtTable)
        {
            InitializeComponent();
            Table = udtTable;
            Filters = QueryFilter.Empty;
            _filterForm = new FilterConditionForm(this);
        }

        private void EditDataForm_Load(object sender, EventArgs e)
        {
            this.Text = "編輯資料『" + Table.Name + "』";
            _source = new XmlHelper(this.Table.GetContent());
            Execute();
        }

        private void tsbtnExecute_Click(object sender, EventArgs e)
        {
            Execute();
        }

        private void tsbtnFilter_Click(object sender, EventArgs e)
        {
            _filterForm.ShowDialog();
        }

        internal void Execute()
        {
            string condition = "true";
            if (!string.IsNullOrWhiteSpace(Filters.Condition))
                condition = Filters.Condition;

            int limit = -1;
            if (!int.TryParse(tscboLimit.Text, out limit))
                limit = -1;

            if (limit == -1 || limit > 500)
            {
                DialogResult dr = MessageBox.Show("查詢資料筆數無限制或過大, 可能造成操作逾時或無回應, 是否繼續執行?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == System.Windows.Forms.DialogResult.No)
                    return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT uid,to_char(last_update,'YYYY/MM/DD HH24:MI:SS') as last_update,* FROM ").Append("$" + Table.Name).Append(" WHERE ").Append(condition);

            if (Filters.Orders.Count > 0)
            {
                sb.Append(" ORDER BY ");
                foreach (string o in Filters.Orders)
                {
                    sb.Append(o).Append(",");
                }
                sb.Remove(sb.Length - 1, 1);
            }

            if (limit > -1)
                sb.Append(" LIMIT ").Append(limit);

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync(sb.ToString());
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgData.Rows.Clear();
            dgData.Columns.Clear();

            XmlHelper rsp = e.Result as XmlHelper;

            string uidColumnName = string.Empty;
            string lastUpdateColName = string.Empty;

            foreach (XmlElement col in rsp.GetElements("Metadata/Column"))
            {
                string fieldName = col.GetAttribute("Field");
                string type = col.GetAttribute("Type");

                if (fieldName == "uid" && !string.IsNullOrWhiteSpace(uidColumnName))
                    continue;

                if (fieldName == "last_update" && !string.IsNullOrWhiteSpace(lastUpdateColName))
                    continue;

                string fieldName2 = fieldName + "\n" + type;
                string columnName = "col" + col.GetAttribute("Index");
                int index = dgData.Columns.Add(columnName, fieldName2);
                DataGridViewColumn column = dgData.Columns[index];
                column.Tag = col;

                if (fieldName == "uid")
                    uidColumnName = columnName;

                if (fieldName == "last_update")
                    lastUpdateColName = columnName;

                if (UDTTable.IsDefaultField(fieldName))
                    column.ReadOnly = true;
            }

            foreach (XmlElement record in rsp.GetElements("Record"))
            {
                int rowIndex = dgData.Rows.Add();
                DataGridViewRow row = dgData.Rows[rowIndex];
                row.HeaderCell.Value = rowIndex;

                XmlHelper h = new XmlHelper(record);
                foreach (XmlElement col in h.GetElements("Column"))
                {
                    string columnName = "col" + col.GetAttribute("Index");
                    string value = col.InnerText;

                    if (!dgData.Columns.Contains(columnName)) continue;

                    DataGridViewCell cell = row.Cells[columnName];
                    cell.Value = value;
                    cell.Tag = value;

                    if (columnName == uidColumnName)
                        row.Tag = value;
                }
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string sql = e.Argument.ToString();
            XmlHelper h = new XmlHelper();
            XmlElement xml = h.AddElement(".", "SQL");
            XmlNode node = xml.OwnerDocument.CreateCDataSection(sql);
            xml.AppendChild(node);

            Envelope env = MainForm.CurrentProject.SendRequest("UDTService.DML.Query", new Envelope(h));
            XmlHelper rsp = new XmlHelper(env.Body);

            e.Result = rsp;
        }

        //private string _originalValue;        
        //private void dgData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        //{
        //    DataGridViewRow row = dgData.Rows[e.RowIndex];
        //    DataGridViewCell cell = row.Cells[e.ColumnIndex];
        //    cell.Tag = 
        //    _originalValue = row.Cells[e.ColumnIndex].Value + string.Empty;
        //}

        //private void dgData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    DataGridViewRow row = dgData.Rows[e.RowIndex];
        //    DataGridViewCell cell = row.Cells[e.ColumnIndex];
        //    string currentValue = cell.Value + string.Empty;
        //    //TODO valid cell value
        //    cell.ErrorText = string.Empty;
        //    DataGridViewColumn column = cell.OwningColumn;
        //    XmlElement colElement = column.Tag as XmlElement;
        //    string fieldName = colElement.GetAttribute("Field");
        //    XmlElement xml = _source.GetElement("Field[@Name='" + fieldName + "']");
        //    if (xml == null) return;

        //    try
        //    {
        //        UDTTable.ValidFieldValue(xml, currentValue);
        //    }
        //    catch (Exception ex)
        //    {
        //        cell.ErrorText = ex.Message;
        //    }

        //    if (!string.IsNullOrWhiteSpace(cell.ErrorText))
        //        return;

        //    if (!this.IsNewRow(row))
        //    {
        //        if (_originalValue == currentValue) return;
        //        EditCell(cell, false);
        //    }
        //}

        #region edit cell
        //private void EditCell(DataGridViewCell cell, bool isNewRow)
        //{
        //    cell.ErrorText = string.Empty;
        //    DataGridViewRow row = cell.OwningRow;
        //    DataGridViewColumn column = cell.OwningColumn;
        //    string currentValue = cell.Value + string.Empty;

        //    XmlElement columnElement = column.Tag as XmlElement;
        //    string columnText = columnElement.GetAttribute("Field");
        //    string dataType = columnElement.GetAttribute("Type");
        //    DataParam dp = new DataParam(cell.RowIndex, cell.ColumnIndex, Table.Name, columnText, _originalValue, currentValue, isNewRow, dataType);

        //    if (row.Tag != null)
        //        dp.UID = row.Tag.ToString();

        //    BackgroundWorker w = new BackgroundWorker();

        //    w.DoWork += new DoWorkEventHandler(w_DoWork);
        //    w.RunWorkerCompleted += new RunWorkerCompletedEventHandler(w_RunWorkerCompleted);
        //    w.RunWorkerAsync(dp);
        //}

        //void w_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    if (e.Error != null)
        //    {
        //        DataParam dp = DataParam.Parse(e.Error.Message);
        //        Exception ex = e.Error.InnerException;

        //        DataGridViewRow row = dgData.Rows[dp.RowIndex];
        //        DataGridViewCell cell = row.Cells[dp.ColumnIndex];

        //        cell.Value = dp.OriginalValue;
        //        cell.ErrorText = ex.Message;

        //        if (dp.IsNewRow)
        //        {
        //            dgData.Rows.Remove(row);
        //            dgData.Rows[dgData.NewRowIndex].Cells[dp.ColumnIndex].ErrorText = ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        DataParam dp = e.Result as DataParam;

        //        DataGridViewRow row = dgData.Rows[dp.RowIndex];
        //        DataGridViewCell cell = row.Cells[dp.ColumnIndex];

        //        foreach (DataGridViewColumn column in dgData.Columns)
        //        {
        //            XmlElement columnElement = column.Tag as XmlElement;
        //            string fieldName = columnElement.GetAttribute("Field");
        //            if (fieldName == "uid" && dp.IsNewRow)
        //            {
        //                row.Cells[column.Name].Value = dp.UID;
        //                row.Tag = dp.UID;
        //            }
        //            else if (fieldName == "last_update")
        //                row.Cells[column.Name].Value = dp.LastUpdate;
        //        }
        //    }
        //}

        //void w_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    DataParam dp = e.Argument as DataParam;
        //    try
        //    {
        //        XmlHelper h, rsp;
        //        Envelope env;

        //        if (dp.IsNewRow)
        //        {
        //            h = new XmlHelper("<InsertRequest/>");
        //            h.AddElement(".", dp.TableName);
        //            h.AddElement(Table.Name, dp.FieldName, dp.Value);

        //            env = MainForm.CurrentProject.DevConnection.SendRequest("UDTService.DML.Insert", new Envelope(h));
        //            rsp = new XmlHelper(env.Body);
        //            dp.UID = rsp.GetText("NewUID");
        //        }
        //        else
        //        {
        //            h = new XmlHelper("<UpdateRequest/>");
        //            h.AddElement(".", dp.TableName);
        //            h.AddElement(Table.Name, "Field");
        //            h.AddElement(Table.Name + "/Field", dp.FieldName, dp.Value);
        //            h.AddElement(Table.Name, "Condition");
        //            h.AddElement(Table.Name + "/Condition", "Equals", dp.UID);
        //            h.SetAttribute(Table.Name + "/Condition/Equals", "FieldName", "uid");

        //            MainForm.CurrentProject.DevConnection.SendRequest("UDTService.DML.Update", new Envelope(h));
        //        }

        //        h = new XmlHelper("<Request/>");
        //        h.SetAttribute(".", "TableName", Table.Name);
        //        h.AddElement(".", "Field");
        //        h.AddElement("Field", "last_update");
        //        h.AddElement(".", "Condition");
        //        h.AddElement("Condition", "Equals", dp.UID);
        //        h.SetAttribute("Condition/Equals", "FieldName", "uid");

        //        env = MainForm.CurrentProject.DevConnection.SendRequest("UDTService.DML.Select", new Envelope(h));
        //        rsp = new XmlHelper(env.Body);
        //        string lastUpdate = rsp.GetText(Table.Name + "/last_update");
        //        dp.LastUpdate = lastUpdate;

        //        e.Result = dp;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(dp.ToString(), ex);
        //    }
        //}
        #endregion
        private void dgData_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteRows();
        }


        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            DeleteRows();
        }

        private void DeleteRows()
        {
            if (dgData.SelectedRows.Count == 0) return;

            DialogResult dr = MessageBox.Show("被刪除之資料將無法復原, 確定要刪除所選資料 ?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != System.Windows.Forms.DialogResult.Yes) return;

            List<string> list = new List<string>();
            foreach (DataGridViewRow row in dgData.SelectedRows)
            {
                if (row.IsNewRow) continue;
                list.Add(row.Tag.ToString());
            }
            string uids = string.Join(",", list.ToArray());

            XmlHelper h = new XmlHelper();
            XmlElement sqlElement = h.AddElement(".", "Command");
            XmlNode node = sqlElement.OwnerDocument.CreateCDataSection("delete from $" + Table.Name + " where uid in (" + uids + ");");
            sqlElement.AppendChild(node);

            try
            {
                MainForm.CurrentProject.SendRequest("UDTService.DML.Command", new Envelope(h));
            }
            catch (Exception ex)
            {
                MessageBox.Show("移除欄位失敗:\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Execute();
        }

        private void cmExportPlainText_Click(object sender, EventArgs e)
        {
            ExportHelper eh = new ExportHelper(this.Table.Name);
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
            SaveFileDialog sd = new SaveFileDialog();
            sd.FileName = this.Table.Name + ".tbak";
            sd.Filter = "*.tbak|*.tbak";
            DialogResult dr = sd.ShowDialog();

            if (dr != System.Windows.Forms.DialogResult.OK) return;
            File.WriteAllText(sd.FileName, XmlHelper.Format(e.Result), Encoding.UTF8);

            MessageBox.Show("匯出完成", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmImportPlainText_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("執行後會變更現有資料表內的資料!\n 是否繼續?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == System.Windows.Forms.DialogResult.No) return;


            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "*.tbak|*.tbak";
            fd.Multiselect = false;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fd.FileName);

                XmlHelper h = new XmlHelper(doc.DocumentElement);
                List<XmlElement> list = new List<XmlElement>(h.GetElements("Command"));

                BatchImportForm bf = new BatchImportForm(list);
                bf.Completed += new EventHandler(bf_Completed);
                bf.ShowDialog();
            }
        }

        void bf_Completed(object sender, EventArgs e)
        {
            this.Execute();
        }

        private void cmExportCompress_Click(object sender, EventArgs e)
        {
            CompressExportForm ech = new CompressExportForm(this.Table.Name);
            ech.Completed += new EventHandler(ech_Completed);
            ech.StartPosition = FormStartPosition.CenterParent;
            ech.ShowDialog();
        }

        void ech_Completed(object sender, EventArgs e)
        {
            MessageBox.Show("匯出完成", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmImportCompress_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("執行後會變更現有資料表內的資料!\n 是否繼續?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == System.Windows.Forms.DialogResult.No) return;

            DecompressImportForm dif = new DecompressImportForm(this.Table.Name);
            dif.StartPosition = FormStartPosition.CenterParent;
            dif.Completed += new EventHandler<ExportEventArgs>(dif_Completed);
            dif.ShowDialog();
        }

        void dif_Completed(object sender, ExportEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(e.Result);

            XmlHelper h = new XmlHelper(doc.DocumentElement);
            List<XmlElement> list = new List<XmlElement>(h.GetElements("Command"));

            BatchImportForm bf = new BatchImportForm(list);
            bf.Completed += new EventHandler(bf_Completed);
            bf.ShowDialog();
        }

        private void dgData_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow row = dgData.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            bool error = false;
            foreach (DataGridViewColumn column in dgData.Columns)
            {
                DataGridViewCell cell = dgData.Rows[e.RowIndex].Cells[column.Index];
                string currentValue = cell.Value + string.Empty;
                //TODO valid cell value
                cell.ErrorText = string.Empty;
                XmlElement colElement = column.Tag as XmlElement;
                string fieldName = colElement.GetAttribute("Field");
                XmlElement xml = _source.GetElement("Field[@Name='" + fieldName + "']");
                if (xml == null) continue;

                if (UDTTable.IsDefaultField(fieldName)) continue;

                try
                {
                    UDTTable.ValidFieldValue(xml, currentValue);
                }
                catch (Exception ex)
                {
                    cell.ErrorText = ex.Message;
                    error = true;
                }
            }

            if (error)
                e.Cancel = true;
        }

        private void dgData_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgData.Rows[e.RowIndex];

            if (row.IsNewRow) return;

            List<DataGridViewCell> changedCells = this.GetChangedCells(row);

            if (changedCells.Count == 0) return;

            //TODO insert a row data
            XmlHelper h, rsp;
            Envelope env;
            string uid = "-1";

            if (this.IsNewRow(row))
            {
                h = new XmlHelper("<InsertRequest/>");
                h.AddElement(".", this.Table.Name);

                foreach (DataGridViewCell cell in changedCells)
                {
                    string fieldName = this.GetCellFieldName(cell);
                    h.AddElement(Table.Name, fieldName, cell.Value + string.Empty);
                }

                try
                {
                    env = MainForm.CurrentProject.SendRequest("UDTService.DML.Insert", new Envelope(h));
                    rsp = new XmlHelper(env.Body);
                    uid = rsp.GetText("NewUID");
                    row.Tag = uid;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("新增資料失敗 : \n" + ex.Message);
                    return;
                }
            }
            else //update a row
            {
                uid = row.Tag + string.Empty;
                h = new XmlHelper("<UpdateRequest/>");
                h.AddElement(".", this.Table.Name);
                h.AddElement(Table.Name, "Field");
                foreach (DataGridViewCell cell in changedCells)
                {
                    string fieldName = this.GetCellFieldName(cell);
                    h.AddElement(Table.Name + "/Field", fieldName, cell.Value + string.Empty);
                }

                h.AddElement(Table.Name, "Condition");
                h.AddElement(Table.Name + "/Condition", "Equals", uid);
                h.SetAttribute(Table.Name + "/Condition/Equals", "FieldName", "uid");

                try
                {
                    MainForm.CurrentProject.SendRequest("UDTService.DML.Update", new Envelope(h));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改資料失敗 : \n" + ex.Message);
                    return;
                }
            }

            h = new XmlHelper("<Request/>");
            h.SetAttribute(".", "TableName", Table.Name);
            h.AddElement(".", "Field");
            h.AddElement("Field", "last_update");
            h.AddElement(".", "Condition");
            h.AddElement("Condition", "Equals", uid);
            h.SetAttribute("Condition/Equals", "FieldName", "uid");

            env = MainForm.CurrentProject.SendRequest("UDTService.DML.Select", new Envelope(h));
            rsp = new XmlHelper(env.Body);
            string lastUpdate = rsp.GetText(Table.Name + "/last_update");

            foreach (DataGridViewCell cell in row.Cells)
            {
                string fieldName = this.GetCellFieldName(cell);
                if (fieldName == "last_update")
                {
                    cell.Value = lastUpdate;
                    cell.Tag = lastUpdate;
                }
                if (fieldName == "uid")
                {
                    cell.Value = uid;
                    cell.Tag = uid;
                }
            }
        }

        private List<DataGridViewCell> GetChangedCells(DataGridViewRow row)
        {
            List<DataGridViewCell> cells = new List<DataGridViewCell>();
            foreach (DataGridViewCell cell in row.Cells)
            {
                string fieldName = this.GetCellFieldName(cell);
                if (UDTTable.IsDefaultField(fieldName)) continue;

                string oriValue = cell.Tag as string;
                string curValue = cell.Value + string.Empty;

                if (oriValue == curValue) continue;
                cells.Add(cell);
            }

            return cells;
        }

        private string GetCellFieldName(DataGridViewCell cell)
        {
            XmlElement colElement = cell.OwningColumn.Tag as XmlElement;
            return colElement.GetAttribute("Field");
        }

        private bool IsNewRow(DataGridViewRow row)
        {
            if (row.IsNewRow) return true;

            if (row.Tag == null) return true;
            return false;
        }

        private void dgData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewRow row = dgData.Rows[e.RowIndex];

            if (row.IsNewRow) return;

            DataGridViewCell cell = row.Cells[e.ColumnIndex];
            
            string currentValue = cell.EditedFormattedValue + string.Empty;
            //TODO valid cell value
            cell.ErrorText = string.Empty;
            DataGridViewColumn column = cell.OwningColumn;
            XmlElement colElement = column.Tag as XmlElement;
            string fieldName = colElement.GetAttribute("Field");
            XmlElement xml = _source.GetElement("Field[@Name='" + fieldName + "']");
            if (xml == null) return;

            try
            {
                UDTTable.ValidFieldValue(xml, currentValue);
            }
            catch (Exception ex)
            {
                cell.ErrorText = ex.Message;
                //e.Cancel = true;
            }
        }
    }

    //class DataParam
    //{
    //    internal int RowIndex { get; private set; }
    //    internal int ColumnIndex { get; private set; }
    //    internal string TableName { get; private set; }
    //    internal string FieldName { get; private set; }
    //    internal string OriginalValue { get; private set; }
    //    internal string Value { get; private set; }
    //    internal string UID { get; set; }
    //    internal string LastUpdate { get; set; }
    //    internal bool IsNewRow { get; private set; }
    //    internal string DataType { get; private set; }

    //    internal DataParam(int rowindex, int columnindex, string tableName, string fieldName, string original, string value, bool isNewRow, string datatype)
    //    {
    //        this.RowIndex = rowindex;
    //        this.ColumnIndex = columnindex;
    //        this.TableName = tableName;
    //        this.FieldName = fieldName;
    //        this.Value = value;
    //        this.OriginalValue = original;
    //        this.IsNewRow = isNewRow;
    //        this.DataType = datatype;
    //    }

    //    internal new string ToString()
    //    {
    //        XmlHelper h = new XmlHelper();
    //        h.AddElement(".", "RowIndex", this.RowIndex.ToString());
    //        h.AddElement(".", "ColumnIndex", this.ColumnIndex.ToString());
    //        h.AddElement(".", "TableName", this.TableName.ToString());
    //        h.AddElement(".", "FieldName", this.FieldName.ToString());
    //        h.AddElement(".", "Value", this.Value.ToString());
    //        h.AddElement(".", "OriginalValue", this.OriginalValue.ToString());
    //        h.AddElement(".", "NewID", this.UID + string.Empty);
    //        h.AddElement(".", "LastUpdate", this.LastUpdate + string.Empty);
    //        h.AddElement(".", "IsNewRow", this.IsNewRow.ToString());
    //        h.AddElement(".", "DataType", this.DataType);
    //        return h.XmlString;
    //    }

    //    internal static DataParam Parse(string dpString)
    //    {
    //        XmlHelper h = XmlHelper.ParseAsHelper(dpString);
    //        int rowIndex = h.TryGetInteger("RowIndex", -1);
    //        int colIndex = h.TryGetInteger("ColumnIndex", -1);
    //        string tableName = h.GetText("TableName");
    //        string fieldName = h.GetText("FieldName");
    //        string value = h.GetText("Value");
    //        string originalValue = h.GetText("OriginalValue");
    //        bool isNewRow = h.TryGetBoolean("IsNewRow", false);
    //        string dataType = h.GetText("DataType");
    //        DataParam dp = new DataParam(rowIndex, colIndex, tableName, fieldName, originalValue, value, isNewRow, dataType);

    //        return dp;
    //    }
    //}

    class QueryFilter
    {
        internal string Condition { get; set; }
        internal List<string> Orders { get; set; }

        internal QueryFilter()
        {
            Orders = new List<string>();
        }

        internal static QueryFilter Empty
        {
            get
            {
                QueryFilter filter = new QueryFilter();
                filter.Condition = string.Empty;
                return filter;
            }
        }
    }

    class ExportHelper
    {
        internal event EventHandler<ExportEventArgs> Completed;
        internal event EventHandler<ErrorEventArgs> OccurError;
        private string _tableName;

        internal ExportHelper(string tableName)
        {
            _tableName = tableName;
        }

        internal void Export()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT uid,to_char(last_update,'YYYY/MM/DD HH24:MI:SS') as last_update,* FROM ").Append("$" + _tableName);

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync(sb.ToString());
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null && OccurError != null)
            {
                OccurError.Invoke(this, new ErrorEventArgs(e.Error));
                return;
            }

            XmlHelper rsp = e.Result as XmlHelper;

            StringBuilder tmp = new StringBuilder("INSERT INTO \"$");
            tmp.Append(_tableName).Append("\" (");

            StringBuilder tmp2 = new StringBuilder();

            string uidColumnName = string.Empty;
            string lastUpdateColName = string.Empty;
            foreach (XmlElement fieldElement in rsp.GetElements("Metadata/Column"))
            {
                string fieldName = fieldElement.GetAttribute("Field");
                string index = fieldElement.GetAttribute("Index");
                if (fieldName == "uid" && !string.IsNullOrWhiteSpace(uidColumnName))
                    continue;

                if (fieldName == "last_update" && !string.IsNullOrWhiteSpace(lastUpdateColName))
                    continue;

                if (fieldName == "uid")
                    uidColumnName = fieldName;

                if (fieldName == "last_update")
                    lastUpdateColName = fieldName;

                tmp.Append("\"").Append(fieldName).Append("\",");
                tmp2.Append("'@@").Append(index).Append("',");
            }

            tmp.Remove(tmp.Length - 1, 1);
            tmp2.Remove(tmp2.Length - 1, 1);

            tmp.Append(") VALUES (").Append(tmp2).Append(");");
            string sqlTemp = tmp.ToString();

            XmlHelper h = new XmlHelper();
            XmlElement dcmd = h.AddElement(".", "Command");
            XmlNode dsec = dcmd.OwnerDocument.CreateCDataSection("TRUNCATE TABLE \"$" + _tableName + "\";");
            dcmd.AppendChild(dsec);

            foreach (XmlElement recElement in rsp.GetElements("Record"))
            {
                string sql = sqlTemp;
                foreach (XmlElement fieldElement in rsp.GetElements("Metadata/Column"))
                {
                    string index = fieldElement.GetAttribute("Index");
                    string type = fieldElement.GetAttribute("Type");

                    XmlHelper recHelper = new XmlHelper(recElement);
                    string value = recHelper.GetText("Column[@Index='" + index + "']");

                    if (value != string.Empty || type == "String")
                    {
                        value = value.Replace("'", "''");
                        value = value.Replace("\\", "\\\\");
                        sql = sql.Replace("'@@" + index + "'", "'" + value + "'");
                    }
                    else
                        sql = sql.Replace("'@@" + index + "'", "null");
                }

                XmlElement cmd = h.AddElement(".", "Command");
                XmlNode section = cmd.OwnerDocument.CreateCDataSection(sql);
                cmd.AppendChild(section);
            }

            ExportEventArgs arg = new ExportEventArgs(h.XmlString);
            if (Completed != null)
                Completed.Invoke(this, arg);
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            string sql = e.Argument.ToString();
            XmlHelper h = new XmlHelper();
            XmlElement xml = h.AddElement(".", "SQL");
            XmlNode node = xml.OwnerDocument.CreateCDataSection(sql);
            xml.AppendChild(node);

            Envelope env = MainForm.CurrentProject.SendRequest("UDTService.DML.Query", new Envelope(h));
            XmlHelper rsp = new XmlHelper(env.Body);

            e.Result = rsp;
        }
    }

    class ExportEventArgs : EventArgs
    {
        internal string Result { get; private set; }
        internal ExportEventArgs(string result)
        {
            Result = result;
        }
    }
}
