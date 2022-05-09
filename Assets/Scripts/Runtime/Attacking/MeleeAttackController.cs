using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackController : BaseAttackController
{
    [Header("Melee properties")]
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackWindupTime;

    public override IEnumerator PerformAttack()
    {
        attackInProgress = true;
        yield return new WaitForSeconds(attackWindupTime);
        Vector3 dir = target.transform.position - transform.position;
        if (dir.magnitude < attackRange)
        {
            target.TakeDamage(damage, -dir.normalized);
        }

        attackInProgress = false;
    }
}
