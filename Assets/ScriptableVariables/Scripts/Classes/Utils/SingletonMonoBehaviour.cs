using UnityEngine;
using System;

/// <summary>
/// Base class for Singletons that inherit from MonoBehaviour.
/// Will automatically be added to the scene graph on first access, if not already present.
/// </summary>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                go.name = typeof(T).ToString() + " (SINGLETON)";
                instance = go.AddComponent<T>();
            }
            return instance;
        }
    }

    public static T ReadButDontCreate => instance;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)Convert.ChangeType(this, typeof(T));
        }
    }
}

