using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///   <para>Constantly rotates to face the player with the "Player" tag. If reverse is true, it constantly faces away from the player.</para>
/// </summary>
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
        if (playerRef != null)
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
}
