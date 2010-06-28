using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace E_Police
{
    public partial class Form1 : Form, ITrafficEventInputScreen
    {
        public Form1()
        {
            InitializeComponent();
            var i = 3;
        }

        #region ITrafficEventInputScreen Members

        public string EventTime
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string CapturedAt
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string EventDescription
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string LicensePlateCategory
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string VehicleCategory
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string LicensePlateNumber
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string VehicleOwnerName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string OwnerAddr
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string OwnerPhone
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Image EvidencePic
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.jThumbnailView1.FolderName = folderBrowserDialog1.SelectedPath;
            }
        }

        private void thumbnailView1_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void sToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void sToolStripMenuItem2_Click(object sender, EventArgs e)
        {
        }



        #region ITrafficEventInputScreen Members


        public string OwnerName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void AttachPresenter(ITrafficEventInputScreenObserver presenter)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void jThumbnailView1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        private void jThumbnailView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                string filePath = e.Item.Tag as string;
                if (!string.IsNullOrEmpty(filePath))
                {
                    this.mainPicture.Load(filePath);
                }
            }
        }
    }
}
