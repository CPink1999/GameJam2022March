using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>Receives player input events and calls methods on the PlayerMovement class</summary>
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(ParryManager))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMovement playerManager;
    private ParryManager parryManager;

    private void Start()
    {
        playerManager = GetComponent<PlayerMovement>();
        parryManager = GetComponent<ParryManager>();
    }

    public void HandleLeft (InputAction.CallbackContext value)
    {
        if (enabled)
        {
            if (value.performed)
            {
                playerManager.MoveLeft();
            }
        }
    }

    public void HandleRight (InputAction.CallbackContext value)
    {
        if (enabled)
        {
            if (value.performed)
            {
                playerManager.MoveRight();
            }
        }
    }

    public void HandleAcross (InputAction.CallbackContext value)
    {
        if (enabled)
        {
            if (value.performed)
            {
                playerManager.MoveAcross();
            }
        }
    }

    public void HandleParry (InputAction.CallbackContext value)
    {
        if (enabled)
        {
            if (value.performed)
            {
                parryManager.Parry();
            }
        }
    }
}
