using System.Collections.Generic;
using UnityEngine;
using System;

namespace CustomLibrary.Util.ScriptableVariables
{
    /// <summary>
    /// Instance Manager for Scriptable Variables.
    /// Useful when multiple Instances of a prefab are spawned,
    /// and where each of those instances also needs it's own instance
    /// of a scriptable variable.
    /// </summary>
    public class InstanceManager : MonoBehaviour
    {
        private Type type = null;

        /// <summary>
        /// backing dictionary to store references to created instances.
        /// Should not be accessed directly.
        /// TODO: hide access, find a way to still give access to the editor script
        /// </summary>
        public Dictionary<int, ScriptableObject> referenceDictionary;

        /// <summary>
        /// Type-check to promote usage of one instance manager per scriptable variable type.
        /// </summary>
        private bool IsTypeValid(Type t)
        {
            if (type == null)
            {
                type = t;
                return true;
            }

            if (type != t)
            {
                Debug.LogError("Instance Manager has already been typed. Item not added");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns a reference to a scriptable variable associated with the given key.
        /// </summary>
        public T GetReference<T>(int key, T obj) where T : ScriptableObject
        {
            if (!IsTypeValid(typeof(T))) return null;

            ScriptableObject outRef;
            if (referenceDictionary == null) 
                referenceDictionary = new Dictionary<int, ScriptableObject>();

            if (!referenceDictionary.TryGetValue(key, out outRef))
            {
                outRef = Instantiate(obj);
                referenceDictionary.Add(key, outRef);
            }
            return (T)outRef;
        }
    }
}

