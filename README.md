# UWPPackagesGetter
Windows Runtime Component that gets all installed apps from your users Windows 10 Device. (Win32 Programs are not included). 

Note: This uses restricted capabilites so only Business accounts can upload apps using this Runtime Component to the Store. However, anyone can still use this if they are sideloading the app.

## Before you start, do this first:

1. Add the `rescap` namespace to the `IgnorableNamespaces` section of your Package.appxmanifest file. So it looks something like this:
``` xml
<Package
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  xmlns:mp="http://schemas.microsoft.com/appx/2014/phone/manifest"
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"
  IgnorableNamespaces="uap mp rescap">
  ....
</Package>
```

2. Then in the capabilities tags, add the "packageQuery" and "packageManagement" restricted capabilites:
``` xml
 <Capabilities>
    <Capability Name="internetClient" />
    <rescap:Capability Name="packageManagement"/>
    <rescap:Capability Name="pacakgeQuery"/>
  </Capabilities>
```
Now the classes used will all work without permission exceptions :)

## Installation:

Open the Nuget Package Manager console and enter this command:
`Install-Package UWPPackgesGetter.ColinKiama`

or... In a a Universal Windows Project, go to Tools > NuGet Package Manager > Manage NuGet Packages for Solution then on the "Browse" section, search for "UWPPackgesGetter.ColinKiama" then, you can install the package for each project you want to use in your current solution.


## How to use:

``` C#
 // Quick Example
 ObservableCollection<PackageItem> MyPackages = new ObservableCollection<PackageItem>();
 MyPackages = PackageGetter.GetAllPackagesAsync();
```
