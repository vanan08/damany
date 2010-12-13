using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Query
{
    public class ImageDirSys
    {
        public static string BeginDir = Properties.Settings.Default.OutputPath;
        public static string BigIconPath = Properties.Settings.Default.BigImageDirectoryName;
        public static string IconPath = Properties.Settings.Default.IconDirectoryName;
        public static string VideoPath = Properties.Settings.Default.VideoDirectoryName;
        public enum SearchType { PicType, VideoType };

        public ImageDirSys(string camera, DateTimeInString dtString)
        {
            this.CameraID = camera;
            this.Year = dtString.year;
            this.Month = dtString.month;
            this.Day = dtString.day;
            this.Hour = dtString.hour;
            this.Minute = dtString.minute;
            this.Second = dtString.second;
        }

        public string CameraID
        {
            get;
            set;
        }
        public string Year
        {
            get;
            set;
        }
        public string Month
        {
            get;
            set;
        }
        public string Day
        {
            get;
            set;
        }
        public string Hour
        {
            get;
            set;
        }
        public string Minute
        {
            get;
            set;
        }
        public string Second
        {
            get;
            set;
        }

    }
}
