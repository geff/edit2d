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
    public partial class UserControlLocal : UserControl
    {
        public UserControlLocal()
        {
            InitializeComponent();
        }

        protected void RefreshGlobalTreeView(bool keepCheckedNodes)
        {
            ((FrmEdit2D)this.ParentForm).treeView.RefreshView(keepCheckedNodes);
        }

        protected void RefreshGlobalTreeView()
        {
            ((FrmEdit2D)this.ParentForm).treeView.RefreshView();
        }

        protected void RefreshGlobalTreeView<T>(T nodeToCheck)
        {
            ((FrmEdit2D)this.ParentForm).treeView.RefreshView<T>(nodeToCheck);
        }

        protected void CheckNodeGlobalTreeView<T>(T nodeToCheck)
        {
            ((FrmEdit2D)this.ParentForm).treeView.CheckNode<T>(nodeToCheck);
        }
    }
}
