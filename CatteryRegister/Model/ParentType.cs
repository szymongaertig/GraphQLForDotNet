using System.Linq;
using CatteryRegister.DataContext;
using HotChocolate.Types;

namespace CatteryRegister.Model
{
    public class ParentType : ObjectType<Parents>
    {
        protected override void Configure(IObjectTypeDescriptor<Parents> descriptor)
        {
            descriptor.Field("female")
                .Resolve<Cat>((cx, ct) =>
                {
                    var dbContext = cx.Service<CatteryDbContext>();
                    var parentNode = cx.Parent<Parents>();
                    var cat = dbContext.Set<Cat>().Single(x => x.Id == parentNode.FemaleParentId);
                    return cat;
                });
            
            descriptor.Field("male")
                .Resolve<Cat>((cx, ct) =>
                {
                    var dbContext = cx.Service<CatteryDbContext>();
                    var parentNode = cx.Parent<Parents>();
                    var cat = dbContext.Set<Cat>().Single(x => x.Id == parentNode.MaleParentId);
                    return cat;
                });
            base.Configure(descriptor);
        }
    }
}