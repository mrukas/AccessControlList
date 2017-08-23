using System;
using System.Linq;

namespace ACL
{
    public class AccessControlList : IAccessControlList
    {
        private readonly ResourceHolder _granted = new ResourceHolder();
        private readonly ResourceHolder _denied = new ResourceHolder();

        private readonly string _hierarchySeparator;

        public AccessControlList(string hierarchySeparator = ".")
        {
            _hierarchySeparator = hierarchySeparator;
        }

        public void Grant(string principal, string operation, string resource)
        {
            _granted.Add(resource, operation, principal);
        }

        public void Grant(string principal, Operation operation, string resource)
        {
            Grant(principal, operation.ToString().ToLowerInvariant(), resource);
        }

        public void Revoke(string principal, string operation, string resource)
        {
            _granted.Remove(resource, operation, principal);
        }

        public void Revoke(string principal, Operation operation, string resource)
        {
            Revoke(principal, operation.ToString().ToLowerInvariant(), resource);
        }

        public void Deny(string principal, string operation, string resource)
        {
            _denied.Add(resource, operation, principal);
        }

        public bool IsGranted(string[] principals, string operation, string resource)
        {
            return principals.Any(x => IsGranted(x, operation, resource));
        }

        public void Deny(string principal, Operation operation, string resource)
        {
            Deny(principal, operation.ToString().ToLowerInvariant(), resource);
        }

        public bool IsGranted(string principal, string operation, string resource)
        {
            if (_denied.Contains(resource, operation, principal))
            {
                return false;
            }

            if (_granted.Contains(resource, operation, principal))
            {
                return true;
            }

            return false;
        }

        public bool IsHierarchyGranted(string[] principals, string operation, string resource)
        {
            return principals.Any(x => IsHierarchyGranted(x, operation, resource));
        }

        public bool IsHierarchyGranted(string principal, string operation, string resource)
        {
            var isDenied = _denied.Contains(resource, operation, principal);

            if (isDenied)
            {
                return false;
            }

            var granted = _granted.Contains(resource, operation, principal);

            if (granted)
            {
                return true;
            }

            var removedHierarchy = RemoveHierarchySegment(resource);

            if (removedHierarchy != resource)
            {
                return IsHierarchyGranted(principal, operation, removedHierarchy);
            }

            return false;
        }

        public bool IsHierarchyGranted(string principal, Operation operation, string resource)
        {
            var isGranted = IsHierarchyGranted(principal, operation.ToString().ToLowerInvariant(), resource);

            if (isGranted)
            {
                return true;
            }

            if (operation == Operation.Read)
            {
                return IsHierarchyGranted(principal, Operation.Write, resource);
            }

            return false;
        }

        public bool IsGranted(string principal, Operation operation, string resource)
        {
            return IsGranted(principal, operation.ToString().ToLowerInvariant(), resource);
        }

        private string RemoveHierarchySegment(string resource)
        {
            var separatorIndex = resource.LastIndexOf(_hierarchySeparator, StringComparison.Ordinal);

            if (separatorIndex != -1)
            {
                return resource.Substring(0, separatorIndex);
            }

            return resource;
        }
    }
}
