using System.Diagnostics;
using Xunit;

namespace ACLTest
{
    public class ACLTests : IClassFixture<ACLFixture>
    {
        private readonly ACLFixture _fixture;

        public ACLTests(ACLFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void TestSpeed()
        {

            var count = 1000;

            var sw = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var operation = _fixture.GetRandomOperation();
                var principal = _fixture.GetRandomPrincipal();
                var resource = _fixture.GetRandomResource();

                var result = _fixture.ACL.IsGranted(principal, operation, resource);

            }
            sw.Stop();
        }
    }
}
