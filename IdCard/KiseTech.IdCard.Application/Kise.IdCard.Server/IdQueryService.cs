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
        static IdQueryService _instance;
        private readonly IIdLookupService _idLookupService;

        public IdQueryService(IIdLookupService idLookupService)
        {
            _idLookupService = idLookupService;
        }


        public IdLookUpResult QueryAsync(string queryString)
        {

            _idLookupService.queryCondition = queryString;
            _idLookupService.queryType = "QueryQGRK";

            var replyXml = string.Empty;
            replyXml = _idLookupService.queryCondition;
            var normalQr = Helper.Parse(replyXml);

            _idLookupService.queryType = "QueryZTK";
            replyXml = _idLookupService.queryCondition;
            var suspectQr = Helper.Parse(replyXml);

            var result = new IdLookUpResult();

            return result;
        }


    }

}