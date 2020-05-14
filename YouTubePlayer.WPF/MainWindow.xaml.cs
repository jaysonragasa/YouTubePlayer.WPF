using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;

namespace YouTubePlayer.WPF
{
    public partial class MainWindow : Window
    {
        #region vars
        DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Normal)
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        CefSharp.Wpf.ChromiumWebBrowser _browser;

        IntPtr _hWnd = IntPtr.Zero;
        #endregion

        #region ctor

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }
        #endregion

        #region events

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // get current window handle;
            {
                _hWnd = new WindowInteropHelper(this).Handle;
            }

            // create chrome browser.
            // need to do this programatically because it's 
            // bugging up when directly added in XAML
            {
                _browser = new CefSharp.Wpf.ChromiumWebBrowser();
                this.gridContent.Children.Add(_browser);
                _browser.FrameLoadStart += (s, fl) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.tbYTID.IsEnabled = false;
                    });
                };
                _browser.FrameLoadEnd += (s, fl) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.tbYTID.IsEnabled = true;
                    });
                };
            }

            // init events
            {
                gridTitleBar.MouseDown += GridTitleBar_MouseDown;
                gridTitleBar.MouseUp += GridTitleBar_MouseUp;
                gridParentContent.SizeChanged += GridParentContent_SizeChanged;
                btnLoad.Click += BtnLoad_Click;

                // force window on top.
                _timer.Tick += (s, te) =>
                {
                    ForceOnTop();
                };
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (this._browser != null)
            {
                _browser.Address = $"https://www.youtube-nocookie.com/embed/{this.tbYTID.Text}";
            }
        }

        private void GridParentContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 330 || e.NewSize.Height < 200)
            {
                gridTitleBar.Visibility = Visibility.Collapsed;
            }
            else
            {
                gridTitleBar.Visibility = Visibility.Visible;
            }
        }

        private void GridTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this._timer.Stop();

                this.DragMove();
            }
        }

        private void GridTitleBar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this._timer.Start();

            this.ReleaseMouseCapture();
        }
        #endregion

        #region methods

        void ForceOnTop()
        {
            Win32API.SetWindowOnTop(this._hWnd);
        }
        #endregion
    }
}