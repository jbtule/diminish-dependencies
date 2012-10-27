using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiminishDependencies.Diminished
{
    public class Program
    {
        static Program()
        {
            Diminish.Setup.Resolver();
        }

        public static int Main(string[] args)
        {
            
            return  DiminishDependencies.Program.Main(args);
        }
    }
}
