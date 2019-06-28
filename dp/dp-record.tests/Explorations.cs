using System;
using System.Diagnostics;
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
    }
}