using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.PC.Domain
{
    public class CameraInfo
    {
        public int Id { get; set; }
        public Uri Location { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CameraProvider Provider { get; set; }
        public bool Enabled { get; set; }

        public string LoginUserName { get; set; }
        public string LoginPassword { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if ( !(obj is CameraInfo) )
            {
                return false;
            }

            var other = (CameraInfo) obj;

            return this.Location.Equals(other.Location);
        }
    }
}
