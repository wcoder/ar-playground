using SceneKit;

namespace XamARKitSample.Nodes
{
    public class Model3DNode : SCNNode
    {
        public Model3DNode(string modelFileName)
        {
            var scene = SCNScene.FromFile(modelFileName);
            var node = scene.RootNode;

            AddChildNode(node);
        }
    }
}
