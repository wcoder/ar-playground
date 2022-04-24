using ARKit;
using SceneKit;
using System;
using UIKit;
using XamARKitSample.Nodes;

namespace XamARKitSample
{
    public partial class ViewController : UIViewController
    {
        private ARSCNView _sceneView;

        const float zPosition = -0.25f;

        public ViewController(IntPtr handle) : base(handle)
        {
            _sceneView = new ARSCNView
            {
                AutoenablesDefaultLighting = true,
                ShowsStatistics = true,
            };
            View.AddSubview(_sceneView);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _sceneView.Frame = View.Frame;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            _sceneView.Session.Run(new ARWorldTrackingConfiguration
            {
                AutoFocusEnabled = true,
                LightEstimationEnabled = true,
                WorldAlignment = ARWorldAlignment.Gravity
            }, ARSessionRunOptions.ResetTracking | ARSessionRunOptions.RemoveExistingAnchors);

            var imageUrl = "https://raw.githubusercontent.com/dotnet/brand/main/logo/dotnet-logo.png";
            var imageNode = new ImageNode(imageUrl, 0.1f, 0.1f)
            {
                Position = new SCNVector3(0, 0, zPosition)
            };

            _sceneView.Scene.RootNode.AddChildNode(imageNode);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            _sceneView.Session.Pause();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}
