using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Edit2D.UC
{
    public partial class PropertyGridLocal : UserControl
    {
        public PropertyGridLocal()
        {
            InitializeComponent();
        }

        public Object TagProp
        {
            get
            {
                Object tagProp = this.PropertyGrid.Tag;
                return tagProp;
            }
            set
            {
                Object tagProp = value;
                this.PropertyGrid.Tag = tagProp;
            }

        }
    }
}
