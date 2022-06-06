using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMeleeAttackController : BaseAttackController
{
    [Header("Melee properties")]
    [SerializeField]
    private float attackRange;

    public override IEnumerator PerformAttack()
    {
        attackInProgress = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackWindupTime);
        ParticleMaster.Instance.SpawnParticles(transform.position, Quaternion.Euler(transform.forward), ParticleType.StraightShot);
        animator.ResetTrigger("Attack");

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit info, attackRange, 3))
        {
            var health = info.transform.GetComponent<HealthController>();
            if (health != null)
            {
                if (isPlayer)
                {
                    GameState.DealDamage(modifiedDamage);
                }
                health.TakeDamage(modifiedDamage, -(target.transform.position - transform.position).normalized);
            }
        }
        yield return new WaitForSeconds(attackWinddownTime);

        attackInProgress = false;
    }
}
