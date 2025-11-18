using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public string poolKey;

    private void OnDisable()
    {
        if (ObjectPoolManager.Instance != null)
            ObjectPoolManager.Instance.Return(poolKey, gameObject);
    }
}
