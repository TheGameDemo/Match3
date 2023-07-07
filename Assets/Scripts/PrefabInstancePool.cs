using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

/// <summary>
/// We'll be working through lots of tile instances, so we're going to pool them.
/// </summary>
/// <typeparam name="T"></typeparam>
public struct PrefabInstancePool<T> where T : MonoBehaviour
{
    Stack<T> pool;

    /// <summary>
    /// Instantiates a gives prefab.
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public T GetInstance(T prefab)
    {
        if (pool == null)
        {
            pool = new();
        }
        // To support disabled domain reloading,
        // if the pool exist check if it contains a reference to something that has been destroyed.
        // If so we assume that the pool survived exiting play mode and thus clear it to get rid of the old references.
        // This is only needed in the editor.
#if UNITY_EDITOR
        // The pool.TryPeek(out T i) && !i statement is used to check if a Stack contains an element with a specific value.
        // The TryPeek method is used to return the top element of a Stack without removing it from the stack.
        // and the !i condition checks if the element is null.
        else if (pool.TryPeek(out T i) && !i)
        {
            // Instances destroyed, assuming due to exiting play mode.
            pool.Clear();
        }
#endif
        // The pool.TryPop(out T instance) is a method used to remove and return an object from the top of a Stack<T> collection.
        // The method returns true if an element was removed and returned successfully, and false if the collection is empty.
        // The removed element is stored in the instance variable passed as an out parameter.
        if (pool.TryPop(out T instance))
        {
            instance.gameObject.SetActive(true);
        }
        else
        {
            instance = Object.Instantiate(prefab);
        }
        return instance;
    }

    /// <summary>
    /// Destroys a given instance's game object.
    /// </summary>
    /// <param name="instance"></param>
    public void Recycle(T instance)
    {
        // When recycling, if we're in the editor check whether the pool is missing.
        // If so we assume the reference got lost due to a hot reload and only then do we destroy the game object.
#if UNITY_EDITOR
        if (pool == null)
        {
            // Pool lost, assuming due to hot reload.
            Object.Destroy(instance.gameObject);
            return;
        }
#endif
        pool.Push(instance);
        // The instance.gameObject.SetActive(false) is a method used to deactivate a game object in the scene.
        // When the argument is false, the game object is deactivated and is no longer visible or interactive in the scene.
        instance.gameObject.SetActive(false);
    }
}
