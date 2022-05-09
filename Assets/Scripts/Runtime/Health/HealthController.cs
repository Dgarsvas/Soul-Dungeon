using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    public float startingHealth;

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
        health = startingHealth;
        HealthBarController.Instance?.TrackHealth(this);
    }

    private void OnDestroy()
    {
        HealthBarController.Instance?.StopTrackingHealth(this);
    }

    public virtual void TakeDamage(int damage, Vector3 dir)
    {
        health -= damage;
        OnDamageTaken?.Invoke(damage, dir);
        if (health <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}
