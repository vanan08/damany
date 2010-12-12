using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Police
{
    public interface ITrafficEventInputScreenObserver
    {
        void LicensePlateNumberChanged();
        void SaveClicked();
        void SaveAndPrintClicked();
    }
}
