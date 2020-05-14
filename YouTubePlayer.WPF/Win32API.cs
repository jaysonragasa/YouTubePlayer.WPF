using System;
using System.Runtime.InteropServices;

namespace YouTubePlayer.WPF
{
    public class Win32API
    {
        const int HWND_TOPMOST = -1;
        const int HWND_NOTOPMOST = -2;
        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOSIZE = 0x0001;
        const int TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        public static void SetWindowOnTop(IntPtr hWnd)
        {
            Win32API.SetWindowPos(
                    //Process.GetCurrentProcess().Handle,
                    hWnd,
                    (IntPtr)Win32API.HWND_TOPMOST,
                    //(int)this.Top, (int)this.Left, (int)(this.Left + this.Width), (int)(this.Top + this.Height),
                    0, 0, 0, 0,
                    Win32API.TOPMOST_FLAGS
                    );
        }
    }
}