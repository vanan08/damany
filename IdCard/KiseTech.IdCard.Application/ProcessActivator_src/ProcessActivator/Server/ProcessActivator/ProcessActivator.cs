using System;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using IProcessActivator;

namespace ProcessActivator
{
	/// <summary>
	/// Summary description for ProcessActivator.
	/// </summary>
	public class ProcessActivator : MarshalByRefObject, IProcessActivator.IProcessActivator
	{
		public ProcessActivator()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public bool Run(string strProgramName, string strArgumentString)
		{
			Process.Start(strProgramName, strArgumentString);

			return false;
		}

		public static void Main()
		{
			TcpServerChannel channel = new TcpServerChannel(9000);

			ChannelServices.RegisterChannel(channel);

			WellKnownServiceTypeEntry remObj = new WellKnownServiceTypeEntry
			(
				typeof(ProcessActivator),
				"ProcessActivator",
				WellKnownObjectMode.SingleCall
			);

			RemotingConfiguration.RegisterWellKnownServiceType(remObj);

			Console.WriteLine("Press [ENTER] to exit.");

			Console.ReadLine();
		}
	}
}
