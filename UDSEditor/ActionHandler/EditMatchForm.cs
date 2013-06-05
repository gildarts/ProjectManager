using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using FISCA.DSAClient;
using ProjectManager.Project.UDT;
using ProjectManager.ActionHandler.UDT.Command;

namespace ProjectManager.ActionHandler
{
    public partial class EditMatchForm : Form
    {
        private DataGridView _datagrid;

        public EditMatchForm(DataGridView datagrid)
        {
            InitializeComponent();
            _datagrid = datagrid;
        }

        private void btnBrows_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TSML Files (.tsml)|*.tsml";
            DialogResult dr = ofd.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
                txtPreVersion.Text = ofd.FileName;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtPreVersion.Text))
            {
                MessageBox.Show("指定檔案不存在", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(txtPreVersion.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("載入檔案失敗 : " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _datagrid.Rows.Clear();

            //取出目前專案的 UDT 資料表
            XmlHelper curHelper = new XmlHelper("<Content />");
            foreach (UDTTable table in MainForm.CurrentUDT.Tables)
                curHelper.AddElement(".", table.GetContent());

            XmlHelper preHelper = new XmlHelper(doc.DocumentElement);

            #region 比對欲新增之資料表
            foreach (XmlElement curTable in curHelper.GetElements("Table"))
            {
                string curTableName = curTable.GetAttribute("Name");
                XmlElement preTable = preHelper.GetElement("Table[@Name='" + curTableName + "']");

                if (preTable == null) //之前版本無此 table
                {
                    ImportTableCommand cmd = UDTCmdProvider.LoadXml<ImportTableCommand>(curTable);
                    AddCommand(cmd);
                    continue;
                }

                XmlHelper preTableHelper = new XmlHelper(preTable);
                XmlHelper curTableHelper = new XmlHelper(curTable);
                XmlElement tableNameElement = GenTableNameElement(curTableName);
                //比對欄位
                #region 比對欄位
                foreach (XmlElement curField in curTableHelper.GetElements("Field"))
                {
                    string curFieldName = curField.GetAttribute("Name");
                    XmlElement preField = preTableHelper.GetElement("Field[@Name='" + curFieldName + "']");

                    if (preField == null)
                    {
                        AddFieldCommand cmd = UDTCmdProvider.LoadXml<AddFieldCommand>(tableNameElement, curField);
                        AddCommand(cmd);
                        continue;
                    }

                    string curDataType = curField.GetAttribute("DataType");
                    string preDataType = preField.GetAttribute("DataType");
                    XmlElement fieldNameElement = this.GenFieldNameElement(curFieldName);
                    if (curDataType != preDataType)
                    {
                        AlterFieldDataTypeCommand cmd = UDTCmdProvider.LoadXml<AlterFieldDataTypeCommand>(tableNameElement, fieldNameElement, GenElement("DataType", curDataType));
                        AddCommand(cmd);
                    }

                    XmlHelper curFieldHelper = new XmlHelper(curField);
                    XmlHelper preFieldHelper = new XmlHelper(preField);

                    bool curIndex = curFieldHelper.TryGetBoolean("@Indexed", false);
                    bool preIndex = preFieldHelper.TryGetBoolean("@Indexed", false);
                    if (curIndex != preIndex)
                    {
                        AlterFieldIndexedCommand cmd = UDTCmdProvider.LoadXml<AlterFieldIndexedCommand>(tableNameElement, fieldNameElement, GenElement("Indexed", curIndex.ToString()));
                        AddCommand(cmd);
                    }

                    bool curAllowNull = curFieldHelper.TryGetBoolean("@AllowNull", true);
                    bool preAllowNull = preFieldHelper.TryGetBoolean("@AllowNull", true);
                    if (curAllowNull != preAllowNull)
                    {
                        AlterFieldAllowNullCommand cmd = UDTCmdProvider.LoadXml<AlterFieldAllowNullCommand>(tableNameElement, fieldNameElement, GenElement("AllowNull", curAllowNull.ToString()));
                        AddCommand(cmd);
                    }

                    string curDefault = curField.GetAttribute("Default");
                    string preDefault = preField.GetAttribute("Default");
                    if (curDefault != preDefault)
                    {
                        IUDTCommand cmd;
                        if (string.IsNullOrWhiteSpace(curDefault))
                            cmd = UDTCmdProvider.LoadXml<AlterFieldDropDefaultCommand>(tableNameElement, fieldNameElement);
                        else
                            cmd = UDTCmdProvider.LoadXml<AlterFieldSetDefaultCommand>(tableNameElement, fieldNameElement, GenElement("Default", curDefault));

                        AddCommand(cmd);
                    }
                }
                #endregion

                //反過來欄位比對, 之前有現在沒有就刪除
                #region 反過來欄位比對, 之前有現在沒有就刪除
                foreach (XmlElement preField in preTableHelper.GetElements("Field"))
                {
                    string preFieldName = preField.GetAttribute("Name");
                    XmlElement curField = curTableHelper.GetElement("Field[@Name='" + preFieldName + "']");

                    if (curField == null)
                    {
                        IUDTCommand cmd = UDTCmdProvider.LoadXml<RemoveFieldCommand>(tableNameElement, GenFieldNameElement(preFieldName));
                        AddCommand(cmd);
                        continue;
                    }
                }
                #endregion

                #region Unique 比較
                //比對 Unique 舊版不存在就新增
                foreach (XmlElement curUniq in curTableHelper.GetElements("Unique"))
                {
                    string curUniqName = curUniq.GetAttribute("Name");
                    XmlElement preUniq = preTableHelper.GetElement("Unique[@Name='" + curUniqName + "']");

                    if (preUniq == null)
                    {
                        XmlHelper th = XmlHelper.ParseAsHelper("<Table />");
                        th.SetAttribute(".", "Name", curTableName);
                        th.AddElement(".", curUniq);
                        IUDTCommand cmd = UDTCmdProvider.LoadXml<SetUniqueCommand>(th.GetElement("."));
                        AddCommand(cmd);
                    }
                }
                //反過來比對 Unique 新版不存在就刪除
                foreach (XmlElement preUniq in preTableHelper.GetElements("Unique"))
                {
                    string preUniqName = preUniq.GetAttribute("Name");
                    XmlElement curUniq = curTableHelper.GetElement("Unique[@Name='"+preUniqName+"']");
                    if (curUniq == null)
                    {
                        IUDTCommand cmd = UDTCmdProvider.LoadXml<RemoveUniqueCommand>(GenTableNameElement(curTableName), GenElement("UniqueName", preUniqName));
                        AddCommand(cmd);
                    }
                }
                #endregion

                #region ForeignKey 比較
                //比對 Fk, 舊版不存在就新增
                foreach (XmlElement curFk in curTableHelper.GetElements("ForeignKey"))
                {
                    string curFkName = curFk.GetAttribute("Name");
                    XmlElement preFk = preTableHelper.GetElement("ForeignKey[@Name='" + curFkName + "']");

                    if (preFk == null)
                    {    
                        IUDTCommand cmd = UDTCmdProvider.LoadXml<AddForeignKeyCommand>(curFk);
                        AddCommand(cmd);
                    }
                }
                //反過來比對 Fk, 新版不存在就刪除
                foreach (XmlElement preFk in preTableHelper.GetElements("ForeignKey"))
                {
                    string preFkName = preFk.GetAttribute("Name");
                    XmlElement curFk = curTableHelper.GetElement("ForeignKey[@Name='" + preFkName + "']");
                    if (curFk == null)
                    {
                        IUDTCommand cmd = UDTCmdProvider.LoadXml<RemoveForeignKeyCommand>(GenTableNameElement(curTableName), GenElement("ForeignKeyName", preFkName));
                        AddCommand(cmd);
                    }
                }
                #endregion
            }
            #endregion

            #region 比對欲刪之資料表
            foreach (XmlElement preTable in preHelper.GetElements("Table"))
            {
                string preTableName = preTable.GetAttribute("Name");
                XmlElement curTable = curHelper.GetElement("Table[@Name='" + preTableName + "']");

                if (curTable == null)
                {
                    IUDTCommand cmd = UDTCmdProvider.LoadXml<DropTableCommand>(GenTableNameElement(preTableName));
                    AddCommand(cmd);
                }
            }
            #endregion

            this.Close();
        }

        #region generate Element
        private XmlElement GenTableNameElement(string tableName)
        {
            return GenElement("TableName", tableName);
        }

        private XmlElement GenFieldNameElement(string fieldName)
        {
            return GenElement("FieldName", fieldName);
        }

        private XmlElement GenElement(string elementName, string value)
        {
            return XmlHelper.ParseAsDOM("<" + elementName + ">" + value + "</" + elementName + ">");
        }
        #endregion
        private void AddCommand(IUDTCommand cmd)
        {
            lblInfo.Text = "增加 Command : " + cmd.Description;
            int index = _datagrid.Rows.Add();
            DataGridViewRow row = _datagrid.Rows[index];
            row.Tag = cmd;
            row.Cells["colType"].Value = cmd.Type;
            row.Cells["colDesc"].Value = cmd.Description;

            Application.DoEvents();
        }

        private void EditMatchForm_Load(object sender, EventArgs e)
        {
            lblInfo.Text = string.Empty;
        }
    }
}
