using UnityEngine;

public class Tile : MonoBehaviour
{
    PrefabInstancePool<Tile> pool;

    // Gets an instance of itself, gives it the same pool, and places it at a give position. 
    public Tile Spawn(Vector3 position)
    {
        Tile instance = pool.GetInstance(this);
        instance.pool = pool;
        instance.transform.localPosition = position;
        return instance;
    }

    // Recycles itself.
    public void Despawn() => pool.Recycle(this);
}
