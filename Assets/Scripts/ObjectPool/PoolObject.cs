using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public string Key;

    public PoolObject Clone()
    {
        GameObject newGameObject = Instantiate(gameObject);

        if (!newGameObject.TryGetComponent(out PoolObject poolObject))
            poolObject = newGameObject.AddComponent<PoolObject>();

        newGameObject.name = gameObject.name;
        newGameObject.SetActive(false);

        return poolObject;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
