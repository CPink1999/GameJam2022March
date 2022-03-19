using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform testTransform;
    [SerializeField] private float invisibilityTime = 1f;
    [SerializeField] private float startMoveDelay = 1f;
    [SerializeField] private float finishMoveDelay = 0.3f;

    [SerializeField] private GameObject playerRig;

    private PillarManager pillars;
    private int currentPillarIndex;

    private void Start()
    {
        pillars = GameObject.FindGameObjectWithTag("Pillars").GetComponent<PillarManager>();
    }

    public void MoveLeft ()
    {
        currentPillarIndex = (currentPillarIndex + pillars.Count - 1) % pillars.Count;
        MoveToCurrentPillar();
    }

    public void MoveRight ()
    {
        currentPillarIndex = (currentPillarIndex + 1) % pillars.Count;
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

    private IEnumerator PerformMovement (Transform location)
    {
        yield return new WaitForSeconds(startMoveDelay);
        playerRig.SetActive(false);
        transform.position = location.position;
        yield return new WaitForSeconds(invisibilityTime);
        playerRig.SetActive(true);
        yield return new WaitForSeconds(finishMoveDelay);
    }
}
