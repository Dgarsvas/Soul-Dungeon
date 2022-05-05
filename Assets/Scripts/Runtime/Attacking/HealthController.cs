using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float health;


    public UnityEvent OnDeath;
    public UnityEvent<float, Vector3> OnDamageTaken;


    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {

        }
    }
}
