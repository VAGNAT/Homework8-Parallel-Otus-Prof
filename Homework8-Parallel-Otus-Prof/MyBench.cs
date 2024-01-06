using BenchmarkDotNet.Attributes;
using Homework8_Parallel_Otus_Prof.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework8_Parallel_Otus_Prof
{
    [MemoryDiagnoser]
    public class MyBench
    {
        private int[]? data;

        [Params(100000, 1000000, 10000000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            data = Generator.GetIntArray(N);
        }

        [Benchmark]
        public void SimpleSum()
        {
            Calculate.SimpleSum(data!);
        }               

        [Benchmark]
        public void LinqSum()
        {
            Calculate.LinqSum(data!);
        }

        [Benchmark]
        public void TaskSum()
        {
            Calculate.TaskSum(data!);
        }

        [Benchmark]
        public void ThreadSum()
        {
            Calculate.ThreadSum(data!);
        }

        [Benchmark]
        public void ThreadSumWithChunkArray()
        {
            Calculate.ThreadSumWithChunkArray(data!);
        }        
    }
}
