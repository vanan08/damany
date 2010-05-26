using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace StreamService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                     IncludeExceptionDetailInFaults = true)]
    class VLCServer : RemoteControlService.IStreamPlayer
    {
        private AXVLC.VLCPlugin2Class player;
        private System.Diagnostics.EventLog logger;

        public VLCServer(System.Diagnostics.EventLog l)
        {
            this.player = new AXVLC.VLCPlugin2Class();
            this.logger = l;
        }

        #region IStreamPlayer Members

        public void Stop()
        {
            if (this.player.playlist.isPlaying)
            {
                this.player.playlist.stop();
                this.logger.WriteEntry("stop playing");
            }
        }

        public bool StreamVideo(string path)
        {
            string mrl = string.Format("file://{0}", path);

            //client ip
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = endpoint.Address;

            string streamTo = string.Format(":sout=udp:{0}", "239.255.12.12");
            string[] options = new string[] { "-vvv", streamTo, ":ttl=1" };


            this.player.playlist.items.clear();
            int idx = this.player.playlist.add(mrl, null, options);
            this.player.playlist.playItem(idx);

            this.logger.WriteEntry("play file-[" + path + "]");

            return true;
        }

        #endregion
    }
}
