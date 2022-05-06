using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController : BaseAttackController
{
    [SerializeField]
    private GameObject projectilePrefab;

    public override void Attack()
    {
        GameObject spawned = Instantiate(projectilePrefab, transform.position + transform.forward, Quaternion.identity);
        spawned.GetComponent<BaseProjectile>().Init(transform.position);
    }
}
