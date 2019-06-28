using dp_record.adapters;
using Xunit;

namespace dp_record.tests
{
    public class CLI_tests
    {
        [Fact]
        public void No_args()
        {
            var sut = new CLI(new string[0]);
            Assert.Equal(60, sut.IntervalSeconds);
            Assert.True(sut.TakeScreenshots);
        }
        
        [Fact]
        public void InteravalSeconds_given()
        {
            var sut = new CLI(new[]{"120"});
            Assert.Equal(120, sut.IntervalSeconds);
            Assert.True(sut.TakeScreenshots);
        }
        
        [Fact]
        public void InteravalSeconds_not_as_first_param()
        {
            var sut = new CLI(new[]{"xxx", "120"});
            Assert.Equal(120, sut.IntervalSeconds);
        }
        
        [Fact]
        public void No_screenshots_requested()
        {
            var sut = new CLI(new[]{"120", "-noscreenshots"});
            Assert.Equal(120, sut.IntervalSeconds);
            Assert.False(sut.TakeScreenshots);
        }
        
        [Fact]
        public void Option_syntax_variants()
        {
            var sut = new CLI(new[]{"-noscreenshots"});
            Assert.False(sut.TakeScreenshots);
            
            sut = new CLI(new[]{"--noscreenshots"});
            Assert.False(sut.TakeScreenshots);
            
            sut = new CLI(new[]{"/noscreenshots"});
            Assert.False(sut.TakeScreenshots);
        }
    }
}