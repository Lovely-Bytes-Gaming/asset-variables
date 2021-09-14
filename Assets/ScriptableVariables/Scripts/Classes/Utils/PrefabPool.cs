using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Prefab Pool")]
public class PrefabPool : ScriptableObject
{
    [SerializeField, Tooltip("Prefab type spawned by this pool")]
    private Poolable template;

    [SerializeField]
    private int 
        maxCapacity;

    private Stack<GameObject> unusedObjects;

    private void OnEnable()
    {
        unusedObjects = new Stack<GameObject>();
    }

    public GameObject SpawnInstance()
    {
        GameObject go;

        if (unusedObjects.Count < 1)
            go = Instantiate(template.gameObject);
        else
            go = unusedObjects.Pop();

        go.GetComponent<Poolable>().Spawn(DespawnCallback);

        return go;
    }

    public GameObject[] SpawnInstanceArray(int num)
    {
        GameObject[] outGOs = new GameObject[num];
        for(int i = 0; i < num; ++i)
        {
            outGOs[i] = SpawnInstance();
        }
        return outGOs;
    }

    public GameObject SpawnInstance(Vector3 atPosition)
    {
        GameObject go;

        if (unusedObjects.Count < 1)
            go = Instantiate(template.gameObject);
        else
            go = unusedObjects.Pop();

        go.transform.position = atPosition;
        go.GetComponent<Poolable>().Spawn(DespawnCallback);

        return go;
    }

    private void DespawnCallback(GameObject go)
    {
        go.transform.parent = null;
        if(unusedObjects.Count >= maxCapacity)
        {
            Destroy(go);
        }
        else
        {
            go.SetActive(false);
            unusedObjects.Push(go);
        }
    }
}
