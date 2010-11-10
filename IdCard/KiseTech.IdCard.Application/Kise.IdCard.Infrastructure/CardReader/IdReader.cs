using System;

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

        public IdInfo Read()
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
                Address = cardData.Address,
                BornDate = cardData.Born,
                GrantDept = cardData.GrantDept,
                Minority = cardData.Nation,
                Name = cardData.Name,
                PhotoFilePath = cardData.PhotoFileName,
                Sex = cardData.Sex,
                ValidateFrom = cardData.UserLifeBegin,
                ValidateUntil = cardData.UserLifeEnd,
                IdCardNo = cardData.IDCardNo
            };
            return info;

        Error:
            throw new Exception("read idcard error");

        }
    }
}