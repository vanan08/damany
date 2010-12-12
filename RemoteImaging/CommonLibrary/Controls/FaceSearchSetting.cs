using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Util;

namespace RemoteImaging.Controls
{
    public partial class CameraSetting : UserControl
    {
        public CameraSetting()
        {
            InitializeComponent();
        }

        public float LeftExtRatio
        {
            get
            {
                return (float) this.leftExtRatio.EditValue;
            }
            set
            {
                this.leftExtRatio.EditValue = value;
            }
        }

        public float RightExtRatio
        {
            get
            {
                return (float) this.rightExtRatio.EditValue;
            }
            set
            {
                this.rightExtRatio.EditValue = value;
            }
        }

        public float TopExtRatio
        {
            get
            {
                return (float)(this.topExtRatio.EditValue);
            }
            set
            {
                this.topExtRatio.EditValue = value;
            }
        }

        public float BottomExtRatio
        {
            get
            {
                return (float) this.bottomExtRatio.EditValue;
            }
            set
            {
                this.bottomExtRatio.EditValue = value;
            }
        }

        public int MinFaceWidth
        {
            get
            {
                return (int)(this.minFaceWidth.EditValue);
            }
            set
            {
                this.minFaceWidth.EditValue = value;
            }
        }

        public int MaxFaceWidth
        {
            get
            {
                return (int)(this.maxFaceWidth.EditValue);
            }
            set
            {
                this.maxFaceWidth.EditValue = value;
            }
        }

        public bool EnableDetectMotion
        {
            get
            {
                return this.enableMotionDetect.Checked;
            }
            set
            {
                this.enableMotionDetect.Checked = value;
            }
        }

        public bool DrawMotionRegion
        {
            get
            {
                return this.drawMotionRect.Checked;
            }
            set
            {
                this.drawMotionRect.Checked = value;
            }
        }

        public decimal MotionRegionAreaLimit
        {
            get
            {
                return (decimal) this.motionRegionAreaLimit.EditValue;
            }
            set
            {
                this.motionRegionAreaLimit.EditValue = value;
            }
        }

        public decimal ImageGroupLength
        {
            get
            {
                return (decimal) this.imageGroupLength.EditValue;
            }
            set
            {
                this.imageGroupLength.EditValue = value;
            }
        }

        private void EnableMotionDetectControlGroupe(bool enabled)
        {
            this.drawMotionRect.Enabled = enabled;
            this.motionRegionAreaLimit.Enabled = enabled;
            this.imageGroupLength.Enabled = enabled;
        }


        private void enableMotionDetecto_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = this.enableMotionDetect.Checked;

            EnableMotionDetectControlGroupe(enabled);
        }

        private void motionRegionAreaLimit_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
