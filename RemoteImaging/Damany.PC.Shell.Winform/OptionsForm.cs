using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.RemoteImaging.Common;
using Damany.RemoteImaging.Common.Forms;
using Damany.PC.Domain;

namespace Damany.PC.Shell.Winform
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
            this.cameraConfigurer2.Add.Click += this.Add_Click;
            this.cameraConfigurer2.Delete.Click += new EventHandler(Delete_Click);
        }

        void Delete_Click(object sender, EventArgs e)
        {
            ListView camLists = this.cameraConfigurer2.camerasList;
            if (camLists.SelectedItems.Count == 0) return;

            foreach (ListViewItem item in camLists.SelectedItems)
            {
                var camInfo = (CameraInfo)item.Tag;
                this.Presenter.DeleteCamera(camInfo);
            }
        }


        public OptionPresenter Presenter { get; set; }

        private void OptionsForm_Shown(object sender, EventArgs e)
        {
            this.Presenter.Start();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            EditCamera form = new EditCamera();
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK) return;

            try
            {
                var cam = new CameraInfo();
                cam.Description = "";
                cam.Id = form.CameraId;
                cam.Location = new Uri(form.Url);
                cam.Provider = form.CameraType;

                this.Presenter.AddCamera(cam);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            	
            }
            

        }

    }
}
