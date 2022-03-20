using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


/// <summary>A singleton which handles the game state</summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private string noteReaderTag = "NoteReader";
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private CinemachineVirtualCamera startingCam;
    [SerializeField] private CinemachineVirtualCamera gameCam;
    [SerializeField] private float cameraTransitionTime = 2f;

    public static GameManager instance;

    private NoteReader noteReader;
    private PlayerInputHandler inputHandler;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }

        noteReader = GameObject.FindGameObjectWithTag(noteReaderTag).GetComponent<NoteReader>();
        inputHandler = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerInputHandler>();

        inputHandler.enabled = false;

        startingCam.Priority = 100;

        StartGame();
    }

    public void StartGame ()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        gameCam.Priority = 100;
        startingCam.Priority = 0;
        yield return new WaitForSeconds(cameraTransitionTime);
        noteReader.BeginRead();
        inputHandler.enabled = true;
        
    }
}
