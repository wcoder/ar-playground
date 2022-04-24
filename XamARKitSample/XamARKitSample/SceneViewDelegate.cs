using System;
using ARKit;
using SceneKit;
using UIKit;
using XamARKitSample.Nodes;

namespace XamARKitSample
{
    public class SceneViewDelegate : ARSCNViewDelegate
    {
        public override void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor is ARImageAnchor imageAnchor)
            {
                var image = imageAnchor.ReferenceImage;
                var width = image.PhysicalSize.Width;
                var height = image.PhysicalSize.Height;

                var planeNode = new PlaneNode(width, height, UIColor.Red);
                var angle = (float)(-Math.PI / 2);

                planeNode.EulerAngles = new SCNVector3(angle, 0, 0);

                node.AddChildNode(planeNode);
            }
        }
    }
}
