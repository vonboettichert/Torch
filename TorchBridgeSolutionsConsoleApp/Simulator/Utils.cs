using System.Collections.Generic;
using System.Linq;

namespace TorchBridgeSolutionsConsoleApp.Simulator
{
    public static class Utils
    {
        internal static IEnumerable<T[]> Permute<T>(IEnumerable<T> items, int count)
        {
            var i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                    yield return new[] {item};
                else
                    foreach (var result in Permute(items.Skip(i + 1), count - 1))
                    {
                        var newArray = new[] {item}.Concat(result);
                        yield return newArray.ToArray();
                    }
                ++i;
            }
        }
    }
}