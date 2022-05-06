using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RetreatState : IState
{
    private NavMeshAgent agent;
    private Transform target;

    public RetreatState(NavMeshAgent navMeshAgent)
    {
        agent = navMeshAgent;
    }

    private void PlayerController_PlayerControllerChanged(PlayerController player)
    {
        target = player.transform;
    }

    public void OnEnter()
    {
        target = PlayerController.Instance.transform;
        PlayerController.PlayerControllerChanged += PlayerController_PlayerControllerChanged;
        agent.isStopped = false;
        agent.SetDestination(GetRetreatPos());
    }

    private Vector3 GetRetreatPos()
    {
        return agent.transform.position + (agent.transform.position - target.position).normalized;
    }

    public void OnExit()
    {
        PlayerController.PlayerControllerChanged -= PlayerController_PlayerControllerChanged;
        agent.isStopped = true;
    }

    public void Tick()
    {
        agent.SetDestination(GetRetreatPos());
    }
}

