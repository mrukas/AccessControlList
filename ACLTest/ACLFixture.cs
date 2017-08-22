using System;
using System.Linq;
using ACL;

namespace ACLTest
{
    public class ACLFixture
    {
        private Random _random = new Random();

        public AccessControlList ACL;
        private readonly string[] _resources;
        private readonly string[] _principals;
        private readonly string[] _operations;

        public ACLFixture()
        {
            ACL = new AccessControlList();

            _resources = GenerateRandomStringArray(1000);
            _principals = GenerateRandomStringArray(100);
            _operations = GenerateRandomStringArray(100);

            foreach (var resource in _resources)
            {
                foreach (var principal in _principals)
                {
                    foreach (var operation in _operations)
                    {
                        if (_random.Next(2) > 0)
                        {
                            ACL.Grant(principal, operation, resource);
                        }
                        else
                        {
                            ACL.Deny(principal, operation, resource);
                        }
                    }
                }
            }
        }

        public string GetRandomOperation()
        {
            var randIdx = _random.Next(_operations.Length);

            return _operations[randIdx];
        }

        private string[] GenerateRandomStringArray(int number)
        {
            var result = new string[number];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = GenerateRandomString(10);
            }

            return result;
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public string GetRandomResource()
        {
            var randIdx = _random.Next(_resources.Length);

            return _resources[randIdx];
        }

        public string GetRandomPrincipal()
        {
            var randIdx = _random.Next(_principals.Length);

            return _principals[randIdx];
        }
    }
}
