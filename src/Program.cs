using BenchmarkDotNet.Running;

namespace CooperaSharp_QueryInLoop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<QueryBenchmark>();
        }
    }
}
