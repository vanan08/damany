using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavigationControl
{
    public class NavigationCommand : INavigationCommand
    {

        public static NavigationCommand PanLeft
        {
            get
            {
                var command = new NavigationCommand();
                command.CommandCode = new byte[] { 0x00, 0x04 };
                command.CommandData = new byte[] { 0xff, 0xff };

                return command;

            }
        }

        public static NavigationCommand PanRight
        {
            get
            {
                var command = new NavigationCommand();
                command.CommandCode = new byte[] { 0x00, 0x02 };
                command.CommandData = new byte[] { 0xff, 0xff };

                return command;

            }
        }

        public static NavigationCommand PanUp
        {
            get
            {
                var command = new NavigationCommand();
                command.CommandCode = new byte[] { 0x00, 0x08 };
                command.CommandData = new byte[] { 0xff, 0xff };

                return command;

            }
        }

        public static NavigationCommand PanDown
        {
            get
            {
                var command = new NavigationCommand();
                command.CommandCode = new byte[] { 0x00, 0x02 };
                command.CommandData = new byte[] { 0xff, 0xff };

                return command;

            }
        }

        public static NavigationCommand Stop
        {
            get
            {
                var command = new NavigationCommand();
                command.CommandCode = new byte[] { 0x00, 0x00 };
                command.CommandData = new byte[] { 0x00, 0x00 };

                return command;

            }
        }



        #region IBinaryCommand Members

        byte[] IBinaryCommand.Build()
        {
            return this.Build();
        }

        #endregion

        public byte DestinationAddress { get; set; }
        public byte[] CommandCode { get; set;}
        public byte[] CommandData { get; set;}
    }
}
