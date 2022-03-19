using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///   <para>Spawns projectiles using its given prefabs</para>
/// </summary>
public class ProjectileSpawner : MonoBehaviour
{
    enum modes
    {
        Offset,
        Origin,
    }

    [SerializeField] private GameObject parryableProjectilePrefab;
    [SerializeField] private GameObject nonParryableProjectilePrefab;
    [Tooltip("Adds an offset to the position that the projectile heads towards.")] [SerializeField] private Vector3 targetOffset;

    [Header("Timing")]
    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float randomSpawnDelay = 1f;

    [Space]

    [Header("Mode Selection")]
    [SerializeField] modes mode;

    [Space]

    [Header("Offset Mode")]
    [SerializeField] private Vector3 spawnOffset = new Vector3(0f, 20f, 0f);

    [Header("Origin Mode")]
    [SerializeField] Transform origin;

    private PillarManager pillars;

    private void Start()
    {
        pillars = GameObject.FindGameObjectWithTag("Pillars").GetComponent<PillarManager>();
        StartCoroutine(SpawnRandomProjectiles());
    }

    public void SpawnParryable (Vector3 target)
    {
        SpawnParryable(target + spawnOffset, target);
    }

    public void SpawnParryable(Vector3 origin, Vector3 target)
    {
        Projectile newProjectile = Instantiate(parryableProjectilePrefab).GetComponent<Projectile>();
        newProjectile.transform.position = origin;
        newProjectile.speed = initialSpeed;
        newProjectile.target = target + targetOffset;
    }

    private IEnumerator SpawnRandomProjectiles ()
    {
        while (true)
        {
            yield return new WaitForSeconds(randomSpawnDelay);
            for (int i = 0; i < Random.Range(0, pillars.Count - 1); i++)
            {
                int randomIndex = Random.Range(0, pillars.Count);
                Transform randomPillar = pillars.GetPillar(randomIndex);
                if (mode == modes.Offset)
                {
                    SpawnParryable(randomPillar.position);
                }
                else if (mode == modes.Origin)
                {
                    SpawnParryable(origin.position, randomPillar.position);
                }
            }
        }
    }
}