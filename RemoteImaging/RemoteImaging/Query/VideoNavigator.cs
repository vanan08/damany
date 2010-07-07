using System;
using System.ComponentModel;
using DevExpress.XtraEditors;

namespace RemoteImaging.Query
{
    public class VideoNavigator : DevExpress.XtraEditors.INavigatableControl, System.ComponentModel.INotifyPropertyChanged
    {
        private INavigatorOwner _owner;
        public void AddNavigator(INavigatorOwner owner)
        {
            _owner = owner;
        }

        public void RemoveNavigator(INavigatorOwner owner)
        {

        }

        public bool IsActionEnabled(NavigatorButtonType type)
        {
            return true;
        }

        public void DoAction(NavigatorButtonType type)
        {
            switch (type)
            {
                case NavigatorButtonType.Custom:
                    break;
                case NavigatorButtonType.First:
                    break;
                case NavigatorButtonType.PrevPage:
                    break;
                case NavigatorButtonType.Prev:
                    break;
                case NavigatorButtonType.Next:
                    break;
                case NavigatorButtonType.NextPage:
                    if (Position + 1 < RecordCount - 1)
                    {
                        ++_position;

                        var e = new PropertyChangedEventArgs("Position");
                        InvokePropertyChanged(e);
                    }
                    break;
                case NavigatorButtonType.Last:
                    break;
                case NavigatorButtonType.Append:
                    break;
                case NavigatorButtonType.Remove:
                    break;
                case NavigatorButtonType.Edit:
                    break;
                case NavigatorButtonType.EndEdit:
                    break;
                case NavigatorButtonType.CancelEdit:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        public int RecordCount
        {
            get { return 10; }
        }

        private int _position;
        public int Position
        {
            get
            {
                return _position;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
    }
}