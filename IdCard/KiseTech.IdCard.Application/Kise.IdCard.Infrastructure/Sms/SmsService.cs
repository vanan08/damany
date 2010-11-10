using System;
using System.Threading.Tasks;
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
            _sms.Port = _comPort;
            _sms.Encoding = Encoding.Unicode_16Bit;
            _sms.BaudRate = BaudRate.BaudRate_9600;
            _sms.DataBits = DataBits.Eight;
            _sms.Parity = Parity.None;
            _sms.StopBits = StopBits.One;
            _sms.FlowControl = FlowControl.None;
            _sms.NewMessageConcatenate = true;

        }

        void _sms_NewMessageReceived(object sender, NewMessageReceivedEventArgs e)
        {
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

        public async Task<string> QueryAsync(string destinationNumber, string message)
        {

            await TaskEx.Run(() =>
                           {
                               _sms.AutoDeleteNewMessage = true;
                               _sms.DeliveryReport = true;
                               _sms.NewMessageIndication = true;
                           });

            return await SendAsync(destinationNumber, message);

        }

        private Task<string> SendAsync(string destinationNumber, string message)
        {
            var tcs = new TaskCompletionSource<string>();

            _curRefNumber = _sms.SendSMS(destinationNumber, message);
            _sms.NewMessageReceived += (s, e) =>
                                           {
                                               tcs.TrySetResult(e.TextMessage);
                                           };
            return tcs.Task;
        }
    }
}