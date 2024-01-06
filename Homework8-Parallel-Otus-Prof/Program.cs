using BenchmarkDotNet.Running;
using Homework8_Parallel_Otus_Prof;
using Homework8_Parallel_Otus_Prof.Helpers;

BenchmarkRunner.Run<MyBench>();

//var arr = Generator.GetIntArray(10000000);

//Console.WriteLine(Calculate.SimpleSum(arr));
//Console.WriteLine(Calculate.ThreadSumWithChunkArray(arr));
//Console.WriteLine(Calculate.ThreadSum(arr));
//Console.WriteLine(Calculate.ParallelLinqSum(arr));
//Console.WriteLine(Calculate.TaskSum(arr));
//Console.WriteLine(Calculate.ParallelSum(arr));
//Console.WriteLine(Calculate.LinqSum(arr));