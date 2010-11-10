using System;
using mCore;

namespace Kise.IdCard.Infrastructure.Sms
{
    public class SmsService : ISmsService
    {
        private readonly string _comPort;
        private readonly int _baundRate;
        private mCore.SMS _sms;
        private string _curRefNumber;
        private Action<bool> _deliveryCallback;
        private Action<string> _responseCallback;

        public SmsService(string comPort, int baundRate)
        {
            _comPort = comPort;
            _baundRate = baundRate;


            InitSmsModule();
        }

        private void InitSmsModule()
        {
            _sms = new SMS();
            _sms.BaudRate = BaudRate.BaudRate_9600;
            _sms.NewMessageIndication = true;
            _sms.AutoDeleteNewMessage = true;
            _sms.DeliveryReport = true;


            _sms.NewDeliveryReport += _sms_NewDeliveryReport;
            _sms.NewMessageReceived += _sms_NewMessageReceived;

        }

        void _sms_NewMessageReceived(object sender, NewMessageReceivedEventArgs e)
        {
            if (!ShouldDoCallBack(e.ReferenceNumber)) return;

            if (_responseCallback != null)
            {
                _responseCallback(e.TextMessage);
            }

        }

        private bool ShouldDoCallBack(int referenceNumber)
        {
            return referenceNumber == int.Parse(_curRefNumber);
        }

        void _sms_NewDeliveryReport(object sender, NewDeliveryReportEventArgs e)
        {
            if (!ShouldDoCallBack(e.MessageReference)) return;

            if (_deliveryCallback != null)
            {
                _deliveryCallback(true);
            }
        }

        public void Send(string destinationNumber, string message, Action<bool> deliverCallback, Action<string> responseCallback)
        {
            _curRefNumber = _sms.SendSMS(destinationNumber, message);
            _deliveryCallback = deliverCallback;
            _responseCallback = responseCallback;
        }
    }
}