using ARKit;
using SceneKit;
using System;
using System.Linq;
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

            var tapGestureRecognizer = new UITapGestureRecognizer(OnTapGesture);
            _sceneView.AddGestureRecognizer(tapGestureRecognizer);

            var pinchGestureRecognizer = new UIPinchGestureRecognizer(OnPinchGesture);
            _sceneView.AddGestureRecognizer(pinchGestureRecognizer);
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

        private void OnTapGesture(UITapGestureRecognizer gestureRecognizer)
        {
            var hit = FindHit(gestureRecognizer);

            hit?.Node?.RemoveFromParentNode();
        }

        private void OnPinchGesture(UIPinchGestureRecognizer gestureRecognizer)
        {
            var hit = FindHit(gestureRecognizer);

            if (hit != null)
            {
                var node = hit.Node;

                var scaleX = (float)gestureRecognizer.Scale * node.Scale.X;
                var scaleY = (float)gestureRecognizer.Scale * node.Scale.Y;

                node.Scale = new SCNVector3(scaleX, scaleY, zPosition);
                gestureRecognizer.Scale = 1; // reset
            }
        }

        private SCNHitTestResult FindHit(UIGestureRecognizer gestureRecognizer)
        {
            var area = gestureRecognizer.View as SCNView;
            var point = gestureRecognizer.LocationInView(area);
            var hits = area.HitTest(point, new SCNHitTestOptions());
            var hit = hits.FirstOrDefault();
            return hit;
        }
    }
}
