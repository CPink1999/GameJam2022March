using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>Contains method relating to moving the player within the specific spots that the player can travel to.</summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float invisibilityTime = 0.2f;
    [SerializeField] private float startMoveDelay = 0.2f;
    [SerializeField] private float finishMoveDelay = 0.2f;

    [Header("References")]
    [SerializeField] private GameObject playerRig;

    private PillarManager pillars;
    private int currentPillarIndex;

    private void Start()
    {
        pillars = GameObject.FindGameObjectWithTag("Pillars").GetComponent<PillarManager>();
        RotateTowardsPillarCenter();
    }

    public void MoveLeft ()
    {
        currentPillarIndex = (currentPillarIndex + 1) % pillars.Count;
        MoveToCurrentPillar();
    }

    public void MoveRight ()
    {
        currentPillarIndex = (currentPillarIndex + pillars.Count - 1) % pillars.Count;
        MoveToCurrentPillar();
    }

    public void MoveAcross ()
    {
        currentPillarIndex = (currentPillarIndex + (pillars.Count / 2)) % pillars.Count;
        MoveToCurrentPillar();
    }

    private void MoveToCurrentPillar ()
    {
        Move(pillars.GetPillar(currentPillarIndex).transform);
    }

    private void Move (Transform location)
    {
        StartCoroutine(PerformMovement(location));
    }

    private void RotateTowardsPillarCenter ()
    {
        Vector3 dirToCenter = pillars.Center.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dirToCenter, Vector3.up);
        transform.rotation = rotation;
    }

    private IEnumerator PerformMovement (Transform location)
    {
        yield return new WaitForSeconds(startMoveDelay);
        playerRig.SetActive(false);
        transform.position = location.position;
        RotateTowardsPillarCenter();
        yield return new WaitForSeconds(invisibilityTime);
        playerRig.SetActive(true);
        yield return new WaitForSeconds(finishMoveDelay);
    }
}
