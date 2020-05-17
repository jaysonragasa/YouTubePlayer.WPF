using System;
using System.Runtime.InteropServices;

namespace YouTubePlayer.WPF
{
    public class Win32API
    {
        /// <summary>
        /// Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
        /// </summary>
        const int HWND_TOPMOST = -1;

        /// <summary>
        /// Retains the current position (ignores X and Y parameters).
        /// </summary>
        const int SWP_NOMOVE = 0x0002;

        /// <summary>
        /// SWP_NOSIZE
        /// </summary>
        const int SWP_NOSIZE = 0x0001;

        const int TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        /// <summary>
        /// Changes the size, position, and Z order of a child, pop-up, or top-level window. 
        /// These windows are ordered according to their appearance on the screen. 
        /// The topmost window receives the highest rank and is the first window in the Z order.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        public static void SetWindowOnTop(IntPtr hWnd)
        {
            Win32API.SetWindowPos(
                    hWnd,
                    (IntPtr)Win32API.HWND_TOPMOST,
                    0, 0, 0, 0,
                    TOPMOST_FLAGS
                    );
        }
    }
}