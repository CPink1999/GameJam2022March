using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFacePlayer : MonoBehaviour
{
    [SerializeField] bool reverse;

    private GameObject playerRef;
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 dirToCenter = playerRef.transform.position - transform.position;
        if (reverse)
        {
            dirToCenter *= -1;
        }
        Quaternion rotation = Quaternion.LookRotation(dirToCenter, Vector3.up);
        transform.rotation = rotation;
    }
}
