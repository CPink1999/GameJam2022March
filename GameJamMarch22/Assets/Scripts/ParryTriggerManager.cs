using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryTriggerManager : MonoBehaviour
{
    public delegate void ParryAction();
    public static event ParryAction OnParry;
    public delegate void ParrySequenceAction();
    public static event ParrySequenceAction OnParrySequence;

    [SerializeField] private float speedMultiplier = 3f;
    [SerializeField] private float successiveParryThreshold = 3;
    [SerializeField] private float successiveParryTime = 0.5f;

    private int successiveParries;
    private float lastSuccessfulParry;

    private void OnTriggerEnter(Collider other)
    {
        Projectile proj = other.transform.parent.GetComponent<Projectile>();

        if (proj != null && proj.isParryable)
        {
            HandleSuccessfulParry(proj);
        } else
        {
            successiveParries = 0;
        }
    }

    private void HandleSuccessfulParry(Projectile proj)
    {
        if ((Time.time - lastSuccessfulParry) <= successiveParryTime)
        {
            successiveParries += 1;
        }
        else
        {
            successiveParries = 1;
        }

        OnParry?.Invoke();
        if (successiveParries >= successiveParryThreshold)
        {
            OnParrySequence?.Invoke();
        }
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Vector3 dir = (proj.transform.position + randomOffset) - transform.position;
        dir = dir.normalized;
        proj.target = dir * 10f;
        proj.speed *= speedMultiplier;
        lastSuccessfulParry = Time.time;
    }
}
