using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>Starts moving towards a given target, destroying itself upon either collision or destination reached</summary>
public class Projectile : MonoBehaviour
{
    public bool isParryable;
    public float speed;
    public Vector3 target;
    public bool destroyOnDestination = true;

    void Update()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, target)) > 0.04f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            if (destroyOnDestination)
            {
                CleanUp();
            }
        }
    }

    private void CleanUp ()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CleanUp();
    }
}
