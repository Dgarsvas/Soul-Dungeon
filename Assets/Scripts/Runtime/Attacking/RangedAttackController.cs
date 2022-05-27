using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController : BaseAttackController
{
    [Header("Projectile properties")]
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    protected float projectileSpeed;
    [SerializeField]
    protected float projectileLifetime;

    public override IEnumerator PerformAttack()
    {
        attackInProgress = true;

        animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(attackWindupTime);
        animator.ResetTrigger("Shoot");

        GameObject spawned = Instantiate(projectilePrefab, transform.position + transform.forward, Quaternion.identity);
        spawned.GetComponent<BaseProjectile>().Init(transform.position, projectileSpeed, projectileLifetime, modifiedDamage, isPlayer);
        yield return new WaitForSeconds(attackWinddownTime);
       
        attackInProgress = false;
    }
}
