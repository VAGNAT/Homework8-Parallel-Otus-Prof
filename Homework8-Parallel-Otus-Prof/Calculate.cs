using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Runtime.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework8_Parallel_Otus_Prof
{
    public static class Calculate
    {
        public static long SimpleSum(int[] array)
        {
            long sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }

        public static long ThreadSumWithChunkArray(int[] array)
        {
            long sum = default;
            int processorsCount = Environment.ProcessorCount;
            var partionArray = array.Chunk((int)Math.Ceiling(array.Length / (decimal)processorsCount)).ToArray();

            long[] sums = new long[processorsCount];
            Thread[] threads = new Thread[processorsCount];

            for (int i = 0; i < processorsCount; i++)
            {
                int localI = i;
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < partionArray[localI].Length; j++)
                    {
                        sums[localI] += partionArray[localI][j];
                    }
                });
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            for (int i = 0; i < sums.Length; i++)
            {
                sum += sums[i];
            }
            return sum;
        }

        public static long ThreadSum(int[] array)
        {
            long sum = 0;
            int processorsCount = Environment.ProcessorCount;
            long[] sums = new long[processorsCount];
            Thread[] threads = new Thread[processorsCount];

            for (int i = 0; i < processorsCount; i++)
            {
                int localI = i;
                threads[i] = new Thread(() =>
                {
                    int threadPart = array.Length / processorsCount;
                    int start = localI * threadPart;
                    int end = (localI == processorsCount - 1) ? array.Length : start + threadPart;
                    for (int j = start; j < end; j++)
                    {
                        sums[localI] += array[j];
                    }
                });
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            for (int i = 0; i < sums.Length; i++)
            {
                sum += sums[i];
            }
            return sum;
        }

        public static long ParallelLinqSum(int[] array)
        {
            return array.AsParallel().Aggregate(
                seed: (long)0,
                func: (total, next) => total + next,
                resultSelector: total => total);
        }

        public static long TaskSum(int[] array)
        {
            long sum = 0;

            int processorsCount = Environment.ProcessorCount;
            var partionArray = array.Chunk((int)Math.Ceiling(array.Length / (decimal)processorsCount)).ToArray();
            var tasks = new Task<long>[processorsCount];

            int count = default;
            foreach (var arr in partionArray)
            {
                tasks[count] = Task.Run(() =>
                {
                    long localSum = default;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        localSum += arr[i];
                    }
                    return localSum;
                });

                count++;
            }

            Task.WaitAll(tasks);

            foreach (var item in tasks)
            {
                sum += item.Result;
            }

            return sum;
        }

        public static long ParallelSum(int[] array)
        {
            long sum = 0;

            int processorsCount = Environment.ProcessorCount;
            var partionArray = array.Chunk((int)Math.Ceiling(array.Length / (decimal)processorsCount)).ToArray();

            Parallel.ForEach(partionArray, (arr) =>
            {
                long localSum = default;
                for (int i = 0; i < arr.Length; i++)
                {
                    localSum += arr[i];
                }
                sum += localSum;
            });

            return sum;
        }

        public static long LinqSum(int[] array)
        {
            return array.Sum(item=>(long)item);
        }
    }
}