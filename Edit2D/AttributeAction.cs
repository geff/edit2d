using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit2D
{
    [global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class AttributeAction : Attribute
    {
        public AttributeAction()
        {
        }
    }
}
