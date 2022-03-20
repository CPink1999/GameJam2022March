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
    [SerializeField] private string instructionsTag = "Instructions";
    [SerializeField] private string gameUITag = "GameUI";

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
    private Animator instructions;
    private Animator gameUI;
    private TakeDamage playerTakeDamage;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        playerTakeDamage = GameObject.FindGameObjectWithTag(playerTag).GetComponent<TakeDamage>();
        UpdateDifficulty(PlayerPrefs.GetInt("Difficulty")); // We have to set it here as well because this method fires at the same time the updateHealth event does.
        noteReader = GameObject.FindGameObjectWithTag(noteReaderTag).GetComponent<NoteReader>();
        inputHandler = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerInputHandler>();
        sceneTransitioner = GameObject.FindGameObjectWithTag(sceneTransitionerTag).GetComponent<Animator>();
        instructions = GameObject.FindGameObjectWithTag(instructionsTag).GetComponent<Animator>();
        gameUI = GameObject.FindGameObjectWithTag(gameUITag).GetComponent<Animator>();

        inputHandler.enabled = false;

        startingCam.Priority = 100;
    }

    private void OnEnable()
    {
        TakeDamage.OnDeath += GameOver;
        DifficultySlider.OnUpdateDifficulty += UpdateDifficulty;
    }

    private void OnDisable()
    {
        TakeDamage.OnDeath -= GameOver;
        DifficultySlider.OnUpdateDifficulty -= UpdateDifficulty;
    }

    private void UpdateDifficulty (int amount)
    {
        if (playerTakeDamage != null)
        {
            playerTakeDamage.UpdateHealth(amount);
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    public IEnumerator GameOverCoroutine()
    {
        noteReader.FadeOut(2f);
        yield return new WaitForSeconds(1f);

        sceneTransitioner.SetTrigger("FadeOut");

        yield return new WaitForSeconds(sceneTransitionTime);

        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        instructions.SetTrigger("FadeOut");
        gameCam.Priority = 100;
        startingCam.Priority = 0;
        yield return new WaitForSeconds(cameraTransitionTime);
        gameUI.SetTrigger("FadeIn");
        noteReader.BeginRead();
        inputHandler.enabled = true;

    }
}
