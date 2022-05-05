using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementController : MonoBehaviour
{
    private const float TARGET_DISTANCE = 0.6f;
    private readonly Quaternion offset = Quaternion.AngleAxis(-45, Vector3.forward);

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private float speedModifier = 2f;

    private bool joystickIsDown;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        speedModifier = agent.speed;
        Joystick.JoystickDown += JoystickDown;
        Joystick.JoystickUp += JoystickUp;
        agent.updateRotation = false;
    }

    private void JoystickUp()
    {
        joystickIsDown = false;
        agent.isStopped = true;
    }

    private void JoystickDown()
    {
        joystickIsDown = true;
        agent.isStopped = false;
    }

    private void OnDestroy()
    {
        Joystick.JoystickDown -= JoystickDown;
        Joystick.JoystickUp -= JoystickUp;
    }

    private void Update()
    {
        if (joystickIsDown)
        {
            Vector2 dir = offset * Joystick.Instance.Direction;
            agent.speed = dir.magnitude * speedModifier;
            dir = dir.normalized * TARGET_DISTANCE;
            agent.SetDestination(transform.position + new Vector3(dir.x, 0f, dir.y));
            transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.y));
        }
    }
}