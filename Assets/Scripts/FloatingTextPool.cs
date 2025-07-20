using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextPool : MonoBehaviour
{
    public static FloatingTextPool Instance { get; private set; }  
    private int initialPoolSize = 10;
    [SerializeField] private GameObject floatingTextPrefab;

    private Queue<GameObject> pool = new();

    private void Awake()
    {
        if(Instance == null) Instance = this;

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(floatingTextPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(floatingTextPrefab, transform);
            return obj;
        }
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pool.Enqueue(obj);
    }
}