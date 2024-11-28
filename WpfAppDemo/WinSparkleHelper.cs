using System;
using System.Runtime.InteropServices;

namespace MyWpfApp
{
    public static class WinSparkleHelper
    {
        // 初始化 WinSparkle
        [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_init();

        // 啟動手動檢查更新
        [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_check_update_with_ui();

        // 設定應用程式的版本
        [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_set_app_version(string version);

        // 設定更新檢查的 XML URL
        [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_set_appcast_url(string url);

        // 清理資源
        [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_cleanup();
    }
}