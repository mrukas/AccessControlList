using System.Collections.Generic;

namespace ACL
{
    public class ResourceHolder
    {
        private readonly Dictionary<string, OperationHolder> _resourceOperations = new Dictionary<string, OperationHolder>();

        public int ResourceCount => _resourceOperations.Count;

        public void Add(string resource, string operation, string principal)
        {
            var oh = GetOperationHolder(resource);

            if (oh == null)
            {
                InsertOperationHolder(resource, operation, principal);
            }
            else
            {
                oh.Add(operation, principal);
            }
        }

        public void Remove(string resource, string operation, string principal)
        {
            var oh = GetOperationHolder(resource);

            if (oh == null)
            {
                return;
            }

            oh.Remove(operation, principal);

            // This ensures to remove resource mappings if no resource holders are in the dictionary.
            if (oh.OperationCount == 0)
            {
                RemoveOperationHolder(resource);
            }
        }

        public bool Contains(string resource, string operation, string principal)
        {
            var oh = GetOperationHolder(resource);

            if (oh == null)
            {
                return false;
            }

            return oh.ContainsPrincipal(operation, principal);
        }

        private OperationHolder GetOperationHolder(string resource)
        {
            OperationHolder oh;

            if (_resourceOperations.TryGetValue(resource, out oh))
            {
                return oh;
            }

            return null;
        }

        private OperationHolder InsertOperationHolder(string resource, string operation, string principal)
        {
            var oh = new OperationHolder();
            oh.Add(operation, principal);

            _resourceOperations[resource] = oh;

            return oh;
        }

        private void RemoveOperationHolder(string resource)
        {
            _resourceOperations.Remove(resource);
        }
    }
}