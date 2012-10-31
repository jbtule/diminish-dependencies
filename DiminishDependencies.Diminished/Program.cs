using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DiminishDependencies.Diminished
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return Diminish.Main<int>.Call<string[]>(assemblyName:"DiminishDependencies", typeName:"DiminishDependencies.Program")(args);
        }
    }
}
