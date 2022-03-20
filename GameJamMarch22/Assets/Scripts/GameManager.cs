using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


/// <summary>A singleton which handles the game state</summary>
public class GameManager : MonoBehaviour
{
    [Header("Tag names")]
    [SerializeField] private string noteReaderTag = "NoteReader";
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string sceneTransitionerTag = "SceneTransitioner";

    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera startingCam;
    [SerializeField] private CinemachineVirtualCamera gameCam;

    [Header("Timing")]
    [SerializeField] private float cameraTransitionTime = 2f;
    [SerializeField] private float sceneTransitionTime = 1f;

    public static GameManager instance;

    private NoteReader noteReader;
    private PlayerInputHandler inputHandler;
    private Animator sceneTransitioner;

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
        sceneTransitioner = GameObject.FindGameObjectWithTag(sceneTransitionerTag).GetComponent<Animator>();

        inputHandler.enabled = false;

        startingCam.Priority = 100;

        StartGame();
    }

    private void OnEnable()
    {
        TakeDamage.OnDeath += GameOver;
    }

    private void OnDisable()
    {
        TakeDamage.OnDeath -= GameOver;
    }

    public void GameOver ()
    {
        StartCoroutine(GameOverCoroutine());
    }

    public IEnumerator GameOverCoroutine ()
    {
        noteReader.FadeOut(2f);
        yield return new WaitForSeconds(1f);

        sceneTransitioner.SetTrigger("FadeOut");

        yield return new WaitForSeconds(sceneTransitionTime);

        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
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
