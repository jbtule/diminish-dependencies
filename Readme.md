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

 - Create a postbuild script for `MyProgram` to copy/compress over your program and dependencies
   to your `MyProgram.Mininifed` project using `DiminishDependencies.Tool` ->

```
"$(SolutionDir)packages\DiminishDependencies.Tool.1.1.2\tools\DiminishDependencies.exe" -o "$(SolutionDir)DiminishDependencies.Diminished\Diminish\refs" "$(TargetDir)MyProgram.exe" "$(TargetDir)*.dll"
```
*in MonoDevelop it would be*
```
mono "${SolutionDir}/packages/DiminishDependencies.Tool.1.1.2/tools/DiminishDependencies.exe" -o "$(SolutionDir)/DiminishDependencies.Diminished/Diminish/refs" "$(TargetDir)/MyProgram.exe" "$(TargetDir)/*.dll"
```

 - Build it so the postbuild script runs

 - In `MyProgram.Minified` you have a `Diminish/refs` folder, `add existing` and set to `EmbeddedResource` each of your
   newly copied and compressed dependencies, *ie.* `MyProgram.exe.dep-lzma, MyDll.dll.dep-lzma, etc.`

 - In `MyProgram.Minified.Program.Main()` you can use the the convenince method for a latebound call/resolver setup *also don't forget to make sure the main function is `public` in your `MyProgram` project.*

```
 public class Program
    {
        public static int Main(string[] args)
        {
			return Diminish.Main<int>.Call<string[]>(assemblyName:"MyProgram", typeName:"MyProgram.Program")(args);
         }
    }
```

 - Now any subsequent build will produce and up to date compressed and runnable exe.

###Alternatively###

you could try:

 - Adding the resolve code to static constructor to MyProgram.Minified.Program (although you may need to nest calls that need to the resolver just to make sure it doesn't try too early.

```
     static Program()
        {
            Diminish.Setup.Resolver();
        }
```

   * or using a module initializer with [InjectModuleInitializer](http://einaregilsson.com/module-initializers-in-csharp/)


