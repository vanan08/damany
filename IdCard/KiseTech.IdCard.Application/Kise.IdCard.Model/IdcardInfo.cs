using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Kise.IdCard.Model
{
    public class IdCardInfo : EntityBase
    {
        public static string StoreRoot { get; set; }

        static IdCardInfo()
        {
            StoreRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IdCardImages");
        }


        private string _relativePath;
        public string RelativePath
        {
            get { return _relativePath; }
            set { SetPropertyValue("RelativePath", ref _relativePath, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue("Name", ref _name, value); }
        }

        private int _sexCode;
        public int SexCode
        {
            get { return _sexCode; }
            set { SetPropertyValue("SexCode", ref _sexCode, value); }
        }

        [NonPersistent]
        public string SexName { get { return _sexCode == 1 ? "男" : "女"; } }

        private int _minorityCode;
        public int MinorityCode
        {
            get { return _minorityCode; }
            set { SetPropertyValue("MinorityCode", ref _minorityCode, value); }
        }

        [NonPersistent]
        public string MinorityName
        {
            get
            {
                return !FileMinorityDictionary.Instance.ContainsKey(MinorityCode) ?
                    "未定义" : FileMinorityDictionary.Instance[MinorityCode];
            }
        }

        private DateTime _bornDate;
        public DateTime BornDate
        {
            get { return _bornDate; }
            set { SetPropertyValue("BornDate", ref _bornDate, value); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetPropertyValue("Address", ref _address, value); }
        }


        private string _idCardNo;
        public string IdCardNo
        {
            get { return _idCardNo; }
            set { SetPropertyValue("IdCardNo", ref _idCardNo, value); }
        }

        private string _grantDept;
        public string GrantDept
        {
            get { return _grantDept; }
            set { SetPropertyValue("GrantDept", ref _grantDept, value); }
        }

        private DateTime _validateFrom;
        public DateTime ValidateFrom
        {
            get { return _validateFrom; }
            set { SetPropertyValue("ValidateFrom", ref _validateFrom, value); }
        }

        private DateTime _validateUntil;
        public DateTime ValidateUntil
        {
            get { return _validateUntil; }
            set { SetPropertyValue("ValidateUntil", ref _validateUntil, value); }
        }

        private byte[] _photoData;

        [NonPersistent]
        [Delayed]
        public byte[] PhotoData
        {
            get
            {
                if (_photoData == null)
                {
                    var abs = GetAbsolutePath();
                    if (string.IsNullOrEmpty(abs)) return null;
                    if (!File.Exists(abs)) return null;

                    try
                    {
                        _photoData = File.ReadAllBytes(abs);
                    }
                    catch
                    {
                        return null;
                    }

                }

                return _photoData;
            }
            set { SetPropertyValue("PhotoData", ref _photoData, value); }
        }

        [NonPersistent]
        [Delayed]
        public System.Drawing.Image CopyOfImage
        {
            get
            {
                return PhotoData == null ? null : System.Drawing.Image.FromStream(new MemoryStream(PhotoData));
            }
        }

        private bool _isSuspect;
        public bool IsSuspect
        {
            get { return _isSuspect; }
            set { SetPropertyValue("IsSuspect", ref _isSuspect, value); }
        }

        private IdStatus _idStatus;
        public IdStatus IdStatus
        {
            get { return _idStatus; }
            set { SetPropertyValue("IdStatus", ref _idStatus, value); }
        }



        public IdCardInfo()
            : base()
        {

        }

        public IdCardInfo(Session session)
            : base(session)
        {

        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (PhotoData != null && RelativePath == null)
            {
                var path = string.Format("{0}{1}\\{2}.jpg", CreationDate.Year, CreationDate.Month, Guid.NewGuid());
                var abs = Path.Combine(StoreRoot, path);
                try
                {
                    var dir = Path.GetDirectoryName(abs);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    File.WriteAllBytes(abs, PhotoData);
                }
                catch
                {
                    return;
                }

                RelativePath = path;
            }
        }

        private string GetAbsolutePath()
        {
            if (RelativePath == null) return null;

            return Path.Combine(StoreRoot, RelativePath);
        }



    }
}
