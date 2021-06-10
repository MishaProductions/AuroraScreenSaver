using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuroraScreenSaver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Debugger.IsAttached)
            {
                RunScreensaver();
                return;
            }
            if (args.Length > 0)
            {
                switch (args[0].ToLower().Trim().Substring(0, 2))
                {
                    // Preview the screen saver
                    case "/p":
                        // args[1] is the handle to the preview window
                        RunScreensaver(new IntPtr(long.Parse(args[1])));
                        break;

                    // Show the screen saver
                    case "/s":
                        RunScreensaver();
                        break;

                    // Configure the screesaver's settings
                    case "/c":
                        // Show the settings form
                        MessageBox.Show("This screen saver has no options that you can set.", "Aurora Screen Saver", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    // Show the screen saver
                    default:
                        RunScreensaver();
                        break;
                }
            }
        }

        private static void RunScreensaver()
        {
            var ar = Screen.AllScreens;
            for (int i = 0; i < ar.Length; i++)
            {
                var item = ar[i];
                var rectMonitor = item.Bounds;
                var frm = new Form1(item.Bounds);
                frm.Show();
                SetWindowPos(frm.Handle, IntPtr.Zero,
                     rectMonitor.Left, rectMonitor.Top, rectMonitor.Width,
                     rectMonitor.Height, 0x0040);
            }
            Application.Run();
        }
        private static void RunScreensaver(IntPtr handle)
        {
            Application.Run(new Form1(handle));
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    }
}
