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
    protected float defaultReloadTime;
    [SerializeField]
    protected float modifiedReloadTime;
    [SerializeField]
    protected float defaultDamage;
    [SerializeField]
    protected float modifiedDamage;
    [SerializeField]
    protected float attackWindupTime;
    [SerializeField]
    protected float attackWinddownTime;
    [SerializeField]
    protected Animator animator;

    private Coroutine attackCoroutine;

    public bool isPlayer;

    public float DPS
    {
        get
        {
            return defaultDamage / defaultReloadTime;
        }
    }

    private void Awake()
    {
        ModifyDamageAndReload();
    }

    public void ModifyDamageAndReload(float damage = 1f, float reload = 1f)
    {
        modifiedDamage = defaultDamage * damage;
        modifiedReloadTime = defaultReloadTime / reload;
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
