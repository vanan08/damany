using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Damany.Util.Extensions;

namespace NavigationControl
{
    public class Command : ICommand
    {
        private readonly IEnumerable<IBinaryCommand> _commands;
        private readonly Stream _sender;

        public Command(IBinaryCommand command, Stream sender) : this(command.AsEnumerable(), sender) {}

        public Command(IEnumerable<IBinaryCommand> commands, Stream sender)
        {
            _commands = commands;
            _sender = sender;
        }

        public void Execute()
        {
            var buffer = _commands.Build();
           _sender.Write(buffer, 0, buffer.Length);
        }
    }
}
