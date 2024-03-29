﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using Xna.Tools;
using Edit2D.ScriptControl;
using Edit2D.UC;

namespace Edit2D
{
    public static class WinformVisualStyle
    {
        public static String CurrentVisualStyleName = String.Empty;
        public static Color BackColorDark = Color.White;
        public static Color BackColorLight = Color.White;
        public static Color BorderColor = Color.White;
        public static Color ForeColor1 = Color.White;
        public static Color ForeColor2 = Color.White;
        public static Color MouseOverColor = Color.White;
        public static Color SelectedColor = Color.White;

        public static Brush BrushBackColorDark = Brushes.White;
        public static Brush BrushBackColorLight = Brushes.White;
        public static Brush BrushSelectedColor = Brushes.White;
        public static Brush BrushMouseOverColor = Brushes.White;
        public static Brush BrushForeColor1 = Brushes.White;

        public static void ApplyStyle(Control ctrl, string visualStyleName)
        {
            CurrentVisualStyleName = visualStyleName;

            if (OpenVisualStyle(visualStyleName))
            {
                ApplyStyleRecursively(ctrl, null);
            }
        }

        public static void ApplyStyle(Control ctrl)
        {
            ApplyStyle(ctrl, CurrentVisualStyleName);
        }

        private static void ApplyStyleRecursively(Control ctrl, Control ctrlParent)
        {
            ApplyStyleOnControl(ctrl, ctrlParent);

            foreach (Control ctrlChild in ctrl.Controls)
            {
                ApplyStyleRecursively(ctrlChild, ctrl);
            }
        }

        private static void ApplyStyleOnControl(Control ctrl, Control ctrlParent)
        {
            if (!(ctrl is GridControl || ctrlParent is PropertyGrid))
                ApplyStyleControl(ctrl);

            if (ctrl is ButtonBase)
            {
                ApplyStyleButton((ButtonBase)ctrl);
            }
            else if (ctrl is Panel)
            {
                ApplyStylePanel((Panel)ctrl);
            }
            else if (ctrl is TextBoxBase)
            {
                ApplyStyleTextbox((TextBoxBase)ctrl);
            }
            else if (ctrl is ListControl)
            {
                ApplyStyleList((ListControl)ctrl);
            }
            else if (ctrl is PropertyGridLocal)
            {
                ApplyStylePropertyGrid((PropertyGridLocal)ctrl);
            }
            else if (ctrl is TabPage)
            {
                ApplyStyleTabPage((TabPage)ctrl);
            }
            else if (ctrl is TreeView)
            {
                ApplyStyleTreeview((TreeView)ctrl);
            }
            else if (ctrl is Label)
            {
                ApplyStyleLabel((Label)ctrl);
            }
            else if (ctrl is StatusStrip)
            {
                ApplyStyleStatusStrip((StatusStrip)ctrl);
            }
            else if (ctrl is CurveControl)
            {
                ApplyStyleCurveControl((CurveControl)ctrl);
            }

            if (ctrl is TableLayoutPanel && ctrlParent is ActionEventLineControl)
            {
                ApplyStyleActionEventLine((TableLayoutPanel)ctrl);
            }

            ApplyStyleSpecific(ctrl);
        }

