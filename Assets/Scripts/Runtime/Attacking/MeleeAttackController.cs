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
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackWindupTime);
        ParticleMaster.Instance.SpawnParticles(new Vector3(transform.position.x, -0.5f, transform.position.z), ParticleType.HitSmokePuff);
        animator.ResetTrigger("Attack");

        Vector3 dir = target.transform.position - transform.position;
        if (dir.magnitude < attackRange)
        {
            if (isPlayer)
            {
                GameState.DealDamage(modifiedDamage);
            }
            target.TakeDamage(modifiedDamage, -dir.normalized);
        }
        yield return new WaitForSeconds(attackWinddownTime);

        attackInProgress = false;
    }
}
