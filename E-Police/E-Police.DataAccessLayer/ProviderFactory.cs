using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using E_Police.DataAccessLayer.Properties;

namespace E_Police.DataAccessLayer
{
    public static class ProviderFactory
    {
        public static IDataProvider CreateProvider()
        {
            object objDataProvider = Assembly.GetExecutingAssembly()
                .CreateInstance(Settings.Default.DataProvider, true);
            if (objDataProvider == null)
            {
                throw new Exception(string.Format("Can't create instance of {0}",
                    Settings.Default.DataProvider));
            }



            IDataProvider provider = objDataProvider as IDataProvider;
            if (provider == null)
            {
                throw new Exception(string.Format("{0} does not implement IDataProvider",
                    Settings.Default.DataProvider));
            }

            return provider;
        }
    }
}
