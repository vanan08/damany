using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Extensions
{
    public static class MiscExtensions
    {
        public static void PlayFile(this AxAXVLC.AxVLCPlugin2 player, string videoFilePath)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (videoFilePath == null) throw new ArgumentNullException("videoFilePath");

            if (player.playlist.isPlaying)
            {
                player.playlist.stop();
            }
            player.playlist.items.clear();


            int idx = player.playlist.add(videoFilePath, null, null);
            player.playlist.playItem(idx);

        }

        public static void StopPlaying(this AxAXVLC.AxVLCPlugin2 player)
        {
            if (player.playlist.isPlaying)
            {
                player.playlist.stop();
                System.Threading.Thread.Sleep(1000);
            }
        }

    }
}
