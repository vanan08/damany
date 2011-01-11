using System.Collections.Generic;
using System.Linq;
using FileHelpers;

namespace Kise.IdCard.Model
{
    public class FileMinorityDictionary
    {
        private static Dictionary<int, string> _dict;

        public static IDictionary<int, string> Instance
        {
            get
            {
                if (_dict == null)
                {
                    var reader = new FileHelperEngine<Minority>();
                    var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "MinorityCode.txt");
                    var codes = reader.ReadFile(path);

                    _dict = codes.ToDictionary(mc => mc.Code, mc => mc.Name);
                }

                return _dict;
            }

        }

        [DelimitedRecord("-")]
        private class Minority
        {
            public int Code;
            public string Name;
        }

    }


}