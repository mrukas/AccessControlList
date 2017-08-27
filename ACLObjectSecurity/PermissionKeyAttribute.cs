using System;

namespace ACLObjectSecurity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class PermissionKeyAttribute : Attribute
    {
        public string Key { get; }

        public PermissionKeyAttribute(string key)
        {
            Key = key.Trim().ToLowerInvariant();
        }
    }
}
