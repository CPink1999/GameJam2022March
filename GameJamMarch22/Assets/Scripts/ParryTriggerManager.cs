using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryTriggerManager : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 3f;

    private void OnTriggerEnter(Collider other)
    {
        Projectile proj = other.transform.parent.GetComponent<Projectile>();

        if (proj != null)
        {
            HandleSuccessfulParry(proj);
        }
    }

    private void HandleSuccessfulParry(Projectile proj)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Vector3 dir = (proj.transform.position + randomOffset) - transform.position;
        dir = dir.normalized;
        proj.target = dir * 10f;
        proj.speed *= speedMultiplier;
    }
}
