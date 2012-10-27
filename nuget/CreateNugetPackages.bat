@echo off
copy ..\Readme.md ..\Readme.txt
..\.nuget\nuget.exe pack DiminishDependencies.nuspec
..\.nuget\nuget.exe pack DiminishDependencies.Tool.nuspec