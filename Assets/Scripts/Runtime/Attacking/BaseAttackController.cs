using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttackController : MonoBehaviour
{
    [HideInInspector]
    public bool canAttack;
    [HideInInspector]
    public bool attackInProgress;
    [HideInInspector]
    public HealthController target;

    protected float timer;

    [Header("General Properties")]
    [SerializeField]
    protected float reloadTime;
    [SerializeField]
    protected float modifiedReloadTime;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float attackWindupTime;
    [SerializeField]
    protected float attackWinddownTime;
    [SerializeField]
    protected Animator animator;

    private Coroutine attackCoroutine;

    public float DPS
    {
        get
        {
            return damage / modifiedReloadTime;
        }
    }

    private void Awake()
    {
        modifiedReloadTime = reloadTime;
    }

    private void Update()
    {
        if (canAttack)
        {
            transform.LookAt(target.transform);
            if (timer < 0)
            {
                attackCoroutine = StartCoroutine(PerformAttack());
                timer = modifiedReloadTime;
            }
        }

        timer -= Time.deltaTime;
    }

    public abstract IEnumerator PerformAttack();

    public virtual void CancelAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        attackInProgress = false;
    }
}
