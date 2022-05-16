using System;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : BaseEntity
{
    [SerializeField]
    private BaseAttackController attackController;

    [Header("Distances")]
    [SerializeField]
    private SqrDistance attackDist;
    [SerializeField]
    private SqrDistance retreatDist;
    [SerializeField]
    private SqrDistance stopRetreatDist;

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

        stateMachine.AddTransition(chase, attack, () => { return distanceToPlayerSqr < attackDist.DistanceSqr; });
        stateMachine.AddTransition(attack, retreat, () => { return distanceToPlayerSqr < retreatDist.DistanceSqr && !attackController.attackInProgress; });
        stateMachine.AddTransition(attack, chase, () => { return distanceToPlayerSqr > attackDist.DistanceSqr && !attackController.attackInProgress; });
        stateMachine.AddTransition(retreat, chase, () => { return distanceToPlayerSqr > stopRetreatDist.DistanceSqr; });

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

    public override void UpdateDistanceToPlayer()
    {
        distanceToPlayerSqr = (transform.position - attackController.target.transform.position).sqrMagnitude;
        EntityManager.Instance.UpdateClosestEnemy(distanceToPlayerSqr, this);
    }

    protected override void TakeDamage(float damage, Vector3 dir)
    {

    }

    protected override void Despawn()
    {
        EntityManager.Instance.RemoveEntity(this);
        if (isPlayer)
        {
            EntityManager.Instance.PlayerHasDied();
        }
        ParticleMaster.Instance.SpawnParticles(transform.position, ParticleType.DeathSkull);
        stateMachine.SafeDestroy();
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
            return distanceToPlayerSqr;
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (isPlayer)
        {
            return;
        }
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, attackDist.Distance, 1f);

        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, retreatDist.Distance, 1f);

        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, stopRetreatDist.Distance, 1f);
    }
#endif
}