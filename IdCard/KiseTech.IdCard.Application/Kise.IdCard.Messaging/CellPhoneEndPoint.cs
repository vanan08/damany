using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kise.IdCard.Messaging
{
    public class CellPhoneEndPoint : System.Net.EndPoint
    {
        public string CellNumber { get; set; }
    }
}
