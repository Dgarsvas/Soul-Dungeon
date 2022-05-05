using System;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : BaseEntity
{
    [SerializeField]
    private BaseAttackController attackController;

    [Header("Distances")]
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private float retreatDistance;
    [SerializeField]
    private float stopRetreatDistance;

    private float distanceToPlayer;

    public override void Start()
    {
        stateMachine = new StateMachine();

        ChaseState chase = new ChaseState(navMeshAgent);
        AttackState attack = new AttackState(attackController, navMeshAgent);
        RetreatState retreat = new RetreatState(navMeshAgent);
        stateMachine.AddTransition(chase, attack, () => { return distanceToPlayer < attackDistance; });
        stateMachine.AddTransition(attack, retreat, () => { return distanceToPlayer < retreatDistance && attackController.hasAttacked; });
        stateMachine.AddTransition(attack, chase, () => { return distanceToPlayer > attackDistance && attackController.hasAttacked; });
        stateMachine.AddTransition(retreat, chase, () => { return distanceToPlayer > stopRetreatDistance; });

        stateMachine.SetState(chase);
        base.Start();
    }

    private void Update()
    {
        if (!isDead && !isPlayer)
        {
            distanceToPlayer = (transform.position - PlayerController.Instance.transform.position).magnitude;
            stateMachine?.Tick();
        }
    }

    public override void TakeDamage(float damage, Vector3 dir)
    {
        health -= damage;

        if (health <= 0)
        {
            isDead = true;
        }
    }

    public override void Despawn()
    {
        Destroy(gameObject);
    }
}