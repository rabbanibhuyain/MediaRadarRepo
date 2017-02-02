using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MediaRadar.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdService" in both code and config file together.
    [ServiceContract]
    public interface IAdService
    {
        [OperationContract]
        void DoWork();
    }
}
