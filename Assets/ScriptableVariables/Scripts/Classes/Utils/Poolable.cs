using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Poolable : MonoBehaviour
{
    private PrefabPool parentPool;

    public void Spawn(PrefabPool parentPool)
    {
        // already spawned by another pool
        if (this.parentPool) return;
        
        this.parentPool = parentPool;
        gameObject.SetActive(true);

        ISpawnCallbackReceiver[] spawnCallbackReceivers =
            GetComponentsInChildren<ISpawnCallbackReceiver>();

        for (int i = 0; i < spawnCallbackReceivers.Length; ++i)
            spawnCallbackReceivers[i].OnSpawn();
    }
    public void Despawn()
    {
        IDespawnCallbackReceiver[] spawnCallbackReceivers =
            GetComponentsInChildren<IDespawnCallbackReceiver>();

        for (int i = 0; i < spawnCallbackReceivers.Length; ++i)
            spawnCallbackReceivers[i].OnDespawn();
        
        parentPool.RemoveInstance(gameObject);
        parentPool = null;
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
