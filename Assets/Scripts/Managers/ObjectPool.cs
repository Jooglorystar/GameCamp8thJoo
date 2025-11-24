using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size;
        public string tag;
        public Queue<GameObject> objects = new Queue<GameObject>();
    }

    [SerializeField] private List<Pool> _pools;

    private Dictionary<string, Pool> _poolDictionary;

    private void Awake()
    {
        _poolDictionary = new Dictionary<string, Pool>();
        InitPools();
    }

    private void InitPools()
    {
        foreach (Pool pool in _pools)
        {
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                pool.objects.Enqueue(obj);
                obj.SetActive(false);
            }
            _poolDictionary.Add(pool.tag, pool);
        }
    }

    public T Get<T>(string p_tag) where T : MonoBehaviour
    {
        if(_poolDictionary.ContainsKey(p_tag))
        {
            GameObject obj = _poolDictionary[p_tag].objects.Dequeue();
            obj.gameObject.SetActive(true);
            _poolDictionary[p_tag].objects.Enqueue(obj);
            return obj.GetComponent<T>();
        }
        return null;
    }
}
