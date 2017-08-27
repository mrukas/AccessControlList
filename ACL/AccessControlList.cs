using System;
using System.Linq;

namespace ACL
{
    public class AccessControlList : IAccessControlList
    {
        protected readonly ResourceHolder Granted = new ResourceHolder();
        protected readonly ResourceHolder Denied = new ResourceHolder();

        private readonly string _hierarchySeparator;

        public AccessControlList(string hierarchySeparator = ".")
        {
            _hierarchySeparator = hierarchySeparator;
        }

        public void Grant(string principal, string operation, string resource)
        {
            Granted.Add(resource, operation, principal);
        }

        public void Revoke(string principal, string operation, string resource)
        {
            Granted.Remove(resource, operation, principal);
        }

        public void Deny(string principal, string operation, string resource)
        {
            Denied.Add(resource, operation, principal);
        }

        public bool IsGranted(string[] principals, string operation, string resource)
        {
            return principals.Any(x => IsGranted(x, operation, resource));
        }

        public bool IsGranted(string principal, string operation, string resource)
        {
            if (Denied.Contains(resource, operation, principal))
            {
                return false;
            }

            if (Granted.Contains(resource, operation, principal))
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
            var isDenied = Denied.Contains(resource, operation, principal);

            if (isDenied)
            {
                return false;
            }

            var granted = Granted.Contains(resource, operation, principal);

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

        protected string RemoveHierarchySegment(string resource)
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
