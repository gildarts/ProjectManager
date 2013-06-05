using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;

namespace ProjectManager.Login.Security
{
    class PublicSecurityToken:SecurityToken
    {
        public override string TokenType
        {
            get { return "Public"; }
        }

        protected override string XmlString
        {
            get { return "<SecurityToken Type='Public' />"; }
        }
    }
}
