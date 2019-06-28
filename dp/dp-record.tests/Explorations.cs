using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Xunit;
using Xunit.Abstractions;

namespace dp_record.tests
{
    public class Explorations
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Explorations(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Location()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _testOutputHelper.WriteLine($"{path}");
        }
    }
}