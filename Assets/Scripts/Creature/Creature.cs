using UnityEngine;

public enum State
{
    Idle = 0,
    Move = 1,
    Attack = 2,
    Hitted = 3,
    Die = 4,
}

public class Creature : MonoBehaviour
{
    protected int MaxHp;
    protected int Hp;
}
