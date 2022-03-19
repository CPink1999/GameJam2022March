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

    [SerializeField] private int maxHealth = 5;
    [SerializeField] private string damageTag = "GivesDamage";

    public int Health { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(damageTag))
        {
            OnTakeDamage?.Invoke();
            Health -= 1;
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
