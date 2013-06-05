using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;
using ProjectManager.Project.UDS.Package;
using ProjectManager.ActionHandler.UDS.Service.Editable;
using ProjectManager.ActionHandler.UDS.Service.Editable.Select;
using ProjectManager.ActionHandler.UDS.Service.Editable.Insert;
using ProjectManager.ActionHandler.UDS.Service.Editable.Update;
using ProjectManager.ActionHandler.UDS.Service.Editable.Delete;
using ProjectManager.ActionHandler.UDS.Service.Editable.Xml;
using ProjectManager.ActionHandler.UDS.Service.Editable.Set;

namespace ProjectManager.ActionHandler.UDS.Service
{
    public class ServiceFactory
    {
        internal static ServiceAction ConvertServiceAction(string serviceAction)
        {
              if (serviceAction == null) serviceAction = string.Empty;
              
            ServiceAction action;
            if (!Enum.TryParse<ServiceAction>(serviceAction, true, out action))
                action = ServiceAction.Select;
            return action;
        }

        internal static IServiceUI CreateServiceUI(string serviceAction)
        {
            ServiceAction action = ConvertServiceAction(serviceAction);
            if (action == ServiceAction.Select)
                return new SelectServiceEditor();
            if (action == ServiceAction.Insert)
                return new InsertServiceEditor();
            if (action == ServiceAction.Update)
                return new UpdateServiceEditor();
            if (action == ServiceAction.Delete)
                return new DeleteServiceEditor();
            if (action == ServiceAction.Set)
                return new SetServiceEditor();
            return new XmlServiceEditor();
        }
    }
}
