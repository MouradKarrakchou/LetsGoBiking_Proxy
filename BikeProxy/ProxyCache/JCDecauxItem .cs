using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ProxyCache
{
    internal class JCDecauxItemContract
    {
        public List<JCDContract> contracts;
        JcdecauxToolProxy jcdecauxToolProxy = new JcdecauxToolProxy();
        public JCDecauxItemContract(List<Object> objects)
        {
            contracts = jcdecauxToolProxy.getAllContracts();
        }
    }
    internal class JCDecauxItemStations
    {
        public List<JCDStation> stations;
        JcdecauxToolProxy jcdecauxToolProxy = new JcdecauxToolProxy();
        public JCDecauxItemStations(List<Object> objects)
        {
            stations = jcdecauxToolProxy.getStations((String) objects[0]);
        }
    }
}
