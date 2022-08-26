using UnityEngine;

public class Plant : Enemy
{
    private void Start()
    {
        maxHp = 600;
        currentHp = 600;
        speed = 1f;
        damage = 120; //clockwise redball 100

        State = EState.Idle;
    }
}
