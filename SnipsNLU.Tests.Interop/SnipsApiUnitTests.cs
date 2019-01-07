using System;
using Xunit;

using SnipsNLU;
namespace SnipsNLU.Tests
{
    public class SnipsApiUnitTests
    {
        [Fact]
        public void CanCreateEngine()
        {
            Assert.True(SnipsApi.CreateEngineFromDir("foo"));
        }
    }
}
