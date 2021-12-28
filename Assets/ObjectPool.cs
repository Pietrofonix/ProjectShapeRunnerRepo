using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    List<GameObject> m_pooledObjects = new List<GameObject>();
    [SerializeField] int m_amountToPool;

    [SerializeField] GameObject m_bulletPrefab;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
    }

    void Start()
    {
        for(int i = 0; i < m_amountToPool; i++)
        {
            GameObject obj = Instantiate(m_bulletPrefab);
            obj.SetActive(false);
            m_pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < m_pooledObjects.Count; i++)
        {
            if (!m_pooledObjects[i].activeInHierarchy)
            {
                return m_pooledObjects[i];
            }
        }

        return null;
    }
}
