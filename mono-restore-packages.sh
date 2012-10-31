#!/bin/sh
mkdir packages
cd packages
mono --runtime=v4.0 ../.nuget/NuGet.exe install ../.nuget/packages.config
mono --runtime=v4.0 ../.nuget/NuGet.exe install ../DiminishDependencies/packages.config
mono --runtime=v4.0 ../.nuget/NuGet.exe install ../DiminishDependencies.Diminished/packages.config
