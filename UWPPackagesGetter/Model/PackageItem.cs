using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPPackagesGetter.Model
{
    public sealed partial class PackageItem
    {
        public AppListEntry PackageEntry { get; set; }
        public BitmapImage PackageLogo { get; set; }
    }
}
