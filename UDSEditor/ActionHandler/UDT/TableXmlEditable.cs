using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ProjectManager.Project.UDT;
using System.Windows.Forms;

namespace ProjectManager.ActionHandler.UDT
{
    class TableXmlEditable : AbstractXmlEditable
    {
        internal UDTTable Table { get; private set; }
        internal TableNodeHandler TableNodeHandler {get; private set;}

        public TableXmlEditable(string documentName, TableNodeHandler tableNode)
        {            
            this.Table = tableNode.Table;
            this.TableNodeHandler = tableNode;
            this.DocumentName = documentName;
            this.Table.Saved += new EventHandler(Table_Saved);            
        }

        public override void OnStartEditing()
        {
            XmlElement source = Table.GetContent();

            this.OnInitialEditor(source);
        }

        void Table_Saved(object sender, EventArgs e)
        {
            if (object.ReferenceEquals(sender, this)) return;
            this.OnInitialEditor(this.Table.GetContent());
        }

        public override void Save()
        {
            if (!this.Valid) return;

            XmlEditor xe = this.Editor as XmlEditor;
            TreeNode parent =  this.TableNodeHandler.Node.Parent;
            UDTNodeHandler udtNode = parent.Tag as UDTNodeHandler;

            if (udtNode == null && parent.Parent != null)
                udtNode = parent.Parent.Tag as UDTNodeHandler;

            if (udtNode == null)
                throw new Exception("節點錯誤, 找不到所屬 UDTNodeHandler.");
                        
            udtNode.UDTHandler.UpdateSchema(xe.Xml);
            
            this.Table.OnSave(this);

            this.OnDataSaved();
        }

        public override bool Valid
        {
            get
            {
                XmlEditor xe = this.Editor as XmlEditor;
                xe.ClearError();

                try
                {
                    if (xe.Xml.GetAttribute("Name").ToLower() != this.Table.Name)
                    {
                        xe.SetError("Table 名稱不可變更");
                        return false;
                    }
                    return base.Valid;
                }
                catch (Exception ex)
                {
                    xe.SetError(ex.Message);
                    return false;
                }
            }
        }
    }
}
