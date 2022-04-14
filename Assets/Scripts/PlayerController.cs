using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;
    public delegate void JoystickInteractionHandler(PlayerController player);
    public static event JoystickInteractionHandler PlayerControllerChanged;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            PlayerControllerChanged?.Invoke(this);
        }

        Instance = this;
    }
}
