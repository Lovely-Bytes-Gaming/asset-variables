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
        capacity;

    private Poolable.SpawnHelper spawnHelper;

    public void Initialize()
    {
        spawnHelper = new Poolable.SpawnHelper(template.gameObject, capacity);
    }

    public GameObject SpawnInstance(Transform parent = null)
        => spawnHelper.SpawnInstance(parent);

    public GameObject SpawnInstance(Vector3 atPosition, Quaternion rotation, Transform parent = null)
        => spawnHelper.SpawnInstance(atPosition, rotation, parent);

    public GameObject[] SpawnInstanceArray(int count)
        => spawnHelper.SpawnInstanceArray(count);
}
