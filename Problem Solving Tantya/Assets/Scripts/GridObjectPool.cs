using System.Collections.Generic;
using UnityEngine;

public class GridObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;

    private Queue<GameObject> pool = new Queue<GameObject>();

    public void Init(int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            GameObject obj = Instantiate(cellPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Get(Vector3 pos)
    {
        if (pool.Count == 0)
        {
            Debug.LogWarning("Pool is empty! Consider increasing the initial capacity.");
            return null;
        }

        GameObject obj = pool.Dequeue();
        obj.transform.position = pos;
        obj.SetActive(true);

        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}