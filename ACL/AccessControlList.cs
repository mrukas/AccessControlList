namespace ACL
{
    public class AccessControlList : IAccessControlList
    {
        private ResourceHolder _granted = new ResourceHolder();
        private ResourceHolder _denied = new ResourceHolder();

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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public bool IsHierarchyGranted(string principal, string operation, string resource)
        {
            throw new System.NotImplementedException();
        }

        public bool IsGranted(string principal, Operation operation, string resource)
        {
            return IsGranted(principal, operation.ToString().ToLowerInvariant(), resource);
        }
    }
}
