namespace Kise.IdCard.UI
{
    using Model;

    public partial class IdCardControl : DevExpress.XtraEditors.XtraUserControl
    {
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
                    this.sex.Text = _idCardInfo.Sex.ToString();
                    this.minority.Text = _idCardInfo.Minority.ToString();
                    this.birthDay.Text = _idCardInfo.BornDate.ToString();
                    this.address.Text = _idCardInfo.Address;
                    this.issuedBy.Text = _idCardInfo.GrantDept;
                    this.expiry.Text = _idCardInfo.ValidateFrom.ToString() + "——" + _idCardInfo.ValidateUntil.ToString();
                    this.idCardNo.Text = _idCardInfo.IdCardNo;

                    this.image.Image = AForge.Imaging.Image.FromFile(_idCardInfo.PhotoFilePath);

                }

            }
        }

        public IdCardControl()
        {
            InitializeComponent();
        }
    }
}
