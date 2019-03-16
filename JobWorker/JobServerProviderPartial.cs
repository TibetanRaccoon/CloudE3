using InterroleContracts;
using System.Diagnostics;

namespace JobWorker
{
    class JobServerProviderPartial : IJobPartial
    {
        public int DoCalculusRange(int from, int to)
        {
            // initialize sum to zero for case from=a and to=a 
            int sum = 0;
            for (int i = from; i <= to; i++)
            {
                sum += i;
            }

            Trace.WriteLine($"DoSum method called - interval[{from}, {to}]. Result:{sum}");
            return sum;
        }
    }
}
