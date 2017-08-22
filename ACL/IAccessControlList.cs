namespace ACL
{
    interface IAccessControlList
    {
        void Grant(string principal, string operation, string resource);
        void Revoke(string principal, string operation, string resource);
        void Deny(string principal, string operation, string resource);

        bool IsGranted(string[] principals, string operation, string resource);
        bool IsGranted(string principal, string operation, string resource);

        bool IsHierarchyGranted(string[] principals, string operation, string resource);
        bool IsHierarchyGranted(string principal, string operation, string resource);
    }
}
