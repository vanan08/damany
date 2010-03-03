using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Component;

namespace Damany.Windows.Form
{
    using NameObjectPair = KeyValuePair<string, object>;
    using NameObjectCollection = System.Collections.Generic.List<KeyValuePair<string,object>>;

    public partial class SanyoNetCamera : UserControl
    {
        public SanyoNetCamera()
        {
            InitializeComponent();
            this.InitControls();
        }

        public event EventHandler ApplyAgcClick
        {
            add
            {
                this.applyAgc.Click += value;
            }
            remove
            {
                this.applyAgc.Click -= value;
            }
        }


        public event EventHandler ApplyIrisClick
        {
            add
            {
                this.applyIris.Click += value;
            }
            remove
            {
                this.applyIris.Click -= value;
            }
        }

        public event EventHandler ApplyShutterClick
        {
            add
            {
                this.applyShutter.Click += value;
            }
            remove
            {
                this.applyShutter.Click -= value;
            }
        }


        public Damany.Component.IrisMode IrisMode
        {
            get
            {
                return this.autoIris.Checked ? IrisMode.Auto : IrisMode.Manual;
            }
            set
            {
                switch (value)
                {
                    case IrisMode.Auto:
                        this.autoIris.Checked = true;
                        break;
                    case IrisMode.Manual:
                        this.manualIris.Checked = true;
                        break;
                    default:
                        throw new InvalidEnumArgumentException("IrisMode");
#pragma warning disable 0162
                        break;
#pragma warning restore 0162
                }
            }
        }

        public ShutterMode ShutterMode
        {
            get
            {
                return this.shutterOff.Checked ? ShutterMode.Off : ShutterMode.Short;
            }
            set
            {
                switch (value)
                {
                    case ShutterMode.Long:
                        break;
                    case ShutterMode.Off:
                        this.shutterOff.Checked = true;
                        break;
                    case ShutterMode.Short:
                        this.shutterShort.Checked = true;
                        break;
                    default:
                        throw new InvalidEnumArgumentException("ShutterMode");
#pragma warning disable 0162
                        break;
#pragma warning restore 0162

                }
            }
        }

        public int ShutterLevel
        {
            get
            {
                return (int) this.shutterLevel.SelectedValue;
            }
            set
            {
                this.shutterLevel.SelectedValue = value;
            }
        }

        public int IrisLevel
        {
            get
            {
                return  (int) this.manualIrisLevel.SelectedItem;
            }
            set
            {
                this.manualIrisLevel.SelectedItem = value;
            }
        }

        public bool AgcEnabled
        {
            get
            {
                return this.agcOn.Checked;
            }
            set
            {
                this.agcOn.Checked = value;
            }
        }

        public bool DigitalGainEnabled
        {
            get
            {
                return this.digitalGain.SelectedItem.Equals("开");
            }
            set
            {
                this.digitalGain.SelectedItem = value ? "开" : "关";
            }
        }


        private void PopulateComboBox(List<KeyValuePair<string, object>> items, ComboBox comboBox)
        {
            comboBox.DataSource = items;
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
        }

        private void PopulateShutterLevels()
        {
            var shutterLevels = new NameObjectCollection();

            shutterLevels.Add(new NameObjectPair("25", 0));
            shutterLevels.Add(new NameObjectPair("50", 1));
            shutterLevels.Add(new NameObjectPair("120", 2));
            shutterLevels.Add(new NameObjectPair("250", 3));
            shutterLevels.Add(new NameObjectPair("500", 4));
            shutterLevels.Add(new NameObjectPair("1000", 5));
            shutterLevels.Add(new NameObjectPair("2000", 6));
            shutterLevels.Add(new NameObjectPair("4000", 7));
            shutterLevels.Add(new NameObjectPair("10000", 8));

            PopulateComboBox(shutterLevels, this.shutterLevel);
            this.shutterLevel.SelectedIndex = 0;
        }

        private void PopulateIrisLevels()
        {
            for (int i = 0; i < 101; ++i)
            {
                this.manualIrisLevel.Items.Add(i);
            }
            
            this.manualIrisLevel.SelectedIndex = 0;
        }

        private void PopulateDigitalGains()
        {
            var digitalGains = new NameObjectCollection();
            digitalGains.Add(new NameObjectPair("关", 0));
            digitalGains.Add(new NameObjectPair("开", 1));

            this.PopulateComboBox(digitalGains, this.digitalGain);
        }

        private void InitControls()
        {
            this.shutterShort.Checked = true;
            this.shutterShort.CheckedChanged += new EventHandler(shutterShort_CheckedChanged);
            PopulateShutterLevels();

            this.manualIris.Checked = true;
            this.manualIris.CheckedChanged += new EventHandler(manualIris_CheckedChanged);
            PopulateIrisLevels();

            this.agcOn.Checked = true;
            this.agcOn.CheckedChanged += new EventHandler(agcOn_CheckedChanged);
            PopulateDigitalGains();
        }

        void agcOn_CheckedChanged(object sender, EventArgs e)
        {
            this.digitalGain.Enabled = this.agcOn.Checked;
        }

        void manualIris_CheckedChanged(object sender, EventArgs e)
        {
            this.manualIrisLevel.Enabled = this.manualIris.Checked;
        }

        void shutterShort_CheckedChanged(object sender, EventArgs e)
        {
            this.shutterLevel.Enabled = this.shutterShort.Checked;
        }
    }
}