        private static void ApplyStyleSpecific(Control ctrl)
        {
            if (ctrl.Tag != null)
            {
                string[] tags = ctrl.Tag.ToString().Split(new String[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (String tag in tags)
	            {
                    if (tag == "B")
                        ctrl.BackColor = BorderColor;
                    if (tag == "BG1")
                        ctrl.BackColor = BackColorDark;
                    if (tag == "BG2")
                        ctrl.BackColor = BackColorLight;

                    if (tag == "F1")
                        ctrl.ForeColor = ForeColor1;
                    if (tag == "F2")
                        ctrl.ForeColor = ForeColor2;
	            }
            }
        }

        private static void ApplyStylePropertyGrid(PropertyGridLocal ctrl)
        {
            if (!String.IsNullOrEmpty(ctrl.TagLineColor))
            {
                if (ctrl.TagLineColor == "B")
                    ctrl.PropertyGrid.LineColor = BorderColor;
                if (ctrl.TagLineColor == "BG1")
                    ctrl.PropertyGrid.LineColor = BackColorDark;
                if (ctrl.TagLineColor == "BG2")
                    ctrl.PropertyGrid.LineColor = BackColorLight;
            }
        }

        private static void ApplyStyleControl(Control ctrl)
        {
            ctrl.Margin = new Padding(1);
            ctrl.BackColor = BackColorLight;
            ctrl.ForeColor = ForeColor1;
        }

        private static void ApplyStyleButton(ButtonBase button)
        {
            button.Margin = new Padding(0);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 2;

            button.BackColor = BackColorLight;

            Color borderColorLight = BackColorLight;
            int offset = 7;
            borderColorLight = Color.FromArgb(Math.Min(BackColorLight.R + offset, 255), Math.Min(BackColorLight.G + offset, 255), Math.Min(BackColorLight.B + offset, 255));

            button.FlatAppearance.BorderColor = borderColorLight;

            button.FlatAppearance.CheckedBackColor = SelectedColor;
            button.FlatAppearance.MouseOverBackColor = MouseOverColor;
        }

        private static void ApplyStylePanel(Panel panel)
        {
            panel.Margin = new Padding(0);
            panel.BackColor = BackColorDark;
        }

        private static void ApplyStyleTextbox(TextBoxBase txt)
        {
            txt.Margin = new Padding(1);
            txt.BackColor = BackColorLight;
            txt.ForeColor = ForeColor1;
        }

        private static void ApplyStyleList(ListControl list)
        {
            if (list is ComboBox)
            {
                ((ComboBox)list).FlatStyle = FlatStyle.Standard;
            }

            list.Margin = new Padding(1);
            list.BackColor = BackColorLight;
            list.ForeColor = ForeColor1;
        }

        private static void ApplyStyleActionEventLine(TableLayoutPanel panel)
        {
            panel.Margin = new Padding(1);
            panel.BackColor = BackColorLight;
            panel.ForeColor = ForeColor1;
        }

        private static void ApplyStyleTabPage(TabPage tabPage)
        {
            tabPage.Margin = new Padding(0);
            tabPage.BackColor = BackColorLight;
            tabPage.ForeColor = ForeColor1;
            tabPage.BorderStyle = BorderStyle.None;
        }

        private static void ApplyStyleTreeview(TreeView treeView)
        {
            treeView.Margin = new Padding(0);
            treeView.BackColor = BackColorLight;
            treeView.ForeColor = ForeColor1;
            treeView.BorderStyle = BorderStyle.None;
            treeView.FullRowSelect = true;
            treeView.ShowLines = false;
            treeView.HideSelection = false;
            treeView.LineColor = SelectedColor;
        }

        private static void ApplyStyleLabel(Label lbl)
        {
            lbl.BackColor = BackColorLight;
            lbl.ForeColor = ForeColor1;

            if (lbl.Parent != null)
                lbl.BackColor = lbl.Parent.BackColor;
        }

        private static void ApplyStyleStatusStrip(StatusStrip statusStrip)
        {
            statusStrip.BackColor = BackColorDark;
            statusStrip.ForeColor = ForeColor1;
        }

        private static void ApplyStyleCurveControl(CurveControl ctrl)
        {
            Color slectingBoxcolor = Color.FromArgb(50, MouseOverColor);

            ctrl.SelectingBoxColor = slectingBoxcolor;
            ctrl.GridTextColor = ForeColor1;
            ctrl.GridLineColor = BorderColor;
            ctrl.GridBoldLineColor = BackColorLight;
            ctrl.GridBackColor = BackColorDark;
        }

        private static bool OpenVisualStyle(string visualStyleName)
        {
            //===========================================================================================//
            //NOTE : Utilisation du site http://colorschemedesigner.com/ pour la génération des styles   //
            //===========================================================================================//

            string pathVisualStyleFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"VisualStyle\" + visualStyleName + ".xml");

            if (!File.Exists(pathVisualStyleFile))
            {
                MessageBox.Show(String.Format("Le fichier '{0}' n'existe pas.", pathVisualStyleFile), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            XPathDocument doc = new XPathDocument(pathVisualStyleFile);
            XPathNavigator xPath = doc.CreateNavigator();

            BackColorLight = ReadColor(xPath, "palette/colorset[@id='primary']/color[@id='primary-1']");
            BorderColor = ReadColor(xPath, "palette/colorset[@id='primary']/color[@id='primary-2']");
            BackColorDark = ReadColor(xPath, "palette/colorset[@id='primary']/color[@id='primary-3']");

            MouseOverColor = ReadColor(xPath, "palette/colorset[@id='primary']/color[@id='primary-4']");

            ForeColor1 = ReadColor(xPath, "palette/colorset[@id='complement']/color[@id='complement-2']");
            ForeColor2 = ReadColor(xPath, "palette/colorset[@id='complement']/color[@id='complement-3']");

            SelectedColor = ReadColor(xPath, "palette/colorset[@id='complement']/color[@id='complement-4']");

            BrushBackColorDark = new SolidBrush(BackColorDark);
            BrushBackColorLight = new SolidBrush(BackColorLight);
            BrushSelectedColor = new SolidBrush(SelectedColor);
            BrushMouseOverColor = new SolidBrush(MouseOverColor);
            BrushForeColor1 = new SolidBrush(ForeColor1);

            return true;
        }

        private static Color ReadColor(XPathNavigator xPath, string request)
        {
            Color color = Color.White;

            XPathNavigator navigator = xPath.SelectSingleNode(request);

            byte r = byte.Parse(navigator.GetAttribute("r", ""));
            byte g = byte.Parse(navigator.GetAttribute("g", ""));
            byte b = byte.Parse(navigator.GetAttribute("b", ""));

            color = Color.FromArgb(r, g, b);

            return color;
        }
    }
}
