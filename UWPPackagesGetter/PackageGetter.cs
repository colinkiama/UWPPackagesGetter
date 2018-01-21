using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UWPPackagesGetter.Model;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Management.Deployment;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPPackagesGetter
{
    public sealed class PackageGetter
    {
        private static PackageManager PkgManager = new PackageManager();

        /// <summary>
        /// Event occurs when packages have been retrieved with ther app icons.
        /// </summary>


        #region Functions

        /// <summary>
        /// Gets app package and image and returns them as a new "PackageItem" asynchronously, which will then be used for the to display app icons and data useful for app launchers .
        /// </summary>
        public static IAsyncOperation<IEnumerable<PackageItem>> GetAllAppsAsync()
        {

            return AsyncInfo.Run<IEnumerable<PackageItem>>((token) =>

            Task.Run<IEnumerable<PackageItem>>(async () =>
            {
                var ListOfInstalledPackages = PkgManager.FindPackagesForUserWithPackageTypes("", PackageTypes.Main);
                List<Package> AllPackages = new List<Package>();
                List<Package> Packages = new List<Package>();
                AllPackages = ListOfInstalledPackages.ToList();
                int Count = AllPackages.Count();

                for (int i = 0; i < Count; i++)
                {
                    Packages.Add(AllPackages[i]);
                }


                ObservableCollection<PackageItem> PackageItems = new ObservableCollection<PackageItem>();
                Count = Packages.Count();
                for (int i = 0; i < Count; i++)
                {
                    try
                    {
                        List<AppListEntry> SingleAppListEntries = new List<AppListEntry>();
                        var AppListEntries = await Packages[i].GetAppListEntriesAsync();
                        SingleAppListEntries = AppListEntries.ToList();
                        if (SingleAppListEntries.Count > 0)
                        {
                            Debug.WriteLine("YES!");
                        }
                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                        {
                            try
                            {
                                BitmapImage Logo = new BitmapImage();

                                var LogoStream = SingleAppListEntries[0].DisplayInfo.GetLogo(new Size(50, 50));

                                IRandomAccessStreamWithContentType LogoStreamSource = await LogoStream.OpenReadAsync();

                                Logo.SetSource(LogoStreamSource);


                                PackageItem ItemToAdd = new PackageItem();

                                ItemToAdd.PackageEntry = SingleAppListEntries[0];

                                ItemToAdd.PackageLogo = Logo;

                                PackageItems.Add(ItemToAdd);
                            }

                            catch (Exception e)
                            {
                                Debug.WriteLine(e.Message);
                            }
                        });
                    }

                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }

                }

                PackagesRetrieved?.Invoke(null, new PackageEventArgs(true));
                return PackageItems;
            }, token)
            );

        }
        #endregion

        #region Events
        public static event EventHandler<PackageEventArgs> PackagesRetrieved;
        #endregion
    }

    public sealed class PackageEventArgs
    {
        public PackageEventArgs(bool arePackagesRetrieved)
        {
            packagesRetrieved = arePackagesRetrieved;
        }
        private bool packagesRetrieved;

        public bool PackagesRetrieved
        {
            get { return packagesRetrieved; }
            set { packagesRetrieved = value; }
        }

    }
}
