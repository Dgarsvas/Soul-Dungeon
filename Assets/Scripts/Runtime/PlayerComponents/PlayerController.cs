using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public delegate void PlayerControllerChangedEvent(PlayerController player);
    public static event PlayerControllerChangedEvent PlayerControllerChanged;
    public BaseAttackController attackController;


    private const float TARGET_DISTANCE = 0.6f;
    private readonly Quaternion offset = Quaternion.AngleAxis(-45, Vector3.forward);

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private float speedModifier = 2f;

    private bool joystickIsDown;
    private bool canAttack;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        agent = GetComponent<NavMeshAgent>();
        speedModifier = agent.speed;
        agent.updateRotation = false;

        PlayerControllerChanged?.Invoke(this);
        Instance = this;
        Joystick.JoystickDown += JoystickDown;
        Joystick.JoystickUp += JoystickUp;
        EnableAttack(true);
    }

    private void OnDestroy()
    {
        Joystick.JoystickDown -= JoystickDown;
        Joystick.JoystickUp -= JoystickUp;
    }

    private void JoystickUp()
    {
        joystickIsDown = false;
        agent.isStopped = true;
        if (canAttack)
        {
            attackController.canAttack = true;
        }
    }

    private void JoystickDown()
    {
        joystickIsDown = true;
        agent.isStopped = false;
        attackController.canAttack = false;
    }

    private void Update()
    {
        if (joystickIsDown)
        {
            Vector2 dir = offset * Joystick.Instance.Direction;
            agent.speed = dir.magnitude * speedModifier;
            dir = dir.normalized * TARGET_DISTANCE;
            if (dir != Vector2.zero)
            {
                agent.SetDestination(transform.position + new Vector3(dir.x, 0f, dir.y));
                transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.y));
            }
        }
    }

    public void EnableAttack(bool state)
    {
        canAttack = state;
        if (!state)
        {
            attackController.canAttack = false;
        }
    }
}