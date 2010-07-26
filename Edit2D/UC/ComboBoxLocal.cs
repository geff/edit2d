using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Edit2D.Properties;

namespace Edit2D.UC
{
    public partial class ComboBoxLocal : ComboBox
    {
        private Brush backgroundBrush = Brushes.YellowGreen;
        private Brush foregroundBrush = Brushes.YellowGreen;
        private Brush mouseOverBrush = Brushes.YellowGreen;

        public ComboBoxLocal()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.FlatStyle = FlatStyle.Flat;
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();

            if(e.State == DrawItemState.HotLight)
                e.Graphics.FillRectangle(mouseOverBrush, e.Bounds);

            if(e.Index != -1)
                e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, foregroundBrush, 2, 2 + e.Index*this.ItemHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (backgroundBrush == Brushes.YellowGreen)
            {
                backgroundBrush = new SolidBrush(this.BackColor);
                foregroundBrush = new SolidBrush(this.ForeColor);
                mouseOverBrush = new SolidBrush(WinformVisualStyle.MouseOverColor);
            }

            e.Graphics.FillRectangle(backgroundBrush, e.ClipRectangle);

            //Rectangle recArrow = new Rectangle(e.ClipRectangle.Width - 20, 0, 20, e.ClipRectangle.Height);
            //ComboBoxRenderer.DrawDropDownButton(e.Graphics, recArrow, System.Windows.Forms.VisualStyles.ComboBoxState.Normal);

            Point position = new Point(e.ClipRectangle.Width - 15, 3);

            e.Graphics.DrawImage(Resources.icon_Combo, position);

            e.Graphics.DrawString(this.Text, this.Font, foregroundBrush, 2, 3);
        }
    }
}
