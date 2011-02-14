using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDetectorTester.Models
{
    public class LengthAndWidth : Cinch.EditableValidatingObject
    {
        public int Width { get; set; }
        public int Length { get; set; }
    }
}
