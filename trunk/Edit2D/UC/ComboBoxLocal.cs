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

        private Brush backgroundBrushDisabled = Brushes.YellowGreen;
        private Brush foregroundBrushDisabled = Brushes.YellowGreen;

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

                backgroundBrushDisabled = new SolidBrush(Color.DarkGray);
                foregroundBrushDisabled = new SolidBrush(Color.DarkGray);
            }


            if (this.Enabled)
            {
                e.Graphics.FillRectangle(backgroundBrush, e.ClipRectangle);
                e.Graphics.DrawString(this.Text, this.Font, foregroundBrush, 2, 3);
            }
            else
            {
                e.Graphics.FillRectangle(backgroundBrushDisabled, e.ClipRectangle);
                e.Graphics.DrawString(this.Text, this.Font, foregroundBrushDisabled, 2, 3);
            }

            Point position = new Point(this.Width - 15, 3);
            e.Graphics.DrawImage(Resources.icon_Combo, position);
        }
    }
}
