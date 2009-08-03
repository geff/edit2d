using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit2D.Action
{
    public interface IActionHandler
    {
        List<Script> ListScript { get; set; }
    }
}
