using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Kise.IdCard.UI;

namespace KiseTech.IdCard.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();

            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("McSkin");


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
