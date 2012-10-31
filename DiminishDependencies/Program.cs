using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DiminishDependencies.Diminish.SevenZip;
using NDesk.Options;

namespace DiminishDependencies
{
    public class Program
    {
      
        public static int Main(string[] args)
        {
            try
            {
                var outputdir = ".";
                var p = new OptionSet()
                            {
                                {"o|output=", v => outputdir = v}
                            };
                var extra = p.Parse(args);


                if(!extra.Any())
                    Console.WriteLine("Usage: DiminishDependencies.exe [-o outputdir] file1 [file2 ...]");
                foreach (var file in extra)
                {
                    var dir =Path.GetDirectoryName(file) ?? ".";
                    var name = Path.GetFileName(file);

                    var filelist = Directory.GetFiles(dir, name);
                    if (!filelist.Any())
                    {
                        throw new IOException("Could Not Find File:" + file);
                    }

                    foreach(var expandedFile in Directory.GetFiles(dir, name))
                    {
                        var baseFileName = Path.GetFileName(expandedFile);

                        string outpath = baseFileName + ".dep-lzma";
                        using (
                            var output =
                                File.Open(Path.Combine(outputdir, outpath),
                                          FileMode.Create, FileAccess.Write))
                        using (var input = File.OpenRead(expandedFile))
                        {
                            Console.WriteLine("DiminishDependencies: Compressing {0} to {1}", Path.GetFileName(expandedFile), outpath);
                            Zipper.Encode(input, output);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    Console.WriteLine(e.InnerException.Message);
                    Console.WriteLine(e.InnerException.StackTrace);
                }
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return -1;
            }
            return 0;
        }
    }
}
