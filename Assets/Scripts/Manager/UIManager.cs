using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UIManager>();

            return _instance;
        }
    }

    [SerializeField] private List<GameObject> UIObjectList = new List<GameObject>();

    private GameObject _UIContainer;
    private Dictionary<string, GameObject> _containerDic;

    private void Start()
    {
        _UIContainer = new GameObject("UIContainer");
        _containerDic = new Dictionary<string, GameObject>();

        for (int i = 0; i < UIObjectList.Count; ++i)
            Register(UIObjectList[i]);
    }

    private void Register(GameObject gameObject)
    {
        GameObject newGameObject = Instantiate(gameObject);

        newGameObject.transform.SetParent(_UIContainer.transform);
        newGameObject.SetActive(false);

        _containerDic.Add(gameObject.name, newGameObject);
    }

    public GameObject Get(string key)
    {
        GameObject gameObject = _containerDic[key];
        gameObject.SetActive(true);

        return gameObject;
    }

    public void Release(GameObject gameObject)
    {
        gameObject.transform.SetParent(_UIContainer.transform);
        gameObject.SetActive(false);
    }

    public void Release(string key)
    {
        _containerDic[key].transform.SetParent(_UIContainer.transform);
        _containerDic[key].SetActive(false);
    }
}
