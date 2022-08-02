using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();
    public PlayerAttack Player;

    private void Update()
    {
        if (EnemyList.Count != 0)
        {
            Player.EnemyList = EnemyList;
        }
    }
}
