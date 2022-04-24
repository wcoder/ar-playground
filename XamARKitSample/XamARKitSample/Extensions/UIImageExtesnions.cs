using Foundation;
using UIKit;

namespace XamARKitSample.Extensions
{
    public static class UIImageExtesnions
    {
        public static UIImage LoadFromUrl(string imageUrl)
        {
            using var nsUrl = new NSUrl(imageUrl);
            using var imageData = NSData.FromUrl(nsUrl);

            return UIImage.LoadFromData(imageData);
        }
    }
}
