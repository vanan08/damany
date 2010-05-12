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

        public const string CmdNavLeft = "NavLeft";
        public const string CmdNavRight = "NavRight";
        public const string CmdNavUp = "NavUp";
        public const string CmdNavDown = "NavDown";
        public const string CmdStop = "Stop";

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
            ExeCuteCommand(CmdNavLeft);
        }

        public void NavRight()
        {
            ExeCuteCommand(CmdNavRight);
        }

        public void NavUp()
        {
            ExeCuteCommand(CmdNavUp);
        }


        public void NavDown()
        {
            ExeCuteCommand(CmdNavDown);
        }


        public void NavStop()
        {
            var cmd = _commands[CmdStop];
            SendCommand(cmd);
        }

        public void ExeCuteCommand(string commandName)
        {
            var cmd = _commands[commandName];
            SendCommand(cmd);
        }

        public void RegisterCommand(string commandName, IEnumerable<INavigationCommand> commands)
        {
            if (_commands.ContainsKey(commandName))
            {
                throw new ArgumentException("key already exist");
            }

            _commands.Add(commandName, commands);
        }

        public IEnumerable<INavigationCommand> this[string commandName]
        {
           get
           {
               return _commands[commandName];
           }
        }


        private void RegisterCommands()
        {
            _commands.Add(CmdNavLeft, NavigationCommand.PanLeft.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdNavRight, NavigationCommand.PanRight.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdNavUp, NavigationCommand.PanUp.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdNavDown, NavigationCommand.PanDown.AsEnumerable<INavigationCommand>());

            _commands.Add(CmdStop, NavigationCommand.Stop.AsEnumerable<INavigationCommand>());

        }


        private void SendCommand(IEnumerable<INavigationCommand> commands)
        {
            var cam = _screen.SelectedCamera();
            commands.AddressTo((byte)cam.YunTaiId);
            var stream = _connectionManager.GetConnection(cam.YunTaiUri);
            var buffer = commands.Build();

            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
