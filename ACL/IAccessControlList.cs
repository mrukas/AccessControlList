namespace ACL
{
    interface IAccessControlList
    {
        void Grant(string principal, Operation operation, string resource);
        void Revoke(string principal, Operation operation, string resource);
        void Deny(string principal, Operation operation, string resource);

        bool IsGranted(string[] principals, Operation operation, string resource);
        bool IsGranted(string principal, Operation operation, string resource);

        bool IsHierarchyGranted(string[] principals, Operation operation, string resource);
        bool IsHierarchyGranted(string principal, Operation operation, string resource);
    }
}
