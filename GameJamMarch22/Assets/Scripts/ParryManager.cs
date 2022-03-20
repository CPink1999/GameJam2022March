using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryManager : MonoBehaviour
{
    [SerializeField] private float parryLength = 1f;
    [SerializeField] private BoxCollider box;

    private void Start()
    {
        box.enabled = false;
    }

    public void Parry ()
    {
        StartCoroutine(DoParry());
    }

    private IEnumerator DoParry()
    {
        box.enabled = true;
        Debug.Log("ENABLED");
        yield return new WaitForSeconds(parryLength);
        Debug.Log("DISABLED");
        box.enabled = false;
    }
}
