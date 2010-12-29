using System;

namespace Kise.IdCard.Server
{
    internal class InvalidQueryFormatException : Exception
    {
        public InvalidQueryFormatException(Exception innerException)
            : base("查询命令格式错误", innerException)
        {

        }
    }
}