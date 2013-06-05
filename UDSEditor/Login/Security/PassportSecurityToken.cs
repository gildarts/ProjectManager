using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.Login.Security
{
    public class PassportSecurityToken : SecurityToken
    {
        private string _passport;
        public PassportSecurityToken(string passportContent)
        {
            _passport = passportContent;
        }

        public override string TokenType
        {
            get { return "Passport"; }
        }

        protected override string XmlString
        {
            get { return "<SecurityToken Type='Passport'>" + _passport + "</SecurityToken>"; }
        }
    }
}
