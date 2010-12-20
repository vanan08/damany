using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kise.IdCard.Server
{
    public class IdQueryService
    {
        static RBSPAdapter_COM.RSBPAdapterCOMObjClass _queryProvider;
        static IdQueryService _instance;

        private IdQueryService()
        {

        }

        public static IdQueryService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new IdQueryService();
                }

                return _instance;
            }
        }

        public IdLookUpResult QueryAsync(string queryString)
        {
            if (_queryProvider == null)
            {
                _queryProvider = new RBSPAdapter_COM.RSBPAdapterCOMObjClass();
            }

            _queryProvider.queryCondition = queryString;
            _queryProvider.queryType = "QueryQGRK";

            var replyXml = string.Empty;
            replyXml = _queryProvider.queryCondition;
            var normalQr = Helper.Parse(replyXml);

            _queryProvider.queryType = "QueryZTK";
            replyXml = _queryProvider.queryCondition;
            var suspectQr = Helper.Parse(replyXml);

            var result = new IdLookUpResult();
            if (normalQr.Error != null)
            {
                result.NormalResult = "0";
            }
            else
            {
                if (normalQr.IdInfos.Length > 0)
                {
                    var info = normalQr.IdInfos[0];
                    result.NormalResult = "1";
                }
                else
                    result.NormalResult = "2";
            }

            if (suspectQr.Error != null)
            {
                result.SuspectResult = "0";
            }
            else
            {
                if (suspectQr.IdInfos.Length > 0)
                {
                    var info = suspectQr.IdInfos[0];
                    result.SuspectResult = "1";
                }
                else
                    result.SuspectResult = "2";
            }

            return result;
        }


    }

}