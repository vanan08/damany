﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public interface IFacePostFilter
    {
        bool IsFace(Portrait portrait);
    }
}
