using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Poolable : MonoBehaviour
{
    private Action<GameObject> despawnCallback;

    public void Spawn(Action<GameObject> despawnCallback)
    {
        this.despawnCallback = despawnCallback;
        
        gameObject.SetActive(true);

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

        despawnCallback(gameObject);
    }
    
    public interface ISpawnCallbackReceiver
    {
        void OnSpawn();
    }

    public interface IDespawnCallbackReceiver
    {
        void OnDespawn();
    }
}
