using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<DataManager>();

            return _instance;
        }
    }

    private PlayerData _playerData;
    private string _path;
    private string _fileName;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _path = Application.persistentDataPath + "/";
        _fileName = "playerdata";
    }

    public void SaveData()
    {
        string contents = JsonUtility.ToJson(_playerData);
        File.WriteAllText(_path + _fileName, contents);
    }

    public void LoadData()
    {
        string contents = File.ReadAllText(_path + _fileName);
        _playerData = JsonUtility.FromJson<PlayerData>(contents);
    }
}
