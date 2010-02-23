using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Common
{
    public class Location
    {
        public Location(uint id) : this(id, string.Empty) {}

        public Location(uint id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public uint Id { get; set; }
        public string Name { get; set; }
    }
}
