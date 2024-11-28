using AutoUpdaterDotNET;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeWinSparkle();
        }

        private void InitializeWinSparkle()
        {
            // 設定 Appcast URL（更新的 XML 文件）
            WinSparkleHelper.win_sparkle_set_appcast_url("http://localhost:8000/Downloads/appcast.xml");

            // 設定應用程式的版本號
            WinSparkleHelper.win_sparkle_set_app_details("aircast", "WpfAppDemo", "1.1.0");

            // 初始化 WinSparkle
            WinSparkleHelper.win_sparkle_init();
        }

        private void EdgeButton_Click(object sender, RoutedEventArgs e)
        {
            OpenBrowser("msedge.exe", "https://www.google.com");
        }

        private void ChromeButton_Click(object sender, RoutedEventArgs e)
        {
            OpenBrowser("chrome.exe", "https://www.google.com");
        }

        // Helper method to open a browser
        private void OpenBrowser(string browserPath, string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = browserPath,
                    Arguments = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open browser: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AutoUpdater_CheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.Error != null)
            {
                MessageBox.Show($"無法檢查更新：{args.Error.Message}", "錯誤", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (args.IsUpdateAvailable)
            {
                var dialogResult = MessageBox.Show(
                    $"新版本 {args.CurrentVersion} 可用！\n" +
                    $"您目前的版本：{args.InstalledVersion}\n\n" +
                    "是否立即更新？",
                    "更新提示",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        AutoUpdater.DownloadUpdate(args);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"更新失敗：{ex.Message}", "錯誤", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("目前已是最新版本！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //winsparkle
            WinSparkleHelper.win_sparkle_check_update_with_ui();
        }

        private void checkupdate_Click(object sender, RoutedEventArgs e)
        {
            // 初始化 AutoUpdater 並設定更新檔路徑
            AutoUpdater.Start("http://localhost:8000/Downloads/AutoUpdater.xml");

            // 可選：設定檢查更新事件
            AutoUpdater.CheckForUpdateEvent += AutoUpdater_CheckForUpdateEvent;
        }

    }
}