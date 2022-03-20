using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryTriggerManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Projectile proj = other.GetComponent<Projectile>();
        
        if (proj != null)
        {
            Debug.Log(other);
            HandleSuccessfulParry(proj);
        }
    }

    private void HandleSuccessfulParry(Projectile proj)
    {
        Vector3 dir = proj.transform.position - transform.position;
        dir = dir.normalized;
        proj.target = dir * 10f;
    }

}
