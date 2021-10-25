using System.Collections.Generic;
using CatteryRegister.Model;

namespace CatteryRegister.DataContext
{
    public static class GenealogicalTree
    {
        public static IReadOnlyList<Parents> Source = new List<Parents>()
        {
            new Parents(2, 1, 4),
            new Parents(7, 2, 6),
            new Parents(9, 7, 8),
            new Parents(9, 7, 9),
            new Parents(9, 7, 10),
            new Parents(9, 7, 11),

            new Parents(13, 5, 6),
            new Parents(14, 5, 6),
            new Parents(15, 5, 6),
            new Parents(16, 5, 6),
            new Parents(17, 3, 14)
        };
    }
}