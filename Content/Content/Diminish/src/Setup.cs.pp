//Public Domain
//AssemblyResolve Concept from http://blogs.msdn.com/b/microsoft_press/archive/2010/02/03/jeffrey-richter-excerpt-2-from-clr-via-c-third-edition.aspx
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using $rootnamespace$.Diminish.SevenZip;

namespace $rootnamespace$.Diminish
{
        private static readonly IDictionary<string, Assembly> _loaded =  new Dictionary<string, Assembly>();
        public static Assembly AssemblyLoad(string asssemblyName)
        {
            var shortName = new AssemblyName(asssemblyName).Name;
            if (!_loaded.ContainsKey(shortName))
            {
                var resourceName = String.Format("DiminishDependencies.Diminished.Diminish.refs.{0}.dep-lzma", shortName);
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
        
                    if (stream == null)
                        throw new Exception(string.Format(
                            "Missing embedded dependency {0} was looking for resource {1}", asssemblyName, resourceName));
                    using (var memStream = new MemoryStream())
                    {
                        Zipper.Decode(stream, memStream);
                        var assembly = Assembly.Load(memStream.ToArray());
                        _loaded.Add(shortName, assembly);
                    }
                }
            }
            return _loaded[shortName];
        }

        public static void Resolver()
        {
		    AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => AssemblyLoad(args.Name);
        }
}
