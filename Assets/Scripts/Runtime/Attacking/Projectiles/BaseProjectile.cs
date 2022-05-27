using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float lifetime;
    [SerializeField]
    protected bool isPlayerProjectile;

    private void Awake()
    {
        StartCoroutine(DestroyAfter(gameObject, lifetime));
    }

    private IEnumerator DestroyAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }

    public abstract void Init(Vector3 targetPos, float speed, float lifetime, float damage, bool isPlayer);

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision);
    }

    protected abstract void HandleCollision(Collision collision);
}
