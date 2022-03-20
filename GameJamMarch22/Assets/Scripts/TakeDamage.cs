using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages player health. Decrements health whenever the object collides with another with the tag damageTag, and destroys the gameobject when health hits 0. Triggers events on take damage and on death.
/// </summary>
public class TakeDamage : MonoBehaviour
{
    public delegate void DamageAction();
    public static event DamageAction OnTakeDamage;
    public delegate void DeathAction();
    public static event DeathAction OnDeath;

    [SerializeField] private string damageTag = "GivesDamage";
    [SerializeField] private int defaultMaxHealth = 3;

    public int Health { get; private set; }

    public void UpdateHealth (int amount)
    {
        Health = amount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(damageTag))
        {
            OnTakeDamage?.Invoke();
            Health = (int) Mathf.Clamp(Health - 1, 0, Mathf.Infinity);
            if (Health <= 0)
            {
                HandleDeath();
            }
        }
    }

    private void HandleDeath ()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
