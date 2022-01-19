using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InflamedGums.Util
{
    public class SetupPrefabPool : MonoBehaviour
    {
        [SerializeField]
        private PrefabPool prefabPool;

        private void Awake()
        {
            prefabPool.Initialize();
        }
    }
}

