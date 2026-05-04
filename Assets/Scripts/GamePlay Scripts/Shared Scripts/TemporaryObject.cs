using UnityEngine;

public class TemporaryObject : MonoBehaviour
{
    public float selfDestructTimer = 2f;

    protected virtual void Update()
    {
        selfDestructTimer -= Time.deltaTime;
        if (selfDestructTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
