\\# UWPPackagesGetter
Windows Runtime Component that gets all installed apps from your users Windows 10 Device. (Win32 Programs are not included). 

Note: This used restricted capabilites so only Business accounts can upload apps using this Runtime Component to the Store. However, anyone can still use this if they are sideloading the app.

## Before you start, do this first:

1. Add the rescap namespace to your Package.appxmanifest file. So it looks something like this:
```
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
```
 <Capabilities>
    <Capability Name="internetClient" />
    <rescap:Capability Name="packageManagement"/>
    <rescap:Capability Name="pacakgeQuery"/>
  </Capabilities>
```

