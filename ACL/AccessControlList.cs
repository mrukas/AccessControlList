namespace ACL
{
    public class AccessControlList : IAccessControlList
    {
        private OperationHolder _granted = new OperationHolder();
        private OperationHolder _denied = new OperationHolder();

        public void Grant(string principal, Operation operation, string resource)
        {
            _granted.Add(resource, operation, principal);
        }

        public void Revoke(string principal, Operation operation, string resource)
        {
            _granted.Remove(resource, operation, principal);
        }

        public void Deny(string principal, Operation operation, string resource)
        {
            _denied.Add(resource, operation, principal);
        }

        public bool IsGranted(string[] principals, Operation operation, string resource)
        {
            throw new System.NotImplementedException();
        }

        public bool IsGranted(string principal, Operation operation, string resource)
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

        public bool IsHierarchyGranted(string[] principals, Operation operation, string resource)
        {
            throw new System.NotImplementedException();
        }

        public bool IsHierarchyGranted(string principal, Operation operation, string resource)
        {
            throw new System.NotImplementedException();
        }
    }
}
