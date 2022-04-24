using SceneKit;
using UIKit;

namespace XamARKitSample.Nodes
{
    public class CubeNode : SCNNode
    {
        public CubeNode(float size, UIColor color)
        {
            var material = new SCNMaterial();
            material.Diffuse.Contents = color;

            var geometry = SCNBox.Create(size, size, size, 0);
            geometry.Materials = new[] { material };

            var node = new SCNNode
            {
                Geometry = geometry,
            };

            AddChildNode(node);
        }
    }
}
