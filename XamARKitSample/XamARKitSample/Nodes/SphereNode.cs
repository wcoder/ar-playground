using SceneKit;
using UIKit;

namespace XamARKitSample.Nodes
{
    public class SphereNode : SCNNode
    {
        public SphereNode(float size, string fileName)
        {
            var node = new SCNNode
            {
                Geometry = CreateGeometry(size, fileName),
            };

            AddChildNode(node);
        }

        private SCNGeometry CreateGeometry(float size, string fileName)
        {
            var material = new SCNMaterial();
            var image = UIImage.FromBundle(fileName);
            material.Diffuse.Contents = image;
            terial.DoubleSided = true;

            var geometry = SCNSphere.Create(size);
            geometry.Materials = new[] { material };

            return geometry;
        }
    }
}
