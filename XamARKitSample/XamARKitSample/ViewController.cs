using ARKit;
using System;
using UIKit;

namespace XamARKitSample
{
    public partial class ViewController : UIViewController
    {
        private ARSCNView _sceneView;

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
