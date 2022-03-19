using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    [SerializeField] Slider slider;

    private void Start ()
    {
        slider.onValueChanged.AddListener(delegate { UpdateDifficulty();  });
    }

    private void UpdateDifficulty ()
    {
        PlayerPrefs.SetInt("Difficulty", (int) slider.value);
    }
}
