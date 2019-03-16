using InterroleContracts;
using System;
using System.ServiceModel;

namespace JobInvoker
{
    class Program
    {
        private static IJob proxy;

        static void Main(string[] args)
        {
            while (true)
            {
                Connect();
                int n = Input();
                Console.WriteLine("Rez: " + proxy.DoCalculus(n));
            }
        }


        static int Input()
        {
            while (true)
            {
                Console.Write("Input max number:");
                if (Int32.TryParse(Console.ReadLine(), out int input))
                {
                    return input;
                }
            }
        }


        public static void Connect()
        {
            var binding = new NetTcpBinding();
            ChannelFactory<IJob> factory = new ChannelFactory<IJob>(binding, new EndpointAddress("net.tcp://localhost:10100/InputRequest"));

            proxy = factory.CreateChannel();
            Console.WriteLine("Connected");
        }
    }
}
