using System;

namespace Kise.IdCard.Server
{
    public class IdLookupService : IIdLookupService
    {
        RBSPAdapter_COM.RSBPAdapterCOMObjClass _provider;

        public IdLookupService()
        {
            _provider = new RBSPAdapter_COM.RSBPAdapterCOMObjClass();
        }
        public string queryType
        {
            set
            {
                _provider.queryType = value;
            }
        }
        public string queryCondition
        {
            get
            {
                return _provider.queryCondition;
            }
            set
            {
                _provider.queryCondition = value;
            }
        }
    }
}
