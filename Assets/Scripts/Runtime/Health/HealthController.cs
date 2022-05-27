using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    public float defaultHealth;

    [SerializeField]
    public float currentMaxHealth;

    public float Health
    {
        get
        {
            return health;
        }
    }

    [HideInInspector]
    public UnityEvent OnDeath;
    [HideInInspector]
    public UnityEvent<float, Vector3> OnDamageTaken;

    private void Start()
    {
        health = defaultHealth;
        currentMaxHealth = defaultHealth;
        HealthBarController.Instance?.TrackHealth(this);
    }

    private void OnDestroy()
    {
        HealthBarController.Instance?.StopTrackingHealth(this);
    }

    public virtual void TakeDamage(float damage, Vector3 dir)
    {
        health -= damage;
        OnDamageTaken?.Invoke(damage, dir);
        if (health <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void ModifyHealth(float healthModifier)
    {
        health = defaultHealth * health / currentMaxHealth * healthModifier;
        currentMaxHealth = defaultHealth * healthModifier;
        TakeDamage(0, Vector3.zero);
    }
}
