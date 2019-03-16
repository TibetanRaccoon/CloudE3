using InterroleContracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.ServiceModel;
using System.Threading.Tasks;

namespace JobWorker
{
    class JobServerProvider : IJob
    {
        public int DoCalculus(int to)
        {
            var roleId = RoleEnvironment.CurrentRoleInstance.Id;
            int sum = 0;

            int instanceCount = RoleEnvironment.Roles[RoleEnvironment.CurrentRoleInstance.Role.Name].Instances.Count;
            int range = to / (instanceCount - 1);
            System.Console.WriteLine(instanceCount);

            if (to % (instanceCount - 1) != 0)
            {
                range++;
            }
            int start = 1;
            int end = range;

            foreach (var item in RoleEnvironment.Roles[RoleEnvironment.CurrentRoleInstance.Role.Name].Instances)
            {
                if (item.Id != roleId)
                {
                    var binding = new NetTcpBinding();
                    ChannelFactory<IJobPartial> factory = new ChannelFactory<IJobPartial>(binding, new EndpointAddress("net.tcp://" + item.InstanceEndpoints["InternalRequest"].IPEndpoint + "/InternalRequest"));
                    IJobPartial proxy = factory.CreateChannel();


                    Task<int> task = new Task<int>(() =>
                    {
                        return proxy.DoCalculusRange(start, end);
                    });

                    task.Start();
                    sum += task.Result;

                    start += range;
                    end += range;
                    if (end > to)
                    {
                        end = to;
                    }

                }
            }

            Task.WaitAll();
            return sum;
        }
    }
}
