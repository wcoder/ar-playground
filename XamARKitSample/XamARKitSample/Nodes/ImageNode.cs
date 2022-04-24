using Foundation;
using SceneKit;
using UIKit;

namespace XamARKitSample.Nodes
{
    public class ImageNode : SCNNode
    {
        public ImageNode(string imageUrl, float width, float height)
        {
            var node = new SCNNode
            {
                Geometry = CreateGeometry(imageUrl, width, height),
            };
            AddChildNode(node);
        }

        private SCNGeometry CreateGeometry(string imageUrl, float width, float height)
        {
            var image = imageUrl.StartsWith("http")
                ? FromUrl(imageUrl)
                : UIImage.FromFile(imageUrl);

            var material = new SCNMaterial();
            material.Diffuse.Contents = image;
            material.DoubleSided = true;

            var geometry = SCNPlane.Create(width, height);
            geometry.Materials = new[] { material };

            return geometry;
        }

        private static UIImage FromUrl(string resource)
        {
            using var url = new NSUrl(resource);
            using var imageData = NSData.FromUrl(url);

            return UIImage.LoadFromData(imageData);
        }
    }
}
