using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : BaseAttackController
{
    private void Awake()
    {
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
        canAttack = true;
    }

    private void JoystickDown()
    {
        canAttack = false;
    }

    private void Update()
    {
        if (canAttack)
        {
            if (timer < 0)
            {
                Attack();
                timer = reloadTime;
            }
        }

        timer -= Time.deltaTime;
    }

    public override void Attack()
    {
        Debug.Log("attack");
    }
}