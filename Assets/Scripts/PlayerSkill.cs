using System.Collections.Generic;
using UnityEngine;

public enum Ability
{
    Multishot = 0,
    Ricochet = 1,
    FrontArrow = 2,
    PiercingShot = 3,
    BouncyWall = 4,
    DiagonalArrows = 5,
    SideArrows = 6,
    RearArrow = 7,
}

public class PlayerSkill : MonoBehaviour
{
    private static PlayerSkill _instance;

    public static PlayerSkill Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlayerSkill>();

            return _instance;
        }
    }

    public List<int> playerAbilities = new List<int>();
}
