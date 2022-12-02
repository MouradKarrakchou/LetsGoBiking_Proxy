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
            return (genericProxyCacheContract.Get("contracts").contracts);
        }

        public List<JCDStation> getStations(string contract)
        {
            genericProxyCacheStation = new GenericProxyCache<JCDecauxItemStations>();
            return (genericProxyCacheStation.Get(contract).stations);
        }
    }
}
