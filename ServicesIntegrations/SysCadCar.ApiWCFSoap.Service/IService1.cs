using SysCadCar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SysCadCar.ApiWCFSoap.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        bool AddCarro(Carro carroAdd);

        [OperationContract]
        IEnumerable<Carro> listCarro();

        [OperationContract]
        Carro viewsCarro(string placa);
    }
}
