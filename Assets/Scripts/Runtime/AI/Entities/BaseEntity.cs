using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseEntity : MonoBehaviour
{
    protected bool isDead;

    [Header("References")]
    [SerializeField]
    protected NavMeshAgent navMeshAgent;
    protected StateMachine stateMachine;
    protected HealthController healthController;


    public bool isPlayer;


    public virtual void Start()
    {
        healthController.OnDamageTaken.AddListener(TakeDamage);
        healthController.OnDeath.AddListener(Despawn);

        EntityManager.Instance.AddEntity(this);
    }

    protected abstract void TakeDamage(float damage, Vector3 dir);

    protected abstract void Despawn();
}
