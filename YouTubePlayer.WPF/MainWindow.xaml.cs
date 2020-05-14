using System.Windows;
using System.Windows.Input;

namespace YouTubePlayer.WPF
{
    public partial class MainWindow : Window
    {
        CefSharp.Wpf.ChromiumWebBrowser _browser;

        public MainWindow()
        {
            InitializeComponent();


            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //webBrowser.do
            ////webBrowser.NavigateToString("<div style=\"position: fixed; z-index: -99; width: 100%; height: 100%\"><iframe width=\"100%\" height=\"100%\" src=\"https://www.youtube.com/embed/dLPeh8ew74Y\" frameborder=\"0\" allow=\"accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe></div>");
            //webBrowser.Source = new Uri("https://www.youtube-nocookie.com/embed/dJOHzKyxmn8", UriKind.Absolute);

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

            gridTitleBar.MouseDown += GridTitleBar_MouseDown;
            gridParentContent.SizeChanged += GridParentContent_SizeChanged;
            btnLoad.Click += BtnLoad_Click;
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            if(this._browser != null)
            {
                _browser.Address = $"https://www.youtube-nocookie.com/embed/{this.tbYTID.Text}";       
            }
        }

        private void GridParentContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(e.NewSize.Width < 330 || e.NewSize.Height < 200)
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
                this.DragMove();
            }
            else
            {
                this.ReleaseMouseCapture();
            }
        }
    }
}
