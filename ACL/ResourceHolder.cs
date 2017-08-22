﻿using System.Collections.Generic;
using System.Collections.Specialized;

namespace ACL
{
    public class ResourceHolder
    {
        private readonly Dictionary<string, StringCollection> _operationUserMapping = new Dictionary<string, StringCollection>();

        public int OperationCount => _operationUserMapping.Count;

        public void Add(string operation, string principal)
        {
            var existingOperation = GetOperationMapping(operation);

            if (existingOperation == null)
            {
                InsertOperationMapping(operation, principal);
            }
            else
            {
                if (!existingOperation.Contains(principal))
                {
                    existingOperation.Add(principal);
                }
            }
        }

        public void Remove(string operation, string principal)
        {
            var existingOperation = GetOperationMapping(operation);

            if (existingOperation == null)
            {
                return;
            }

            existingOperation.Remove(principal);

            // This ensures to remove operation mappings of no principals are in the collection
            if (existingOperation.Count == 0)
            {
                RemoveOperationMapping(operation);
            }
        }

        public bool ContainsPrincipal(string operation, string principal)
        {
            var existingOperation = GetOperationMapping(operation);

            if (existingOperation == null)
            {
                return false;
            }

            return existingOperation.Contains(principal);
        }

        private StringCollection GetOperationMapping(string operation)
        {
            StringCollection principals;

            if (_operationUserMapping.TryGetValue(operation, out principals))
            {
                return principals;
            }

            return null;
        }

        private void InsertOperationMapping(string operation, string principal)
        {
            var principalCollection = new StringCollection { principal };

            _operationUserMapping[operation] = principalCollection;
        }

        private void RemoveOperationMapping(string operation)
        {
            _operationUserMapping.Remove(operation);
        }
    }
}
