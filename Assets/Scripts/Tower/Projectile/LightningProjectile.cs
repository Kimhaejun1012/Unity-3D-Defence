using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : ProjectileBase
{
    [Header("Lightning Settings")]
    public int chainCount = 3;
    public float chainRange = 4f;
    public string key;

    [Header("Visual")]
    public LineRenderer lineRenderer;
    public float lineDuration = 0.15f;

    private List<Monster> hitTargets = new List<Monster>();
    private List<Vector3> linePoints = new List<Vector3>();

    public override void Init(Monster target, float speed, int damage, int level)
    {
        base.Init(target, speed, damage, level);
        hitTargets.Clear();

        linePoints.Clear();

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = false;
        }
    }

    protected override void OnHit(Monster firstTarget)
    {
        ChainDamage(firstTarget);
        DrawLineEffect();
    }

    private void ChainDamage(Monster startTarget)
    {
        Monster current = startTarget;

        for (int i = 0; i < chainCount; i++)
        {
            if (current == null) break;

            linePoints.Add(current.transform.position);

            if (!hitTargets.Contains(current))
            {
                hitTargets.Add(current);
                current.GetComponent<MonsterHealth>().TakeDamage(damage);
                ObjectPoolManager.Instance.Spawn(key, current.transform.position, Quaternion.identity);
            }

            current = FindNextTarget(current);
        }
    }
    private Monster FindNextTarget(Monster from)
    {
        Collider[] hits = Physics.OverlapSphere(from.transform.position, chainRange, LayerMask.GetMask("Monster"));

        Monster nearest = null;
        float nearestDist = float.MaxValue;

        foreach (var h in hits)
        {
            Monster m = h.GetComponent<Monster>();
            if (m == null) continue;
            if (hitTargets.Contains(m)) continue;
            if (!m.gameObject.activeInHierarchy) continue;

            float dist = Vector3.Distance(from.transform.position, m.transform.position);

            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = m;
            }
        }

        return nearest;
    }
    private void DrawLineEffect()
    {
        if (lineRenderer == null || linePoints.Count == 0)
            return;

        lineRenderer.enabled = true;
        lineRenderer.positionCount = linePoints.Count;

        for (int i = 0; i < linePoints.Count; i++)
        {
            lineRenderer.SetPosition(i, linePoints[i]);
        }

        StartCoroutine(LineOffCoroutine());
    }

    private IEnumerator LineOffCoroutine()
    {
        yield return new WaitForSeconds(lineDuration);

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = false;
        }
    }
}
