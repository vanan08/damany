using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.PC.Domain;

namespace Damany.RemoteImaging.Common.Controls
{
    public partial class CameraConfigurer : UserControl
    {
        public CameraConfigurer()
        {
            InitializeComponent();
        }


        public IList<CameraInfo> Cameras
        {
            set
            {
                this.camerasList.Items.Clear();
                foreach (var cam in value)
                {
                    var item = new System.Windows.Forms.ListViewItem(new string[] {
                                            cam.Id.ToString(),
                                            cam.Location.ToString(),
                                            cam.Provider.ToString(),
                                            cam.Description}, -1);

                    item.Tag = cam;
                    this.camerasList.Items.Add(item);
                }
            }
        }




       


    }
}
