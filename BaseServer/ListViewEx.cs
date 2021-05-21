using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseServer
{
    public class ListViewEx : System.Windows.Forms.ListView
    {
        private const int WM_HSCROLL = 0x0114;
        private const int WM_VSCROLL = 0x0115;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_MOUSEWHEEL = 0x020A;

        public event System.EventHandler ScrollEvent;

        [System.Security.Permissions.SecurityPermission
            (System.Security.Permissions.SecurityAction.LinkDemand, Flags =
             System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_VSCROLL ||
                m.Msg == WM_HSCROLL ||
                m.Msg == WM_KEYDOWN ||
                m.Msg == WM_MOUSEWHEEL)
                if (ScrollEvent != null)
                    ScrollEvent(this, null);
            base.WndProc(ref m);
        }
        public void SetDoubleBuffered(bool value)
        {
            this.DoubleBuffered = value;
        }
    }

    // Implements the manual sorting of items by columns.
    public class ListViewItemComparer : IComparer
    {
        private int col;
        public ListViewItemComparer()
        {
            col = 0;
        }
        public ListViewItemComparer(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            ListViewItem itemX = (ListViewItem)x;
            ListViewItem itemY = (ListViewItem)y;
            if (col < itemX.SubItems.Count && col < itemY.SubItems.Count)
                return String.Compare(itemX.SubItems[col].Text, itemY.SubItems[col].Text);
            else
                return 0;
        }
    }

    // Implements the manual reverse sorting of items by columns.
    public class ListViewItemReverseComparer : IComparer
    {
        private int col;
        public ListViewItemReverseComparer()
        {
            col = 0;
        }
        public ListViewItemReverseComparer(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            ListViewItem itemX = (ListViewItem)x;
            ListViewItem itemY = (ListViewItem)y;
            if (col < itemX.SubItems.Count && col < itemY.SubItems.Count)
                return String.Compare(itemY.SubItems[col].Text, itemX.SubItems[col].Text);
            else
                return 0;
        }
    }
    // Implements the manual sorting of items last column by date.
    public class ListViewItemDateComparer : IComparer
    {
        public ListViewItemDateComparer()
        {
        }
        public int Compare(object x, object y)
        {
            ListViewItem itemX = (ListViewItem)x;
            ListViewItem itemY = (ListViewItem)y;
            int colX = itemX.SubItems.Count - 1;
            int colY = itemY.SubItems.Count - 1;
            DateTime dt = DateTime.MinValue;
            if (itemX.SubItems[colX].Tag != null &&
                itemX.SubItems[colX].Tag.GetType() == dt.GetType() &&
                itemY.SubItems[colY].Tag != null &&
                itemY.SubItems[colY].Tag.GetType() == dt.GetType())
            {
                return DateTime.Compare((DateTime)itemX.SubItems[colX].Tag,
                    (DateTime)itemY.SubItems[colY].Tag);
            }
            else
                return 0;
        }
    }

    // Implements the manual sorting of items last column by date.
    public class ListViewItemReverseDateComparer : IComparer
    {
        public ListViewItemReverseDateComparer()
        {
        }
        public int Compare(object x, object y)
        {
            ListViewItem itemX = (ListViewItem)x;
            ListViewItem itemY = (ListViewItem)y;
            int colX = itemX.SubItems.Count - 1;
            int colY = itemY.SubItems.Count - 1;
            DateTime dt = DateTime.MinValue;
            if (itemX.SubItems[colX].Tag != null &&
                itemX.SubItems[colX].Tag.GetType() == dt.GetType() &&
                itemY.SubItems[colY].Tag != null &&
                itemY.SubItems[colY].Tag.GetType() == dt.GetType())
            {
                return DateTime.Compare((DateTime)itemY.SubItems[colY].Tag,
                    (DateTime)itemX.SubItems[colX].Tag);
            }
            else
                return 0;
        }
    }
}
