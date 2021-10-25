using System.Collections.Generic;
using System.Linq;
using CatteryRegister.Model;

namespace CatteryRegister.DataContext
{
    public class ParentsService
    {
        public Parents? GetParents(int childId)
        {
            var result =  GenealogicalTree.Source.FirstOrDefault(x => x.ChildId == childId);
            return result;
        }

        public IReadOnlyList<Parents> GetManyParents(int[] childIds)
        {
            return GenealogicalTree.Source.Where(x => childIds.Contains(x.ChildId)).ToArray();
        }
    }
}