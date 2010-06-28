using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RemoteImaging.LicensePlate
{
    public class LicenseParsingConfig : ConfigurationSection
    {
        private const string IncludeSubdirectoriesName = "IncludeSubdirectories";
        private const string LeastSectionsCountName = "LeastSectionCount";
        private const string LicensePlateSectionIndexName = "LicensePlateSectionIndex";
        private const string TimeSectionIndexName = "TimeSectionIndex";
        private const string SeparateCharName = "SeparateChar";
        private const string FilterName = "Filter";
        private const string ScanIntervalName = "ScanInterval";


        [ConfigurationProperty(IncludeSubdirectoriesName, DefaultValue=true)]
        public bool IncludeSubdirectories
        {
            get
            {
                return (bool) this[IncludeSubdirectoriesName];
            }
            set
            {
                this[IncludeSubdirectoriesName] = value;
                
            }
        }


        [ConfigurationProperty(LeastSectionsCountName, DefaultValue=2)]
        public int LeastSectionCount
        {
            get
            {
                return (int) this[LeastSectionsCountName];
            }
            set
            {
                this[LeastSectionsCountName] = value;
            }
        }


        [ConfigurationProperty(TimeSectionIndexName, DefaultValue=0)]
        public int TimeSectionIndex
        {
            get
            {
                return (int) this[TimeSectionIndexName];
            }
            set
            {
                this[TimeSectionIndexName] = value;
            }
        }



        [ConfigurationProperty(LicensePlateSectionIndexName, DefaultValue=1)]
        public int LicensePlateSectionIndex
        {
            get
            {
                return (int) this[LicensePlateSectionIndexName];
            }
            set
            {
                this[LicensePlateSectionIndexName] = value;
            }
        }


        [ConfigurationProperty(SeparateCharName, DefaultValue='-')]
        public char SeparateChar
        {
            get
            {
                return (char) this[SeparateCharName];
            }
            set
            {
                this[SeparateCharName] = value;
            }
        }

        [ConfigurationProperty(FilterName, DefaultValue="*.jpg")]
        public string Filter
        {
            get
            {
                return (string) this[FilterName];
            }
            set
            {
                this[FilterName] = value;
            }
        }

        [ConfigurationProperty(ScanIntervalName, DefaultValue=30)]
        public int ScanInterval
        {
            get
            {
                return (int) this[ScanIntervalName];
            }
            set
            {
                this[ScanIntervalName] = value;
            }
        }
    }
}
