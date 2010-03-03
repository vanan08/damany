using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Forms;

namespace RemoteImaging
{
    public static class VideoPlayer
    {
        private static string videoPlayerPath;

        static VideoPlayer()
        {
            try
            {
                videoPlayerPath = (string)Registry.LocalMachine.OpenSubKey("Software")
                                .OpenSubKey("Videolan")
                                .OpenSubKey("vlc").GetValue(null);
            }
            catch (Exception)
            {
                videoPlayerPath = null;
            }

        }


        public static string ExePath { get { return videoPlayerPath; } }


        public static void PlayVideosAsync(string[] videos)
        {
            if (videoPlayerPath == null)
            {
                MessageBox.Show("请安装VLC播放器", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            StringBuilder sb = new StringBuilder();
            foreach (var file in videos)
            {
                sb.Append(file); sb.Append(' ');
            }

            sb.Append(@"vlc://quit"); sb.Append(' ');

            Process.Start(videoPlayerPath, sb.ToString());
        }

    }
}
