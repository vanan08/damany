using System;
using System.Threading.Tasks;

namespace Kise.IdCard.Infrastructure.CardReader
{
    public class IdCardReader : IIdCardReader
    {
        public IdCardReader()
            : this(1001)
        {
        }

        public IdCardReader(int port)
        {
            Port = port;
        }

        public int Port { get; private set; }

        public async Task<IdInfo> ReadAsync()
        {
            var cardData = new IdCardData();
            var success = 1;

            success = CardReaderWrapper.Syn_OpenPort(Port);
            if (success != 0)
            {
                goto Error;
            }

            success = CardReaderWrapper.Syn_GetSAMStatus(Port, 0);
            if (success != 0)
            {
                //goto Error;
            }

            byte[] pucIIN = new byte[4];
            byte[] pucSN = new byte[8];

            success = CardReaderWrapper.Syn_StartFindIDCard(Port, ref pucIIN[0], 0);
            if (success != 0)
            {
                //goto Error;
            }

            success = CardReaderWrapper.Syn_SelectIDCard(Port, ref pucSN[0], 0);
            if (success != 0)
            {
                //goto Error;
            }

            success = CardReaderWrapper.Syn_ReadMsg(Port, 0, ref cardData);
            if (success != 0)
            {
                goto Error;
            }


            success = CardReaderWrapper.Syn_ClosePort(Port);
            if (success != 0)
            {
                goto Error;
            }

            var info = new IdInfo()
                           {
                               Address = cardData.Address.Trim(),
                               BornDate = cardData.Born.Trim(),
                               GrantDept = cardData.GrantDept.Trim(),
                               Minority = cardData.Nation.Trim().TrimStart('0'),
                               Name = cardData.Name.Trim(),
                               PhotoData = System.IO.File.ReadAllBytes(cardData.PhotoFileName),
                               Sex = cardData.Sex.Trim().TrimStart('0'),
                               ValidateFrom = cardData.UserLifeBegin.Trim(),
                               ValidateUntil = cardData.UserLifeEnd.Trim(),
                               IdCardNo = cardData.IDCardNo.Trim()
                           };
            return info;

        Error:
            throw new Exception("read idcard error");


        }
    }
}