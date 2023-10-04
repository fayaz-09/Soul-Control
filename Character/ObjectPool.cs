using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private PoolableObject Prefab;
    private List<PoolableObject> objectList;

    private ObjectPool(PoolableObject Prefab, int size)
    {
        this.Prefab = Prefab;
        objectList = new List<PoolableObject>(size);
    }

    public static ObjectPool createInstance(PoolableObject prefab, int size)
    {
        ObjectPool pool = new ObjectPool(prefab, size);

        GameObject poolObject = new GameObject(prefab.name + " Pool");
        pool.CreateObjects(poolObject.transform, size);

        return pool;
    }

    private void CreateObjects(Transform parent, int size)
    {
        for (int i = 0; i < size; i++)
        {
            PoolableObject poolableObject = GameObject.Instantiate(Prefab, Vector3.zero, Quaternion.identity, parent.transform);
            poolableObject.Parent = this;
            poolableObject.gameObject.SetActive(false);
        }
    }

    public void returnObjectToPool(PoolableObject poolableObject)
    {
        objectList.Add(poolableObject);
    }

    public PoolableObject GetObject()
    {
        Debug.Log("test");

        if (objectList.Count > 0)
        {
            PoolableObject instance = objectList[0];
            objectList.RemoveAt(0);

            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            return null;
        }
    }

}
