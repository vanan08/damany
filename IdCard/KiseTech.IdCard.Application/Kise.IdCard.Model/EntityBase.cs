using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kise.IdCard.Model
{
    public class EntityBase : DevExpress.Xpo.XPObject
    {
        public DateTime CreationDate { get; set; }
    }
}
