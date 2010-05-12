using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class SearchCretia
    {
        List<Predicate<DtoLicensePlateInfo>> _predicates = new List<Predicate<DtoLicensePlateInfo>>();

        public void AddCretia(Predicate<DtoLicensePlateInfo> predicate)
        {
            _predicates.Add(predicate);
        }

        public bool MatchWith(DtoLicensePlateInfo toMatch)
        {
            foreach (var predicate in _predicates)
            {
                if (!predicate(toMatch))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
