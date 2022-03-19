using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMovement pm;
    private void Start()
    {
        pm = GetComponent<PlayerMovement>();   
    }

    public void HandleLeft (InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            pm.MoveLeft();
        }
    }

    public void HandleRight (InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            pm.MoveRight();
        }
    }

    public void HandleAcross (InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            pm.MoveAcross();
        }
    }
}
