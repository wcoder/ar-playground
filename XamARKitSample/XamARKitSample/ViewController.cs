using ARKit;
using SceneKit;
using System;
using System.Linq;
using UIKit;
using XamARKitSample.Extensions;
using XamARKitSample.Nodes;

namespace XamARKitSample
{
    public partial class ViewController : UIViewController
    {
        const float zPosition = -0.25f;

        private ARSCNView _sceneView;
        private bool _isAnimating;

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

            // add image
            var imageUrl = "https://raw.githubusercontent.com/dotnet/brand/main/logo/dotnet-logo.png";
            var imageNode = new ImageNode(imageUrl, 0.1f, 0.1f)
            {
                Position = new SCNVector3(0.5f, 0.5f, zPosition)
            };
            _sceneView.Scene.RootNode.AddChildNode(imageNode);

            // add cude
            var cubeNode = new CubeNode(0.1f, UIColor.Blue)
            {
                Position = new SCNVector3(0, 0, zPosition * 2)
            };
            _sceneView.Scene.RootNode.AddChildNode(cubeNode);

            // add sphere
            var sphereNode = new SphereNode(0.1f, "world-map.jpg")
            {
                Position = new SCNVector3(0.25f, 0.25f, zPosition * 2)
            };
            _sceneView.Scene.RootNode.AddChildNode(sphereNode);

            // add gestures
            var tapGestureRecognizer = new UITapGestureRecognizer(OnTapGesture);
            _sceneView.AddGestureRecognizer(tapGestureRecognizer);

            var pinchGestureRecognizer = new UIPinchGestureRecognizer(OnPinchGesture);
            _sceneView.AddGestureRecognizer(pinchGestureRecognizer);

            var rotationGestureRecognizer = new UIRotationGestureRecognizer(OnRotationGesture);
            _sceneView.AddGestureRecognizer(rotationGestureRecognizer);
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
            if (hit != null)
            {
                var node = hit.Node;
                if (node != null)
                {
                    //hit.Node.RemoveFromParentNode();

                    if (_isAnimating)
                    {
                        node.RemoveRotationAction();
                        _isAnimating = false;
                    }
                    else
                    {
                        node.AddRotationAction(SCNActionTimingMode.Linear, 3, true);
                        _isAnimating = true;
                    }
                }
            }
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

        float _zAngle;
        private void OnRotationGesture(UIRotationGestureRecognizer gestureRecognizer)
        {
            var hit = FindHit(gestureRecognizer);
            if (hit != null)
            {
                var node = hit.Node;
                if (node != null)
                {
                    _zAngle += (float)-gestureRecognizer.Rotation;
                    node.EulerAngles = new SCNVector3(node.EulerAngles.X, node.EulerAngles.Y, _zAngle);
                }
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
