using System;
using mCore;

namespace Kise.IdCard.Messaging.Link
{
    public class SmsLink : ILink
    {
        private readonly string _comPort;
        private readonly int _baundRate;
        private mCore.SMS _sms;
        private string _curRefNumber;
        private Action<bool> _deliveryCallback;
        private Action<string> _responseCallback;

        public SmsLink(string comPort, int baundRate)
        {
            _comPort = comPort;
            _baundRate = baundRate;
        }

        public void Start()
        {
            if (_sms == null)
            {
                _sms = new SMS();
                _sms.Port = _comPort;
                _sms.Encoding = Encoding.Unicode_16Bit;
                _sms.BaudRate = BaudRate.BaudRate_9600;
                _sms.DataBits = DataBits.Eight;
                _sms.Parity = Parity.None;
                _sms.StopBits = StopBits.One;
                _sms.FlowControl = FlowControl.None;
                _sms.NewMessageConcatenate = true;
                _sms.NewMessageIndication = true;
                _sms.AutoDeleteNewMessage = true;

                _sms.NewMessageReceived += _sms_NewMessageReceived;
            }
        }

        public void SendAsync(string destination, string message)
        {
            if (_sms == null)
            {
                throw new InvalidOperationException("Start() must be called first");
            }

            _curRefNumber = _sms.SendSMS(destination, message);
        }

        public event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;

        public void RaiseNewMessageReceived(MiscUtil.EventArgs<IncomingMessage> e)
        {
            EventHandler<MiscUtil.EventArgs<IncomingMessage>> handler = NewMessageReceived;
            if (handler != null) handler(this, e);
        }

        private void _sms_NewMessageReceived(object sender, NewMessageReceivedEventArgs e)
        {
            var commaIndes = e.TextMessage.IndexOf(":");
            var msg = e.TextMessage;
            if (commaIndes != -1)
            {
                msg = msg.Substring(commaIndes + 1).Trim();
            }

            var incommingMsg = new IncomingMessage(msg);
            incommingMsg.Sender = e.Phone;
            RaiseNewMessageReceived(new MiscUtil.EventArgs<IncomingMessage>(incommingMsg));
        }

        private void _sms_NewDeliveryReport(object sender, NewDeliveryReportEventArgs e)
        {
            if (_deliveryCallback != null)
            {
                _deliveryCallback(true);
            }
        }

    }
}