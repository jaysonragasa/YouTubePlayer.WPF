using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System.Windows;

namespace YouTubePlayer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppCenter.Start("3d71dfb3-e594-486d-8d6f-b7663b4e004d",
                   typeof(Analytics), typeof(Crashes));
        }
    }
}
