using ACL;
using Xunit;

namespace ACLTest
{
    public class ResourceHolderTest
    {
        [Fact]
        public void Add()
        {
            var rh = new ResourceHolder();
            rh.Add("operation1", "principal1");

            Assert.Equal(1, rh.OperationCount);
        }

        [Fact]
        public void Remove()
        {
            var rh = new ResourceHolder();
            rh.Add("operation1", "principal1");
            rh.Remove("operation1", "principal1");

            Assert.Equal(0, rh.OperationCount);
        }

        [Fact]
        public void ContainsPrincipal()
        {
            var rh = new ResourceHolder();
            rh.Add("operation1", "principal1");

            Assert.True(rh.ContainsPrincipal("operation1", "principal1"));
        }
    }
}
