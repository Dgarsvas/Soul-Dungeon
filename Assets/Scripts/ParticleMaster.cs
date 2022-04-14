using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{
    SimpleHit
}

public class ParticleMaster : MonoBehaviour
{
    public static ParticleMaster Instance
    {
        get;
        private set;
    }

    [SerializeField]
    private GameObject[] particlePrefabs;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void SpawnParticles(Vector3 position, Quaternion quaternion, ParticleType type)
    {
        GameObject spawned = Instantiate(Instance.particlePrefabs[(int)type], position, quaternion);
        StartCoroutine(DestroyAfter(spawned, spawned.GetComponent<ParticleSystem>().main.duration));
    }

    public void SpawnParticles(Vector3 position, ParticleType type)
    {
        GameObject spawned = Instantiate(Instance.particlePrefabs[(int)type], position, Quaternion.identity);
        StartCoroutine(DestroyAfter(spawned, spawned.GetComponent<ParticleSystem>().main.duration));
    }

    private IEnumerator DestroyAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
