using System;
using System.Collections.Generic;

namespace Kise.IdCard.UI
{
    using Model;

    public partial class IdCardControl : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IDictionary<int, string> _minorityDictionary;
        private IdCardInfo _idCardInfo;
        public IdCardInfo IdCardInfo
        {
            get { return _idCardInfo; }
            set
            {
                if (value != null)
                {
                    _idCardInfo = value;

                    this.name.Text = _idCardInfo.Name;
                    this.sex.Text = Model.Helper.GetSexName(_idCardInfo.SexCode);
                    this.minority.Text = MinorityDictionary[_idCardInfo.MinorityCode];
                    this.birthDay.Text = string.Format(BirthDayFormat, _idCardInfo.BornDate.Year, _idCardInfo.BornDate.Month, _idCardInfo.BornDate.Day);
                    this.address.Text = _idCardInfo.Address;
                    this.issuedBy.Text = _idCardInfo.GrantDept;
                    this.expiry.Text = FormatDate(_idCardInfo.ValidateFrom) + " — " + FormatDate(_idCardInfo.ValidateUntil);
                    this.idCardNo.Text = _idCardInfo.IdCardNo;

                    this.image.Image = AForge.Imaging.Image.FromFile(_idCardInfo.PhotoFilePath);

                }

            }
        }

        public string BirthDayFormat { get; set; }

        public IDictionary<int, string> MinorityDictionary { get; set; }

        public IdCardControl()
        {
            InitializeComponent();

            BirthDayFormat = "{0} 年 {1} 月 {2} 日";
        }

        public IdCardControl(IDictionary<int, string> minorityDictionary)
        {
            if (minorityDictionary == null) throw new ArgumentNullException("minorityDictionary");
            _minorityDictionary = minorityDictionary;
        }

        private static string FormatDate(System.DateTime dt)
        {
            return string.Format("{0}.{1}.{2}", dt.Year, dt.Month, dt.Day);
        }
    }
}
