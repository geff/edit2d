using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit2DEngine
{
    [global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class AttributeAction : Attribute
    {
        public AttributeAction()
        {
        }
    }
}
