using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuroraScreenSaver
{
    public partial class Form1 : Form
    {
        bool IsPreview = false;
        public Form1(Rectangle bounds)
        {
            InitializeComponent();
            Cursor.Hide();
            oldX = Cursor.Position.X;
            oldY = Cursor.Position.Y;
            //this.Bounds = bounds;
        }
        public Form1(IntPtr previewHandle)
        {
            InitializeComponent();
            oldX = Cursor.Position.X;
            oldY = Cursor.Position.Y;


            // Set the preview window of the screen saver selection 
            // dialog in Windows as the parent of this form.
            SetParent(this.Handle, previewHandle);

            // Set this form to a child form, so that when the screen saver selection 
            // dialog in Windows is closed, this form will also close.
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

            // Set the size of the screen saver to the size of the screen saver 
            // preview window in the screen saver selection dialog in Windows.
            Rectangle ParentRect;
            GetClientRect(previewHandle, out ParentRect);
            this.Size = ParentRect.Size;

            this.Location = new Point(0, 0);
            IsPreview = true;
            timer1.Stop();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
        int oldX = 0;
        int oldY = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsPreview)
                return;
            if (oldX != Cursor.Position.X | oldY != Cursor.Position.Y)
            {
                Application.Exit();
            }
            oldX = Cursor.Position.X;
            oldY = Cursor.Position.Y;
        }
        #region dllimports
        // Changes the parent window of the specified child window
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        // Changes an attribute of the specified window
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        // Retrieves information about the specified window
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        // Retrieves the coordinates of a window's client area
        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);
        #endregion
    }
}
