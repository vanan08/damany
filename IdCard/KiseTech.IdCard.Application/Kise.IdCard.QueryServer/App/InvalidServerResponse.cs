using System;

namespace Kise.IdCard.Server
{
    public class InvalidServerResponseException : Exception
    {
        public InvalidServerResponseException(Exception inner)
            : base("服务器查询出错", inner)
        {

        }

        public InvalidServerResponseException()
        {

        }
    }
}