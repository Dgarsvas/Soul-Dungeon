using UnityEngine;
using UnityEngine.AI;

public class IdleState : IState
{
    private NavMeshAgent agent;

    public IdleState(NavMeshAgent navMeshAgent)
    {
        agent = navMeshAgent;
    }

    public void OnEnter()
    {
        agent.isStopped = true;
    }

    public void OnExit()
    {
    }

    public void SafeDestroy()
    {
    }

    public void Tick()
    {
    }
}
