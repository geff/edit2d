using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit2D
{
    public static class Common
    {
        public static string CreateNewName<T>(List<T> list, string typeName, string templateName)
        {
            string newName = String.Empty;

                int cmp = 1;

                while (String.IsNullOrEmpty(newName))
                {
                    newName = String.Format(templateName, cmp);

                    if (list.Exists(l => l.GetType().GetProperty(typeName).GetValue(l, null).ToString() == newName))
                    {
                        cmp++;
                        newName = String.Empty;
                    }
                }
            return newName;
        }
    }
}
