## DiminishDependencies ##
http://github.com/jbtule/diminish-dependencies

`DiminishDependencies`  is *Public Domain* set of boot straping code to include compressed assemblies 
(including your original executable) into single unified shell application that decompresseses and runs its.
It uses public domain `LZMA` code from [7zip](http://www.7-zip.org/sdk.html) to compress
and thus gets very high compression ratios.

The suggested way to use this:

 - Install nuget package `DiminishDependencies` into a **NEW** console application `MyProgram.Minified`
   (or `MyProgram.Diminished` or `MyProgram.Compressed` etc...) of your solution.
 - Install nuget package `DiminishDependencies.Tool` into your solution
 - Add Reference... in `MyProgram.Minified` referencing your `MyProgram` project, so `Copy Local` 
   to `false` in `Properties` of reference.

 - Create a prebuild script for `MyProgram.Minified` to copy/compress over your program and dependencies
   from your `MyProgram` project using `DiminishDependencies.Tool` ->

```
DiminishDependencies.exe -o "$(ProjectDir)Diminish\refs" "$(SolutionDir)MyProgram\$(OutDir)MyProgram.exe"
DiminishDependencies.exe -o "$(ProjectDir)Diminish\refs" "$(SolutionDir)MyProgram\$(OutDir)*dll"]
```

> **Troubleshooting tip**, nuget for whatever reason doesn't always keep the the path for DiminishedDependencies.exe
> so using the full path in the prebuild script might make things easier even if you have to change every time you updated
> DiminishedDendencies from nuget, but you probably won't ever need to update DiminishedDependecies since it's a small bit
> of public domain source code and tool.

 - Build it so the prebuild script runs

 - In `MyProgram.Minified` you have a `Diminish/refs` folder, `add existing` and set to `EmbeddedResource` each of your
   newly copied and compressed dependencies, eg. `MyProgram.dep-lzma`

 - In `MyProgram.Minified.Program.Main()` call your `MyProgram.Program.Main()` --*don't forget to make sure the main
   function is `public` in your `MyProgram` project.*

 - Add static constructor to MyProgram.Minified.Program

```
     static Program()
        {
            Diminish.Setup.Resolver();
        }
```

 - Now any subsequent build will produce and up to date compressed and runnable exe.
