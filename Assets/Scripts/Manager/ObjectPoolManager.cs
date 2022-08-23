using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _instance;

    public static ObjectPoolManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ObjectPoolManager>();

            return _instance;
        }
    }

    [SerializeField] private List<PoolObjectData> poolObjectDataList = new List<PoolObjectData>(1);

    private Dictionary<string, PoolObject> _prefabDic;
    private Dictionary<string, PoolObjectData> _dataDic;
    private Dictionary<string, Stack<PoolObject>> _poolDic;
    private Dictionary<string, GameObject> _containerDic;

    private void Start()
    {
        int count = poolObjectDataList.Count;
        if (count == 0)
            return;

        _prefabDic = new Dictionary<string, PoolObject>(count);
        _dataDic = new Dictionary<string, PoolObjectData>(count);
        _poolDic = new Dictionary<string, Stack<PoolObject>>(count);
        _containerDic = new Dictionary<string, GameObject>(count);

        for (int i = 0; i < poolObjectDataList.Count; ++i)
            Register(poolObjectDataList[i]);
    }

    private void Register(PoolObjectData data)
    {
        if (_poolDic.ContainsKey(data.Key))
            return;

        GameObject parentObject = new GameObject(data.Key + "Container");
        _containerDic.Add(data.Key, parentObject);

        GameObject sampleObject = Instantiate(data.ObjectPrefab);

        if (!sampleObject.TryGetComponent(out PoolObject poolObject))
        {
            poolObject = sampleObject.AddComponent<PoolObject>();
            poolObject.Key = data.Key;
        }

        sampleObject.SetActive(false);
        sampleObject.transform.SetParent(parentObject.transform);

        Stack<PoolObject> pool = new Stack<PoolObject>();

        for (int i = 0; i < data.ObjectCount - 1; ++i)
        {
            PoolObject cloneObject = poolObject.Clone();
            pool.Push(cloneObject);
            cloneObject.transform.SetParent(parentObject.transform);
        }

        _prefabDic.Add(data.Key, poolObject);
        _dataDic.Add(data.Key, data);
        _poolDic.Add(data.Key, pool);
    }

    public GameObject Get(string key)
    {
        if (!_poolDic.TryGetValue(key, out Stack<PoolObject> pool))
            return null;

        PoolObject poolObject;

        if (pool.Count > 0)
            poolObject = pool.Pop();
        else
            poolObject = _prefabDic[key].Clone();

        poolObject.Activate();

        return poolObject.gameObject;
    }

    public GameObject Get(string key, Vector3 position)
    {
        GameObject newGameObject = Get(key);
        newGameObject.transform.position = position;

        return newGameObject;
    }

    public GameObject Get(string key, Transform parent, bool worldPositionStays)
    {
        GameObject newGameObject = Get(key);
        newGameObject.transform.SetParent(parent, worldPositionStays);

        return newGameObject;
    }

    public void Release(GameObject gameObject)
    {
        PoolObject poolObject = gameObject.GetComponent<PoolObject>();

        if (!_poolDic.TryGetValue(poolObject.Key, out Stack<PoolObject> pool))
            return;

        poolObject.Deactivate();
        poolObject.transform.SetParent(_containerDic[poolObject.Key].transform);
        pool.Push(poolObject);
    }
}
