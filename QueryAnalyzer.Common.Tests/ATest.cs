using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QueryAnalyzer.Common.Tests
{
    public class ATest
    {
        [AssemblyInitialize()]
        public static void AssemblyInitialize(TestContext context)
        {
            var boostrap = new BootStrapper();
        }
    }
}
