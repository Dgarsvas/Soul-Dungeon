using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttackController : MonoBehaviour
{
    public bool canAttack;
    public bool hasAttacked;

    [SerializeField]
    protected float reloadTime;
    protected float timer;

    private void Update()
    {
        if (canAttack)
        {
            transform.LookAt(PlayerController.Instance.transform);
            if (timer < 0)
            {
                Attack();
                hasAttacked = true;
                timer = reloadTime;
            }
        }

        timer -= Time.deltaTime;
    }

    public abstract void Attack();
}
