using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    public delegate void UpdateDifficultyAction (int amount);
    public static event UpdateDifficultyAction OnUpdateDifficulty;

    [SerializeField] private Text text;
    [SerializeField] private int maxHealth;
    [SerializeField] private int defaultHealth = 3;

    private int health;

    private void Awake ()
    {
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            UpdateDifficulty(PlayerPrefs.GetInt("Difficulty"));
        } else
        {
            UpdateDifficulty(defaultHealth);
        }
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI ()
    {
        text.text = health.ToString();
    }

    private void SaveToPlayerPrefs ()
    {
        PlayerPrefs.SetInt("Difficulty", health);
    }

    public void UpdateDifficulty (int amount)
    {
        health = Mathf.Clamp(health + amount, 1, maxHealth);
        SaveToPlayerPrefs();
        OnUpdateDifficulty?.Invoke(health);
    }
}
