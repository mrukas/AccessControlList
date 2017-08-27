namespace ACL
{
    public class ReadWriteAccessControlList : AccessControlList
    {
        public void Grant(string principal, Operation operation, string resource)
        {
            Grant(principal, operation.ToString().ToLowerInvariant(), resource);
        }

        public void Revoke(string principal, Operation operation, string resource)
        {
            Revoke(principal, operation.ToString().ToLowerInvariant(), resource);
        }

        public void Deny(string principal, Operation operation, string resource)
        {
            Deny(principal, operation.ToString().ToLowerInvariant(), resource);
        }

        public bool IsHierarchyGranted(string principal, Operation operation, string resource)
        {
            var operationString = operation.ToString().ToLowerInvariant();

            if (Denied.Contains(resource, operationString, principal))
            {
                return false;
            }

            // If read is denied also write should be denied.
            if (operation == Operation.Write && Denied.Contains(resource, Operation.Read.ToString().ToLowerInvariant(), principal))
            {
                return false;
            }

            if (Granted.Contains(resource, operationString, principal))
            {
                return true;
            }

            // If write is granted also read should be granted.
            if (operation == Operation.Read &&
                Granted.Contains(resource, Operation.Write.ToString().ToLowerInvariant(), principal))
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

        public bool IsGranted(string principal, Operation operation, string resource)
        {
            var operationString = operation.ToString().ToLowerInvariant();

            if (Denied.Contains(resource, operationString, principal))
            {
                return false;
            }

            // If read is denied also write should be denied.
            if (operation == Operation.Write && Denied.Contains(resource, Operation.Read.ToString().ToLowerInvariant(), principal))
            {
                return false;
            }

            if (Granted.Contains(resource, operationString, principal))
            {
                return true;
            }

            // If write is granted also read should be granted.
            if (operation == Operation.Read &&
                Granted.Contains(resource, Operation.Write.ToString().ToLowerInvariant(), principal))
            {
                return true;
            }

            return false;
        }
    }
}
