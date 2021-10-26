using System;
using CatteryRegister.DataContext;
using CatteryRegister.Services;
using HotChocolate.Types;

namespace CatteryRegister.Model
{
    public class CatType : ObjectType<Cat>
    {
        protected override void Configure(IObjectTypeDescriptor<Cat> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field("image")
                .Resolve(async (cx, ct) =>
                {
                    var parentCat = cx.Parent<Cat>();
                    var randomCatPicture = cx.Service<RandomCatImageClient>();
                    //return new Image(parentCat.ImageId);
                    var picture = await randomCatPicture.Search();
                    return new Image
                    {
                        Large = picture.url,
                        Small = picture.url
                    };
                });

            descriptor.Field("parents")
                .Resolve<Parents?>((cx, ct) =>
                {
                    var childId = cx.Parent<Cat>().Id;
                    var parentsService = cx.Service<ParentsService>();
                    return parentsService.GetParents(childId);
                });

            descriptor.Field("parentsWithLoader")
                .Resolve<Parents?>(async (cx, ct) =>
                {
                    var childId = cx.Parent<Cat>().Id;
                    var parentsService = cx.Service<ParentsDataLoader>();
                    return await parentsService.LoadAsync(childId, ct);
                });
        }
    }

    public class Image
    {
        public Image()
        {
        }

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