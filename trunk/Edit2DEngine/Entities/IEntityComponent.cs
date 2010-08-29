using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit2DEngine.Entities
{
    public interface IEntityComponent : ICloneable
    {
        String Name { get; set; }
        Entity EntityParent { get; set; }
        String TreeViewPath { get; }
        void ChangeLayer();
    }
}
