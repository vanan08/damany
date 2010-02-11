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

        public static void Raise(this EventHandler eventHandler,
                                 object sender, EventArgs e)
        {
            if (eventHandler == null) return;

            if (eventHandler.Target is System.Windows.Forms.Control)
            {
                var control = eventHandler.Target as System.Windows.Forms.Control;
                control.BeginInvoke(eventHandler, sender, e);
            }
            else
            {
                eventHandler(sender, e);
            }

        }

        public static void Raise<T>(this EventHandler<T> eventHandler,
            object sender, T e) where T : EventArgs
        {
            if (eventHandler == null) return;

            if (eventHandler.Target is System.Windows.Forms.Control)
            {
                var control = eventHandler.Target as System.Windows.Forms.Control;
                control.BeginInvoke(eventHandler, sender, e);
            }
            else
            {
                eventHandler(sender, e);
            }

        }
    }
}
