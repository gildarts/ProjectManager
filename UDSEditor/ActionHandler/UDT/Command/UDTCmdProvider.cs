using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ProjectManager.ActionHandler.UDT.Command;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDT.Command
{
    class UDTCmdProvider
    {
        public static List<IUDTCommand> ListCmds()
        {
            List<IUDTCommand> cmds = new List<IUDTCommand>();
            cmds.Add(new AddFieldCommand());
            cmds.Add(new AddForeignKeyCommand());
            cmds.Add(new AlterFieldAllowNullCommand());
            cmds.Add(new AlterFieldDataTypeCommand());
            cmds.Add(new AlterFieldDropDefaultCommand());
            cmds.Add(new AlterFieldIndexedCommand());
            cmds.Add(new AlterFieldSetDefaultCommand());
            cmds.Add(new CreateTableCommand());
            cmds.Add(new DropTableCommand());
            cmds.Add(new ImportTableCommand());
            cmds.Add(new RemoveFieldCommand());
            cmds.Add(new RemoveForeignKeyCommand());
            cmds.Add(new RemoveUniqueCommand());
            cmds.Add(new RenameFieldCommand());
            cmds.Add(new RenameTableCommand());
            cmds.Add(new SetUniqueCommand());
            return cmds;
        }

        public static List<string> ListCmdTypes()
        {
            List<string> names = new List<string>();
            foreach (IUDTCommand cmd in ListCmds())
                names.Add(cmd.Type);
            return names;
        }

        public static IUDTCommand GetCmd(string type)
        {
            foreach (IUDTCommand cmd in ListCmds())
            {
                if (cmd.Type.ToLower() == type.ToLower())
                    return cmd;
            }
            throw new Exception("找不到指定的型態 : " + type);
        }

        public static T LoadXml<T>(params XmlElement[] content)
            where T : IUDTCommand, new()
        {
            List<XmlElement> xs = new List<XmlElement>();
            foreach (XmlElement x in content)
                xs.Add(x);

            return LoadXml<T>(xs);
        }

        public static T LoadXml<T>(List<XmlElement> content)
            where T : IUDTCommand, new()
        {
            T t = new T();
            XmlHelper h = new XmlHelper("<Command/>");
            h.SetAttribute(".", "Type", t.Type);
            foreach (XmlElement x in content)
                h.AddElement(".", x);
            t.Result = h.GetElement(".");

            return t;
        }
    }
}
