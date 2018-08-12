using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Lairinus.Performance
{
    public class ComponentCacher<T> where T : Component
    {
        private Dictionary<int, T> _cache = new Dictionary<int, T>();

        /// <summary>
        /// Adds the passed MonoBehaviour to the cache
        /// </summary>
        /// <param name="component">The desired MonoBehaviour to add to the cached list</param>
        /// <param name="replaceExisting">If the reference is already found, the passed monoBehavior parameter will replace it</param>
        public void Add(Component component)
        {
            if (component == null)
                return;

            int instanceID = component.GetInstanceID();
            if (!_cache.ContainsKey(instanceID))
            {
                T convertedObject = ConvertObjecTypeInternal(component);
                if (convertedObject != null)
                    _cache[instanceID] = convertedObject;
            }
        }

        /// <summary>
        /// Removes all data from this instance of the cache
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
        }

        /// <summary>
        /// Returns an existing cached value if it exists. This is approximately 30-40% faster than GetComponent()! (Unity 2018.2)
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public T Get(Component component)
        {
            int instanceID = component.GetInstanceID();
            if (_cache.ContainsKey(instanceID))
                return _cache[instanceID];

            return default(T);
        }

        public List<T> GetCachedValues()
        {
            return _cache.Values.ToList();
        }

        /// <summary>
        /// Removes a cached T MonoBehaviour reference
        /// </summary>
        /// <param name="component">The MonoBehaviour reference to remove from the cache</param>
        public void Remove(Component component)
        {
            if (component == null)
                return;

            int instanceID = component.GetInstanceID();
            if (_cache.ContainsKey(instanceID))
                _cache.Remove(instanceID);
        }

        private static T ConvertObjecTypeInternal(Component component)
        {
            if (component == null)
                return default(T);

            System.Type type = component.GetType();
            if (type == typeof(T))
                return (T)component;
            else if (component is T)
                return component as T;
            else
            {
                T convertedObject = component.gameObject.GetComponent<T>();
                return convertedObject;
            }
        }
    }
}