using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class StraightProjectile : BaseProjectile
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Collider col;
    [SerializeField]
    private int damage;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public override void Init(Vector3 targetPos)
    {
        rb.AddForce(-(targetPos - transform.position).normalized * speed, ForceMode.VelocityChange);
    }

    protected override void HandleCollision(Collision collision)
    {
        ContactPoint point = collision.GetContact(0);
        ParticleMaster.Instance.SpawnParticles(point.point, Quaternion.Euler(point.normal), ParticleType.SimpleHit);

        if (collision.gameObject.CompareTag("Entity"))
        {
            collision.gameObject.GetComponent<HealthController>().TakeDamage(damage, -point.normal);
        }
        
        Destroy(gameObject);
    }
}
