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
            rh.Add("resource1", "operation1", "principal1");

            Assert.Equal(1, rh.ResourceCount);
        }

        [Fact]
        public void Remove()
        {
            var rh = new ResourceHolder();
            rh.Add("resource1", "operation1", "principal1");
            rh.Remove("resource1", "operation1", "principal1");

            Assert.Equal(0, rh.ResourceCount);
        }

        [Fact]
        public void Contains()
        {
            var rh = new ResourceHolder();
            rh.Add("resource1", "operation1", "principal1");

            Assert.True(rh.Contains("resource1", "operation1", "principal1"));
        }
    }
}
