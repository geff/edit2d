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

        public String TagLineColor { get; set; }

        private void PropertyGridLocal_BackColorChanged(object sender, EventArgs e)
        {
            this.PropertyGrid.ViewBackColor = this.BackColor;
        }

        private void PropertyGridLocal_ForeColorChanged(object sender, EventArgs e)
        {
            this.PropertyGrid.ViewForeColor = this.ForeColor;
        }
    }
}
