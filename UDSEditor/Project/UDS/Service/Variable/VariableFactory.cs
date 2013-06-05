using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ProjectManager.Project.UDS.Service.Variable
{
    class VariableFactory
    {
        internal static List<string> VariableTypes
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("UserInfo");
                list.Add("ClientInfo");
                list.Add("Literal");
                list.Add("Database");
                list.Add("Resource");
                list.Add("Request");
                list.Add("UUID");
                return list;
            }
        }

        internal static IVariable Parse(XmlElement varElement)
        {
            string source = varElement.GetAttribute("Source").ToLower();
            if (source == "userinfo")            
                return new UserInfoVariable(varElement);
            if (source == "literal")
                return new LiteralVariable(varElement);
            if(source == "database")
                return new SQLVariable(varElement);
            if (source == "clientinfo")
                return new ClientInfoVariable(varElement);
            if (source == "resource")
                return new ResourceVariable(varElement);
            if (source == "request")
                return new RequestVariable(varElement);
            if (source == "uuid")
                return new UUIDVariable(varElement);
            return null;
        }

        internal static IVariable Create(string source)
        {
            source = source.ToLower();
            if (source == "userinfo")
                return new UserInfoVariable();
            if (source == "literal")
                return new LiteralVariable();
            if (source == "database")
                return new SQLVariable();
            if (source == "clientinfo")
                return new ClientInfoVariable();
            if (source == "resource")
                return new ResourceVariable();
            if (source == "request")
                return new RequestVariable();
            if (source == "uuid")
                return new UUIDVariable();
            return null;
        }
    }
}
