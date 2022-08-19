using System;
using UnityEngine;

[Serializable]
public class PoolObjectData
{
    public const int INITIAL_COUNT = 10;

    public string Key;
    public GameObject ObjectPrefab;
    public int ObjectCount = INITIAL_COUNT;
}
