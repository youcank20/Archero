using UnityEngine;

public class Plant : Enemy
{
    [SerializeField] private Transform redSphereTransform;

    private void Start()
    {
        maxHp = 600;
        currentHp = 600;
        speed = 1f;
        damage = 120;

        State = EState.Idle;
    }

    private void Update()
    {
        if (State == EState.Idle)
            LookAt(Player.Instance.transform.position);
    }

    protected override void SetStateIdle()
    {
        base.SetStateIdle();
    }

    protected override void SetStateHitted()
    {
        if (State != EState.Attack)
            base.SetStateHitted();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().MinusHp(damage);
        }
    }

    private void ShootRedSphere()
    {
        if (State != EState.Attack)
            return;

        LookAt(Player.Instance.transform.position);

        MakeRedSphere();
    }

    private void MakeRedSphere()
    {
        GameObject redSphere = ObjectPoolManager.Instance.Get("RedSphere", transform, false);

        redSphere.transform.position = transform.position + Vector3.up * 0.5f;
        redSphere.GetComponent<RedSphere>().SetRotate(rotateTransform.rotation);
        redSphere.GetComponent<RedSphere>().SetDamage(100);
        redSphere.GetComponent<RedSphere>().Initialize(redSphereTransform.position);
    }
}
