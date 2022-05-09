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
    protected int damage;

    private void Update()
    {
        if (canAttack)
        {
            transform.LookAt(target.transform);
            if (timer < 0)
            {
                PerformAttack();
                timer = reloadTime;
            }
        }

        timer -= Time.deltaTime;
    }

    public abstract IEnumerator PerformAttack();
}
