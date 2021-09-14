using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPrefabPool : MonoBehaviour
{
    [SerializeField]
    private PrefabPool prefabPool;

    private void Awake()
    {
        prefabPool.Initialize();
    }
}
