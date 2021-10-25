using System;
using System.Linq;
using CatteryRegister.DataContext;
using HotChocolate.Types;

namespace CatteryRegister.Model
{
    public class CatType : ObjectType<Cat>
    {
        protected override void Configure(IObjectTypeDescriptor<Cat> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field("image")
                .Resolve((cx, ct) =>
                {
                    var parentCat = cx.Parent<Cat>();
                    return new Image(parentCat.ImageId);
                });

            descriptor.Field("parents")
                .Resolve<Parents?>((cx, ct) =>
                {
                    var childId = cx.Parent<Cat>().Id;
                    return GenealogicalTree.Source.FirstOrDefault(x => x.ChildId == childId);
                });
        }
    }

    public class Image
    {
        public string Large { get; set; }
        public string Small { get; set; }

        private string _imageBaseUrl = "http://somestorage.om/{0}/{1}.jpg";

        public Image(Guid pictureId)
        {
            Small = string.Format(_imageBaseUrl, "small", pictureId);
            Large = string.Format(_imageBaseUrl, "large", pictureId);
        }
    }
}