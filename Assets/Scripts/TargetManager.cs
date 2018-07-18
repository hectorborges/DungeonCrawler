using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;
    public float targetRadius;
    public LayerMask targetLayer;
    List<Transform> enemiesInRange;

    public Transform closestTarget;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Collider[] nearbyTargets = Physics.OverlapSphere(transform.position, targetRadius, targetLayer);

        foreach(Collider target in nearbyTargets)
        {
            if (closestTarget == null)
                closestTarget = target.transform;

            if(Utility.CheckDistance(transform.position, target.transform.position) < Utility.CheckDistance(transform.position, closestTarget.transform.position))
                closestTarget = target.transform;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, targetRadius);
    }
}
