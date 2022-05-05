using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseEntity : MonoBehaviour
{
    [SerializeField]
    protected float health;
    protected bool isDead;

    [Header("References")]
    [SerializeField]
    protected NavMeshAgent navMeshAgent;
    protected StateMachine stateMachine;

    public bool isPlayer;


    public virtual void Start()
    {
        EntityManager.Instance.AddEntity(this);
    }

    public abstract void TakeDamage(float damage, Vector3 direction);

    public abstract void Despawn();
}
