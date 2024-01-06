using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework8_Parallel_Otus_Prof.Helpers
{
    internal static class Generator
    {
        internal static int[] GetIntArray(int count)
        {
            var rnd = new Random();
            return Enumerable.Range(1, count).Select(x => x + rnd.Next(1, 100)).ToArray();
        }
    }
}
