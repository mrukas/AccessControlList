using System.Collections.Generic;

namespace ACL
{
    public class OperationHolder
    {
        private readonly Dictionary<string, ResourceHolder> _operationResourceHolders = new Dictionary<string, ResourceHolder>();

        public void Add(string resource, string operation, string principal)
        {
            var rh = GetResourceHolder(resource);

            if (rh == null)
            {
                InsertResourceHolder(resource, operation, principal);
            }
            else
            {
                rh.Add(operation, principal);
            }
        }

        public void Remove(string resource, string operation, string principal)
        {
            var rh = GetResourceHolder(resource);

            if (rh == null)
            {
                return;
            }

            rh.Remove(operation, principal);

            // This ensures to remove resource mappings if no resource holders are in the dictionary.
            if (rh.OperationCount == 0)
            {
                RemoveResourceHolder(resource);
            }
        }

        public bool Contains(string resource, string operation, string principal)
        {
            var rh = GetResourceHolder(resource);

            if (rh == null)
            {
                return false;
            }

            return rh.ContainsPrincipal(operation, principal);
        }

        private ResourceHolder GetResourceHolder(string resource)
        {
            ResourceHolder rh;

            if (_operationResourceHolders.TryGetValue(resource, out rh))
            {
                return rh;
            }

            return null;
        }

        private ResourceHolder InsertResourceHolder(string resource, string operation, string principal)
        {
            var rh = new ResourceHolder();
            rh.Add(operation, principal);

            _operationResourceHolders[resource] = rh;

            return rh;
        }

        private void RemoveResourceHolder(string resource)
        {
            _operationResourceHolders.Remove(resource);
        }
    }
}