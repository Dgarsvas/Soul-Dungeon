using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackController : BaseAttackController
{
    [Header("Melee properties")]
    [SerializeField]
    private float attackRange;

    public override IEnumerator PerformAttack()
    {
        attackInProgress = true;
        yield return null;
        Vector3 dir = target.transform.position - transform.position;
        if (dir.magnitude < attackRange)
        {
            target.TakeDamage(damage, -dir.normalized);
        }

        attackInProgress = false;
    }
}
