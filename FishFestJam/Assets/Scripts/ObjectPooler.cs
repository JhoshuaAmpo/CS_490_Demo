using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToCopy;

    [SerializeField]
    private int maxObjectCount = 1;

    public List<GameObject> objectPool;

    private void Awake() {
        objectPool = new();
        GameObject tmp;
        for (int i = 0; i < maxObjectCount; i++)
        {
            tmp = Instantiate(objectToCopy,this.transform);
            tmp.SetActive(false);
            objectPool.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < maxObjectCount; i++)
        {
            if(!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }
        return null;
    }
}
