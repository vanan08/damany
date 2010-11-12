using System;
using System.Collections.Generic;
using System.Linq;
using FileHelpers;


namespace Kise.IdCard.Infrastructure
{
    public class FileMinorityDictionary
    {
        private static Dictionary<int, string> _dict;

        public static IDictionary<int, string> LoadDictionary(string filePath)
        {
            if (_dict == null)
            {
                var reader = new FileHelperEngine<Minority>();
                var codes = reader.ReadFile(filePath);

                _dict = codes.ToDictionary(mc => mc.Code, mc => mc.Name);
            }

            return _dict;
        }

        [DelimitedRecord("-")]
        private class Minority
        {
            public int Code;
            public string Name;
        }

    }


}