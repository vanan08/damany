using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.PC.Domain;

namespace Damany.RemoteImaging.Common.Forms
{
    public partial class EditCamera : Form
    {
        public EditCamera()
        {
            InitializeComponent();
        }

        public int CameraId
        {
            get
            {
                return int.Parse(this.Id.Text);
            }
        }

        public string Url
        {
            get
            {
                return this.ipAddress.Text;
            }
        }

        public CameraProvider CameraType
        {
            get
            {
                return (CameraProvider) this.cameraType.SelectedValue;
            }
        }

        private void EditCamera_Load(object sender, EventArgs e)
        {
            var query = from camType in Enum.GetValues(typeof(CameraProvider)).Cast<Enum>()
                        select new { Name = camType.ToString(), Value = camType };

            this.cameraType.DataSource = query.ToList();
            this.cameraType.DisplayMember = "Name";
            this.cameraType.ValueMember = "Value";

        }
    }
}
