using System;

namespace WindowsFormsApplication1
{
    public interface ISetRectangle
    {
        void Set(System.Drawing.Rectangle rectangle, Action<Exception> callBack);
    }
}