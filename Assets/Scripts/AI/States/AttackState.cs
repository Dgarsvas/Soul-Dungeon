using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
    BaseAttackController attackController;
    NavMeshAgent navMeshAgent;
    public AttackState(BaseAttackController attack, NavMeshAgent agent)
    {
        attackController = attack;
        navMeshAgent = agent;
    }

    public void OnEnter()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.isStopped = true;
        attackController.canAttack = true;
    }

    public void OnExit()
    {
        navMeshAgent.updateRotation = true;
        attackController.canAttack = false;
    }

    public void Tick()
    {
    }
}
