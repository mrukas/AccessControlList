using System.Collections.Generic;
using System.Collections.Specialized;

namespace ACL
{
    public class OperationHolder
    {
        private readonly Dictionary<string, StringCollection> _operationPrincipals = new Dictionary<string, StringCollection>();

        public int OperationCount => _operationPrincipals.Count;

        public void Add(string operation, string principal)
        {
            var operationPrincipals = GetOperationPrincipals(operation);

            if (operationPrincipals == null)
            {
                InsertOperationPrincipals(operation, principal);
            }
            else
            {
                if (!operationPrincipals.Contains(principal))
                {
                    operationPrincipals.Add(principal);
                }
            }
        }

        public void Remove(string operation, string principal)
        {
            var operationPrincipals = GetOperationPrincipals(operation);

            if (operationPrincipals == null)
            {
                return;
            }

            operationPrincipals.Remove(principal);

            // This ensures to remove operation mappings of no principals are in the collection
            if (operationPrincipals.Count == 0)
            {
                RemoveOperationPrincipals(operation);
            }
        }

        public bool ContainsPrincipal(string operation, string principal)
        {
            var operationPrincipals = GetOperationPrincipals(operation);

            if (operationPrincipals == null)
            {
                return false;
            }

            return operationPrincipals.Contains(principal);
        }

        private StringCollection GetOperationPrincipals(string operation)
        {
            StringCollection principals;

            if (_operationPrincipals.TryGetValue(operation, out principals))
            {
                return principals;
            }

            return null;
        }

        private void InsertOperationPrincipals(string operation, string principal)
        {
            var principalCollection = new StringCollection { principal };

            _operationPrincipals[operation] = principalCollection;
        }

        private void RemoveOperationPrincipals(string operation)
        {
            _operationPrincipals.Remove(operation);
        }
    }
}
