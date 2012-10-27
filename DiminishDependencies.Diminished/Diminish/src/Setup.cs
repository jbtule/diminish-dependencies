//Public Domain
//AssemblyResolve Concept from http://blogs.msdn.com/b/microsoft_press/archive/2010/02/03/jeffrey-richter-excerpt-2-from-clr-via-c-third-edition.aspx
using System;
using System.IO;
using System.Reflection;
using DiminishDependencies.Diminished.Diminish.SevenZip;

namespace DiminishDependencies.Diminished.Diminish
{
    public static class Setup
    {
        public static void Resolver()
		{
		    AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                var resourceName = String.Format("DiminishDependencies.Diminished.Diminish.refs.{0}.dep-lzma",new AssemblyName(args.Name).Name);
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {  
					if(stream == null)
                        throw new Exception(string.Format("Missing embedded dependency {0} was looking for resource {1}", args.Name, resourceName));
                    using(var memStream =new MemoryStream())
                    {
                        Zipper.Decode(stream, memStream);
                        return Assembly.Load(memStream.ToArray());
                    }
                }
            };
        }
    }
}
