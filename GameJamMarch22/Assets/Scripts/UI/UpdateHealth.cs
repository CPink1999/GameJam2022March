using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UpdateHealth : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private Text text;
    private TakeDamage playerTakeDamage;

    void Start()
    {
        text = GetComponent<Text>();
        playerTakeDamage = GameObject.FindGameObjectWithTag(playerTag).GetComponent<TakeDamage>();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI ()
    {
        text.text = playerTakeDamage.Health.ToString();
    }
}
