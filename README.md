![https://raw.githubusercontent.com/jaysonragasa/YouTubePlayer.WPF/master/ss/temp05142020-193659.gif](https://raw.githubusercontent.com/jaysonragasa/YouTubePlayer.WPF/master/ss/temp05142020-193659.gif)
# YouTube Floating Player
This is a very simple YouTube player stand-alone app that stays on top on every windows on your desktop and especially on taskbar. Well, at least it try to since the windows taskbar is, by default designed to be on top.  
  
Back when I was still developing Windows Forms. Win32API is used everytime to all my projects just trying to manipulate how the standard controls work. My favorite website back then is [PINVOKE.NET](https://www.pinvoke.net/default.aspx/user32.setwindowpos)
  
This app uses `SetWindowspos` API 
```csharp
[DllImport("user32.dll", SetLastError = true)]
static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
```
to set the current window on top by setting `-1` parameter along with other two flags `0x0002` and `0x0001`  
  
Here's a ready to use class
```csharp
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
```
  
WPF has `Window.Topmost` property that does the same but it has a limit.
  
The taskbar on windows has the same window placement and flag so when you move the floating player on top of the taskbar and you clicked on the taskbar, the player goes behind. There are no Win32API AFAIK that forces the window to ALWAYS be on top unless we put some timer to re-set the window z-index/placement which we did the same here in the floating player and this is where the `Window.Topmost` is not capable of doing so, that why we need that Win32API.  
  
This is written with WPF and using CefSharp component for web browser control and it does not need any support to any browsers just to run the app.
<Br/>
<Br/>
![https://github.com/cefsharp/CefSharp/raw/master/logo.png](https://github.com/cefsharp/CefSharp/raw/master/logo.png)  
[CefSharp](https://github.com/cefsharp/CefSharp) lets you embed Chromium in .NET apps. It is a lightweight .NET wrapper around the Chromium Embedded Framework (CEF) by Marshall A. Greenblatt. About 30% of the bindings are written in C++/CLI with the majority of code here is C#. It can be used from C# or VB, or any other CLR language. CefSharp provides both WPF and WinForms web browser control implementations.