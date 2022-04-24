using System;
using SceneKit;
using UIKit;

namespace XamARKitSample.Nodes
{
    public class PlaneNode : SCNNode
    {
        public PlaneNode(nfloat width, nfloat length, UIColor color)
        {
            var node = new SCNNode
            {
                Geometry = CreateGeometry(width, length, color),
                Position = new SCNVector3(0, 0, 0),
                Opacity = 0.5f,
            };

            AddChildNode(node);
        }

        private SCNGeometry CreateGeometry(nfloat width, nfloat length, UIColor color)
        {
            var material = new SCNMaterial();
            material.Diffuse.Contents = color;
            material.DoubleSided = false;

            var geometry = SCNPlane.Create(width, length);
            geometry.Materials = new[] { material };

            return geometry;
        }
    }
}
