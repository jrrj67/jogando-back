using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Utils
{
    public class MockHttpContextConfig
    {
        public static string Scheme { get => "https"; }
        public static string Host { get => "localhost"; }
        public static string Path { get => "/api/roles"; }
    }
}
