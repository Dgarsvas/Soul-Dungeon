using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseEntity : MonoBehaviour
{
    protected bool isActive;

    [Header("References")]
    [SerializeField]
    protected NavMeshAgent navMeshAgent;
    protected StateMachine stateMachine;
    [SerializeField]
    public HealthController healthController;

    [HideInInspector]
    public bool isPlayer;

    public virtual void Start()
    {
        healthController.OnDamageTaken.AddListener(TakeDamage);
        healthController.OnDeath.AddListener(Despawn);
        EntityManager.Instance.OnPlayerChanged.AddListener(TargetChanged);

        EntityManager.Instance.AddEntity(this);
    }

    private void OnDestroy()
    {
        EntityManager.Instance.OnPlayerChanged.RemoveListener(TargetChanged);
        healthController.OnDamageTaken.RemoveListener(TakeDamage);
        healthController.OnDeath.RemoveListener(Despawn);
    }

    protected abstract void TakeDamage(float damage, Vector3 dir);

    protected abstract void TargetChanged(BaseEntity target);

    protected abstract void Despawn();

    public abstract float GetDistanceToPlayer();

    public virtual void ActivateEntity(bool state)
    {
        isActive = state;
    }
}
