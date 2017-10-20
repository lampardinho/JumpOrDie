using System.Collections.Generic;
using UnityEngine;

public static class PoolManager
{
    const int DefaultPoolSize = 3;

    class Pool
    {
        private int _nextId = 1;
        private Stack<GameObject> _inactive;
        private GameObject _prefab;
        
        public Pool(GameObject prefab, int initialQty)
        {
            _prefab = prefab;
            _inactive = new Stack<GameObject>(initialQty);
        }

        public GameObject Spawn(Vector3 pos, Quaternion rot)
        {
            GameObject obj;
            if (_inactive.Count == 0)
            {
                obj = GameObject.Instantiate(_prefab, pos, rot);
                obj.name = _prefab.name + " (" + (_nextId++) + ")";

                obj.AddComponent<PoolMember>().myPool = this;
            }
            else
            {
                obj = _inactive.Pop();

                if (obj == null)
                {
                    return Spawn(pos, rot);
                }
            }

            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.SetActive(true);
            return obj;
        }

        public void Despawn(GameObject obj)
        {
            obj.SetActive(false);
            _inactive.Push(obj);
        }
    }
    
    class PoolMember : MonoBehaviour
    {
        public Pool myPool;
    }

    private static Dictionary<GameObject, Pool> _pools;

    static void Init(GameObject prefab = null, int qty = DefaultPoolSize)
    {
        if (_pools == null)
        {
            _pools = new Dictionary<GameObject, Pool>();
        }
        if (prefab != null && _pools.ContainsKey(prefab) == false)
        {
            _pools[prefab] = new Pool(prefab, qty);
        }
    }

    public static void Preload(GameObject prefab, int qty = 1)
    {
        Init(prefab, qty);

        GameObject[] obs = new GameObject[qty];
        for (int i = 0; i < qty; i++)
        {
            obs[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);
        }

        for (int i = 0; i < qty; i++)
        {
            Despawn(obs[i]);
        }
    }

    public static GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        Init(prefab);
        return _pools[prefab].Spawn(pos, rot);
    }

    public static void Despawn(GameObject obj)
    {
        PoolMember pm = obj.GetComponent<PoolMember>();
        if (pm == null)
        {
            Debug.Log("Object '" + obj.name + "' wasn't spawned from a pool. Destroying it instead.");
            GameObject.Destroy(obj);
        }
        else
        {
            pm.myPool.Despawn(obj);
        }
    }
}
