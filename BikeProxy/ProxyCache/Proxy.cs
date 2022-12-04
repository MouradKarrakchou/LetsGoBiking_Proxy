using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ProxyCache
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Proxy" à la fois dans le code et le fichier de configuration.
    public class Proxy : IProxy
    {
        JcdecauxToolProxy jcdecauxTool = new JcdecauxToolProxy();
        GenericProxyCache<JCDecauxItemContract> genericProxyCacheContract;
        GenericProxyCache<JCDecauxItemStations> genericProxyCacheStation;
        public List<JCDContract> getAllContracts()
        {
            genericProxyCacheContract = new GenericProxyCache<JCDecauxItemContract>();
            string cacheItemName = "contracts";
            object[] args = new object[1] { cacheItemName };
            return (genericProxyCacheContract.Get("contracts", args).contracts);
        }

        public JCDContract getContract(string cityName)
        {
            genericProxyCacheContract = new GenericProxyCache<JCDecauxItemContract>();
            string cacheItemName = cityName;
            object[] args = new object[1] { cacheItemName };
            List<JCDContract> contracts = genericProxyCacheContract.Get(cityName, args).contracts;
            foreach (JCDContract c in contracts)
            {
                if (c.name == cityName.ToLower())
                    return c;
                if(c.cities != null)
                    foreach (String city in c.cities)
                    {
                        if (city == cityName)
                            return c;
                    }
            }
            return null;
        }

        public List<JCDStation> getStations(string contract)
        {
            string cacheItemName = contract;
            object[] args = new object[1] { cacheItemName };
            genericProxyCacheStation = new GenericProxyCache<JCDecauxItemStations>();
            return (genericProxyCacheStation.Get(contract, args).stations);
        }
    }
}
