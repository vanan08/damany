using System;

namespace Kise.IdCard.UI
{
    using Model;

    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        //ISmsService sms = new SmsService("com3", 9600);

        public Form1()
        {
            InitializeComponent();

            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MinorityCode.txt");
            idCardControl1.MinorityDictionary = Infrastructure.FileMinorityDictionary.LoadDictionary(path);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            var idReader = new Infrastructure.CardReader.IdCardReader(1001);
            while (true)
            {
                Infrastructure.CardReader.IdInfo info = null;

                await System.Threading.Tasks.TaskEx.Delay(3000);
                try
                {
                    info = await idReader.ReadAsync();
                }
                catch (Exception)
                {
                    continue;
                }

                idCardControl1.IdCardInfo = info.ToModelIdCardInfo();
            }
        }
    }
}
