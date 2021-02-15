using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> func)
        {
            foreach (var elem in collection)
                func(elem);
        }
    }
}
