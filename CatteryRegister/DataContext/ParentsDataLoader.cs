using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CatteryRegister.Model;
using GreenDonut;

namespace CatteryRegister.DataContext
{
    public class ParentsDataLoader : DataLoaderBase<int, Parents>
    {
        public ParentsDataLoader(IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
        }

        protected override ValueTask FetchAsync(IReadOnlyList<int> keys, 
            Memory<Result<Parents>> results, CancellationToken cancellationToken)
        {
            var parents = GenealogicalTree.Source.Where(x => keys.Contains(x.ChildId));
            var span = results.Span;
            var id = 0;
            foreach (var key in keys)
            {
                var fromFoundParents = parents.FirstOrDefault(x => x.ChildId == key);

                span[id] = fromFoundParents;
                id++;
            }
            return ValueTask.CompletedTask;
        }
    }
}