using System;
using System.Linq;
using CatteryRegister.DataContext;
using HotChocolate.Types;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;

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