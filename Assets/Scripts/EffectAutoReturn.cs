using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectAutoReturn : MonoBehaviour
{
    public float delay = 3f;
    private Coroutine routine;
    private PooledObject pooledObject;

    private void Start()
    {
        pooledObject = GetComponent<PooledObject>();
    }

    private void OnEnable()
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(AutoReturnCoroutine());
    }

    private void OnDisable()
    {
        if (routine != null)
            StopCoroutine(routine);
    }

    private IEnumerator AutoReturnCoroutine()
    {
        yield return new WaitForSeconds(delay);

        pooledObject.Return();
    }
}
