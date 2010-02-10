using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Common.Util
{
    public static class Extensions
    {
        public static void CopyTo(this byte[] from, int offsetFrom, int leng, byte[] to, int offsetTo)
        {
            unsafe
            {
                for (int i = 0; i < leng; ++i)
                {
                    fixed (byte* pFrom = &from[offsetFrom + i])
                    fixed (byte* pTo = &to[offsetTo + i])
                    {
                        *pTo = *pFrom;
                    }

                }
            }
        }
    }
}
