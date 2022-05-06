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

    private float distanceToPlayer = float.MaxValue;

    ChaseState chase;
    AttackState attack;
    RetreatState retreat;
    IdleState idle;

    public override void Start()
    {
        stateMachine = new StateMachine();

        chase = new ChaseState(navMeshAgent);
        attack = new AttackState(attackController, navMeshAgent);
        retreat = new RetreatState(navMeshAgent);
        idle = new IdleState(navMeshAgent);

        stateMachine.AddTransition(chase, attack, () => { return distanceToPlayer < attackDistance; });
        stateMachine.AddTransition(attack, retreat, () => { return distanceToPlayer < retreatDistance && attackController.hasAttacked; });
        stateMachine.AddTransition(attack, chase, () => { return distanceToPlayer > attackDistance && attackController.hasAttacked; });
        stateMachine.AddTransition(retreat, chase, () => { return distanceToPlayer > stopRetreatDistance; });

        stateMachine.SetState(chase);
        attackController.target = EntityManager.Instance.GetPlayer();
        base.Start();
    }

    private void Update()
    {
        if (isActive && !isPlayer)
        {
            UpdateDistanceToPlayer();
            stateMachine?.Tick();
        }
    }

    private void UpdateDistanceToPlayer()
    {
        distanceToPlayer = (transform.position - attackController.target.position).magnitude;
        EntityManager.Instance.UpdateClosestEnemy(distanceToPlayer, transform);
    }

    protected override void TakeDamage(float damage, Vector3 dir)
    {

    }

    protected override void Despawn()
    {
        if (isPlayer)
        {
            EntityManager.Instance.PlayerDied();
        }
        Destroy(gameObject);
    }

    public override void ActivateEntity(bool state)
    {
        base.ActivateEntity(state);
        if (!state)
        {
            stateMachine.SetState(idle);
        }
    }

    protected override void TargetChanged(Transform target)
    {
        attackController.target = target;
    }

    public override float GetDistanceToPlayer()
    {
        if (isPlayer)
        {
            return float.MaxValue;
        }
        else
        {
            return distanceToPlayer;
        }
    }
}