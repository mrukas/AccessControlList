using System.Reflection;
using ACL;

namespace ACLObjectSecurity
{
    public class PermissionManager
    {
        private ReadWriteAccessControlList _acl;

        public PermissionManager(ReadWriteAccessControlList acl)
        {
            _acl = acl;
        }

        public (bool HasViewAbleChilds, bool HasEditableChilds) ApplyPermissions(string principal, object item, string permissionKey = "")
        {
            if (item == null)
            {
                return (false, false);
            }

            var objType = item.GetType();

            if (objType.IsSimple())
            {
                return (false, false);
            }

            var attribute = objType.GetCustomAttribute<PermissionKeyAttribute>();


            if (attribute != null)
            {
                permissionKey += $"{_acl.HierarchySeparator}{attribute.Key}";
            }

            var properties = objType.GetProperties();

            foreach (var prop in properties)
            {
                var propType = prop.PropertyType;
                var tempPermissionKey = permissionKey;
                var propAttribute = prop.GetCustomAttribute<PermissionKeyAttribute>();

                if (attribute != null)
                {
                    tempPermissionKey += $"{_acl.HierarchySeparator}{attribute.Key}";
                }

                //if (propType.is)


            }

            return (false, false);
        }
    }
}
