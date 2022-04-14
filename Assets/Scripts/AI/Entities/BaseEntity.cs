using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEntity : MonoBehaviour
{
    [SerializeField]
    protected float health;
    protected bool isDead;

    [Header("References")]
    [SerializeField]
    protected NavMeshAgent navMeshAgent;
    protected StateMachine stateMachine;

    public abstract void TakeDamage(float damage, Vector3 direction);

    public abstract void Despawn();
}