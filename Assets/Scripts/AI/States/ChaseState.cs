using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    private NavMeshAgent agent;
    private Transform target;

    public ChaseState(NavMeshAgent navMeshAgent)
    {
        agent = navMeshAgent;
    }

    private void PlayerController_PlayerControllerChanged(PlayerController player)
    {
        target = player.transform;
    }

    public void OnEnter()
    {
        Debug.Log("Chase");
        target = PlayerController.Instance.transform;
        PlayerController.PlayerControllerChanged += PlayerController_PlayerControllerChanged;
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }

    public void OnExit()
    {
        PlayerController.PlayerControllerChanged -= PlayerController_PlayerControllerChanged;
        agent.isStopped = true;
    }

    public void Tick()
    {
        agent.SetDestination(target.position);
    }
}
