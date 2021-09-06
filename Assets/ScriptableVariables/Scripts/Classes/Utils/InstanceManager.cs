using System.Collections.Generic;
using UnityEngine;
using System;

namespace InflamedGums.Util.ScriptableVariables
{
    /// <summary>
    /// Instance Manager for Scriptable Variables.
    /// Useful when multiple Instances of a prefab are spawned,
    /// and where each of those instances also needs it's own instance
    /// of a scriptable variable.
    /// Act's kind of like a poor man's c++ smart pointer.
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable Objects/Utils/Instance Manager")]
    public class InstanceManager : ScriptableObject
    {
        /// <summary>
        /// Helper class for reference counting
        /// </summary>
        public class SharedReference
        {
            public int count;
            public ScriptableObject value;
        }

        /// <summary>
        /// the template defines what kind of objects this class manages
        /// </summary>
        public ScriptableObject template;

        /// <summary>
        /// backing dictionary to store references to created instances.
        /// Should not be accessed directly.
        /// TODO: hide access, find a way to still give access to the editor script
        /// </summary>
        public Dictionary<int, SharedReference> referenceDictionary;

        /// <summary>
        /// Returns a reference to a scriptable variable associated with the given key.
        /// If the reference does not exist, it is created. 
        /// When using this method to get a reference, make sure to call
        /// 'DropReference' in your MonoBehaviour's 'OnDestroy' method.
        /// </summary>
        public ScriptableObject GetSharedReference(int key)
        {
            if (referenceDictionary == null)
                referenceDictionary = new Dictionary<int, SharedReference>();

            if (!referenceDictionary.TryGetValue(key, out SharedReference outRef))
            {
                outRef = new SharedReference { count = 1, value = Instantiate(template) };
                referenceDictionary.Add(key, outRef);
            }
            else
            {
                outRef.count++;
            }
            return outRef.value;
        }

        /// <summary>
        /// Returns a reference to a scriptable variable associated with the given key.
        /// If the reference does not exist, null is returned.
        /// </summary>
        public ScriptableObject GetWeakReference(int key)
        {
            if (referenceDictionary == null)
                return null;

            if (!referenceDictionary.TryGetValue(key, out SharedReference outRef))
                return null;
            
            return outRef.value;
        }

        /// <summary>
        /// Call this method when the reference associated with the given key is no longer needed by your class.
        /// Destroys the reference.
        /// </summary>
        public void DropReference(int key)
        {
            if (referenceDictionary == null)
                return;

            if (!referenceDictionary.TryGetValue(key, out SharedReference outRef))
                return;
            else
            {
                outRef.count--;
                if (outRef.count < 1)
                {
                    referenceDictionary.Remove(key);
                }
            }
        }
    }
}

