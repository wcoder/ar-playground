using SceneKit;
using UIKit;
using XamARKitSample.Extensions;

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
                ? UIImageExtesnions.LoadFromUrl(imageUrl)
                : UIImage.FromFile(imageUrl);

            var material = new SCNMaterial();
            material.Diffuse.Contents = image;
            material.DoubleSided = true;

            var geometry = SCNPlane.Create(width, height);
            geometry.Materials = new[] { material };

            return geometry;
        }
    }
}
