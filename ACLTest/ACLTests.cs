using ACL;
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

            for (var i = 0; i < count; i++)
            {
                var operation = _fixture.GetRandomOperation();
                var principal = _fixture.GetRandomPrincipal();
                var resource = _fixture.GetRandomResource();

                var result = _fixture.ACL.IsHierarchyGranted(principal, operation, resource);
            }
        }

        [Fact]
        public void IsGranted()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", "operation1", "resource1");
            Assert.True(acl.IsGranted("principal1", "operation1", "resource1"));
        }

        [Fact]
        public void Deny()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", "operation1", "resource1");
            acl.Deny("principal1", "operation1", "resource1");
            Assert.False(acl.IsGranted("principal1", "operation1", "resource1"));
        }

        [Fact]
        public void Grant()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", "operation1", "resource1");
            Assert.True(acl.IsGranted("principal1", "operation1", "resource1"));
        }

        [Fact]
        public void Revoke()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", "operation1", "resource1");
            acl.Revoke("principal1", "operation1", "resource1");
            Assert.False(acl.IsGranted("principal1", "operation1", "resource1"));
        }

        [Fact]
        public void IsHierarchyGranted1()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", "operation1", "r1");
            Assert.True(acl.IsHierarchyGranted("principal1", "operation1", "r1.r2.r3"));
        }

        [Fact]
        public void IsHierarchyGranted2()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", "operation1", "r1");
            acl.Deny("principal1", "operation1", "r1.r2");
            Assert.False(acl.IsHierarchyGranted("principal1", "operation1", "r1.r2.r3"));
        }

        [Fact]
        public void IsHierarchyGranted3()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", "operation1", "r1");
            acl.Deny("principal1", "operation1", "r1.r2");
            acl.Grant("principal1", "operation1", "r1.r2.r3");
            Assert.True(acl.IsHierarchyGranted("principal1", "operation1", "r1.r2.r3.r4"));
        }

        [Fact]
        public void IsHierarchyGrantedEnum1()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", Operation.Write, "r1");
            Assert.True(acl.IsHierarchyGranted("principal1", Operation.Write, "r1.r2.r3.r4"));
        }

        [Fact]
        public void IsHierarchyGrantedEnum2()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", Operation.Write, "r1");
            Assert.True(acl.IsHierarchyGranted("principal1", Operation.Read, "r1.r2.r3.r4"));
        }

        [Fact]
        public void IsHierarchyGrantedEnum3()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", Operation.Write, "r1");
            acl.Deny("principal1", Operation.Write, "r1.r2.r3");
            Assert.False(acl.IsHierarchyGranted("principal1", Operation.Write, "r1.r2.r3.r4"));
        }

        [Fact]
        public void IsHierarchyGrantedEnum4()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", Operation.Write, "r1");
            acl.Deny("principal1", Operation.Write, "r1.r2.r3");
            Assert.True(acl.IsHierarchyGranted("principal1", Operation.Read, "r1.r2.r3.r4"));
        }

        [Fact]
        public void IsHierarchyGrantedEnum5()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", Operation.Write, "r1");
            acl.Deny("principal1", Operation.Read, "r1.r2.r3");
            Assert.False(acl.IsHierarchyGranted("principal1", Operation.Read, "r1.r2.r3.r4"));
        }

        [Fact]
        public void IsHierarchyGrantedEnum6()
        {
            var acl = new AccessControlList();
            acl.Grant("principal1", Operation.Write, "r1");
            acl.Deny("principal1", Operation.Read, "r1.r2.r3");
            Assert.False(acl.IsHierarchyGranted("principal1", Operation.Write, "r1.r2.r3.r4"));
        }
    }
}
