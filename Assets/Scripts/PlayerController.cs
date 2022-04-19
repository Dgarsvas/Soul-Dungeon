using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public delegate void JoystickInteractionHandler(PlayerController player);
    public static event JoystickInteractionHandler PlayerControllerChanged;
    public BaseAttackController attackController;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            PlayerControllerChanged?.Invoke(this);
        }

        Instance = this;
        Joystick.JoystickDown += JoystickDown;
        Joystick.JoystickUp += JoystickUp;
    }

    private void OnDestroy()
    {
        Joystick.JoystickDown -= JoystickDown;
        Joystick.JoystickUp -= JoystickUp;
    }

    private void JoystickUp()
    {
        attackController.canAttack = true;
    }

    private void JoystickDown()
    {
        attackController.canAttack = false;
    }
}