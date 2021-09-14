using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Poolable : MonoBehaviour
{
    private SpawnHelper spawnHelper;

    private void Spawn()
    {
        ISpawnCallbackReceiver[] spawnCallbackReceivers =
            GetComponentsInChildren<ISpawnCallbackReceiver>();

        for (int i = 0; i < spawnCallbackReceivers.Length; ++i)
            spawnCallbackReceivers[i].OnSpawn();
    }
    public void Despawn()
    {
        IDespawnCallbackReceiver[] despawnCallbackReceivers =
            GetComponentsInChildren<IDespawnCallbackReceiver>();

        for (int i = 0; i < despawnCallbackReceivers.Length; ++i)
            despawnCallbackReceivers[i].OnDespawn();

        spawnHelper.Despawn(this);
    }
    
    public interface ISpawnCallbackReceiver
    {
        void OnSpawn();
    }

    public interface IDespawnCallbackReceiver
    {
        void OnDespawn();
    }


    public class SpawnHelper
    {
        private readonly Stack<GameObject> unusedObjects;
        private readonly GameObject template;
        private readonly int capacity;

        public SpawnHelper(GameObject template, int capacity)
        {
            this.template = template;
            unusedObjects = new Stack<GameObject>(capacity);
            this.capacity = capacity;
        }

        public GameObject SpawnInstance(Transform parent = null)
        {
            GameObject go;

            if (unusedObjects.Count < 1)
            {
                go = Instantiate(template.gameObject);
                go.GetComponent<Poolable>().spawnHelper = this;
            }
            else
                go = unusedObjects.Pop();

            go.transform.parent = parent;
            go.transform.localPosition = Vector3.zero;
            go.SetActive(true);
            go.GetComponent<Poolable>().Spawn();

            return go;
        }

        public GameObject[] SpawnInstanceArray(int num)
        {
            GameObject[] outGOs = new GameObject[num];
            for (int i = 0; i < num; ++i)
            {
                outGOs[i] = SpawnInstance();
            }
            return outGOs;
        }

        public GameObject SpawnInstance(Vector3 atPosition, Transform parent = null)
        {
            GameObject go;

            if (unusedObjects.Count < 1)
                go = Instantiate(template.gameObject);
            else
                go = unusedObjects.Pop();

            go.transform.position = atPosition;
            go.transform.parent = parent;
            go.SetActive(true);
            go.GetComponent<Poolable>().Spawn();

            return go;
        }

        public void Despawn(Poolable p)
        {
            GameObject go = p.gameObject;
            if (unusedObjects.Count >= capacity)
            {
                Destroy(go);
            }
            else
            {
                go.transform.parent = null;
                go.SetActive(false);
                unusedObjects.Push(go);
            }
        }
    }
}
