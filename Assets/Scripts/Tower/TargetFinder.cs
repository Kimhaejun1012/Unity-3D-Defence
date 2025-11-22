using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class TargetFinder
{
    public static Monster GetNearestEnemy(Vector3 origin, float range)
    {
        Collider[] hits = Physics.OverlapSphere(origin, range, LayerMask.GetMask("Monster"));

        Monster nearest = null;
        float nearestDist = float.MaxValue;

        foreach (var hit in hits)
        {
            Monster m = hit.GetComponent<Monster>();
            if (m == null) continue;

            float dist = Vector3.Distance(origin, m.transform.position);

            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = m;
            }
        }

        return nearest;
    }
}
