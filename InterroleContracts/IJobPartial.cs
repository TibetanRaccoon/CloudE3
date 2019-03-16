using System.ServiceModel;

namespace InterroleContracts
{
    [ServiceContract]
    public interface IJobPartial
    {
        [OperationContract]
        int DoCalculusRange(int from, int to);
    }
}
