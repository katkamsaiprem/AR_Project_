using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event Action OnShootPressed;
    public event Action OnReloadPressed;

    private void Update()
    {
        // Mouse input
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnShootPressed?.Invoke();
        }

        //Keyboard input
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            OnReloadPressed?.Invoke();//? is used to find that event is not null
        }
        // Touch input
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            OnShootPressed?.Invoke();
        }

         
    }

    public void TriggerReload()
    {
        OnReloadPressed?.Invoke();// if OnReloadPressed != null then invoke
    }
}
