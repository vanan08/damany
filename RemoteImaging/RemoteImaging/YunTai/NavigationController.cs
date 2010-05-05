using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NavigationControl;
using Damany.Util.Extensions;

namespace RemoteImaging.YunTai
{
    public class NavigationController
    {
        private readonly INavigationScreen _screen;
        private readonly ConnectionManager _connectionManager 
            = new ConnectionManager();

        private const string NavLeftName = "NavLeft";
        private const string NavRightName = "NavRight";
        private const string StopName = "Stop";

        private readonly Dictionary<string, IEnumerable<INavigationCommand>>
            _commands = new Dictionary<string, IEnumerable<INavigationCommand>>();


        public NavigationController(INavigationScreen screen)
        {
            if (screen == null) throw new ArgumentNullException("screen");
            _screen = screen;

            RegisterCommands();
        }

        public void Start()
        {
            _screen.AttachController(this);
        }

        public void NavLeft()
        {
            var command = _commands[NavLeftName];
            SendCommand(command);
        }

        public void NavRight()
        {
            var command = _commands[NavRightName];
            SendCommand(command);
        }

        private void SendCommand(IEnumerable<INavigationCommand> commands)
        {
            var cam = _screen.SelectedCamera();
            commands.AddressTo( (byte) cam.YunTaiId);
            var stream = _connectionManager.GetConnection(cam.YunTaiUri);
            var buffer = commands.Build();

            stream.Write(buffer, 0, buffer.Length);
        }

        public void NavStop()
        {
            var cmd = _commands[StopName];
            SendCommand(cmd);
        }



        private void RegisterCommands()
        {
            var navLeft = new List<NavigationCommand>();
            navLeft.Add(NavigationCommand.PanLeft);
            _commands.Add(NavLeftName, navLeft.Cast<INavigationCommand>());

            _commands.Add(NavRightName, NavigationCommand.PanRight.AsEnumerable<INavigationCommand>());

            _commands.Add(StopName, NavigationCommand.Stop.AsEnumerable<INavigationCommand>());

        }


    }
}
