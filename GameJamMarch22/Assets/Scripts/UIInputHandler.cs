using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputHandler : MonoBehaviour
{
    public void HandleStart(InputAction.CallbackContext value)
    {
        if (enabled)
        {
            if (value.performed)
            {
                GameManager.instance.StartGame();
            }
        }
    }
}
