using System;
using SceneKit;

namespace XamARKitSample.Extensions
{
    public static class SCNNodeExtensions
    {
        const string RotationActionKey = "rotation";

        public static void AddRotationAction(
            this SCNNode node,
            SCNActionTimingMode timingMode,
            double seconds,
            bool loop = false)
        {

            var rotateAction = SCNAction.RotateBy(0, (float)Math.PI, 0, seconds);
            rotateAction.TimingMode = timingMode;

            if (loop)
            {
                var indefiniteRotation = SCNAction.RepeatActionForever(rotateAction);
                node.RunAction(indefiniteRotation, RotationActionKey);
            }
            else
            {
                node.RunAction(rotateAction, RotationActionKey);
            }
        }

        public static void RemoveRotationAction(this SCNNode node)
        {
            node.RemoveAction(RotationActionKey);
        }
    }
}
