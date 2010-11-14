﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace Kise.IdCard.Model
{
    public class IdCardInfo : EntityBase
    {
        public string Name { get; set; }
        public int SexCode { get; set; }
        public int MinorityCode { get; set; }
        public DateTime BornDate { get; set; }
        public string Address { get; set; }
        public string IdCardNo { get; set; }
        public string GrantDept { get; set; }
        public DateTime ValidateFrom { get; set; }
        public DateTime ValidateUntil { get; set; }
        public byte[] PhotoData { get; set; }

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


    }
}
