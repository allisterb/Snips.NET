using System;
using Xunit;

namespace SnipsNLU
{
    public class SnipsApiTests
    {
        [Fact]
        public void CanCreateEngine()
        {
            Assert.True(SnipsApi.CreateEngineFromDir(@"C:\Projects\Snips.NET\ext\snips-nlu-rs\data\tests\models\nlu_engine", 
                out IntPtr enginePtr));
        }

        [Fact]
        public void CanGetEngineModel()
        {
            Assert.True(SnipsApi.CreateEngineFromDir(@"C:\Projects\Snips.NET\ext\snips-nlu-rs\data\tests\models\nlu_engine",
                out IntPtr enginePtr));

            Assert.True(SnipsApi.GetModelVersion(out string version));
            Assert.NotEmpty(version);
        }
    }
}
