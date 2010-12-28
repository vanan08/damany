using System;

namespace Kise.IdCard.Server
{
    public class IdLookupServiceMock : IIdLookupService
    {
        public string queryType
        {
            set
            {

            }
        }
        public string queryCondition
        {
            get
            {
                return System.IO.File.ReadAllText("1.xml");
            }
            set
            {

            }
        }
    }
}
