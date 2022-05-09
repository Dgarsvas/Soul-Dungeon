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

    private ChaseState chase;
    private AttackState attack;
    private RetreatState retreat;
    private IdleState idle;

    public override void Start()
    {
        stateMachine = new StateMachine();

        chase = new ChaseState(navMeshAgent);
        attack = new AttackState(attackController, navMeshAgent);
        retreat = new RetreatState(navMeshAgent);
        idle = new IdleState(navMeshAgent);

        stateMachine.AddTransition(chase, attack, () => { return distanceToPlayer < attackDistance; });
        stateMachine.AddTransition(attack, retreat, () => { return distanceToPlayer < retreatDistance && attackController.attackInProgress; });
        stateMachine.AddTransition(attack, chase, () => { return distanceToPlayer > attackDistance && attackController.attackInProgress; });
        stateMachine.AddTransition(retreat, chase, () => { return distanceToPlayer > stopRetreatDistance; });

        stateMachine.SetState(chase);
        attackController.target = EntityManager.Instance.GetPlayer().healthController;
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
        distanceToPlayer = (transform.position - attackController.target.transform.position).magnitude;
        EntityManager.Instance.UpdateClosestEnemy(distanceToPlayer, this);
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

    protected override void TargetChanged(BaseEntity target)
    {
        attackController.target = target.healthController;
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