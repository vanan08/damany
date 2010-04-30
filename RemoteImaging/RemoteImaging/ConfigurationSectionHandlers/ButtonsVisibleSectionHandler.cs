using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RemoteImaging.ConfigurationSectionHandlers
{
    public class ButtonsVisibleSectionHandler : ConfigurationSection
    {
        private const string HumanFaceLibraryButton = "HumanFaceLibraryButtonVisible";
        private const string FaceCompareButton = "CompareFaceButtonVisible";
        private const string ShowAlermFormButton = "ShowAlermFormButtonVisible";


        [ConfigurationProperty(HumanFaceLibraryButton, DefaultValue = true)]
        public bool HumanFaceLibraryButtonVisible
        {
            get
            {
                return (bool) this[HumanFaceLibraryButton];
            }
            set
            {
                this[HumanFaceLibraryButton] = value;
            }
        }

        [ConfigurationProperty(FaceCompareButton, DefaultValue = true)]
        public bool CompareFaceButtonVisible
        {
            get
            {
                return (bool) this[FaceCompareButton];
            }
            set
            {
                this[FaceCompareButton] = value;
            }
        }

        [ConfigurationProperty(ShowAlermFormButton, DefaultValue = true)]
        public bool ShowAlermFormButtonVisible
        {
            get
            {
                return (bool)this[ShowAlermFormButton];
            }
            set
            {
                this[ShowAlermFormButton] = value;
            }
        }
    }
}
