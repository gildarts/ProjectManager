using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.ActionHandler.Files
{
    class ModifierCollection : List<Modifier>
    {
        public void Import(ModifierCollection list)
        {
            foreach (Modifier m in list)
            {
                this.Add(m);
            }
        }
    }
}
