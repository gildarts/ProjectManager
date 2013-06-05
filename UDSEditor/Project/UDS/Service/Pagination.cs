using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project.UDS.Service
{
    class Pagination
    {
        internal bool AllowPagination { get; set; }
        internal int MaxPageSize { get; set; }

        internal Pagination(XmlElement pageElement)
        {
            if (pageElement == null)
            {
                this.AllowPagination = true;
                this.MaxPageSize = 0;
                return;
            }

            XmlHelper h = new XmlHelper(pageElement);
            this.AllowPagination = h.TryGetBoolean("@Allow", true);
            this.MaxPageSize = h.TryGetInteger("MaxPageSize", 0);
        }

        internal Pagination(bool allow, int max)
        {
            this.AllowPagination = allow;
            this.MaxPageSize = max;
        }

        internal XmlElement GetXml()
        {
            XmlHelper h = new XmlHelper("<Pagination/>");
            h.SetAttribute(".", "Allow", this.AllowPagination.ToString());

            string max = string.Empty;
            if (this.MaxPageSize > 0)
            {
                max = this.MaxPageSize.ToString();
                h.SetAttribute(".", "MaxPageSize", max);
            }

            return h.GetElement(".");
        }
    }
}
