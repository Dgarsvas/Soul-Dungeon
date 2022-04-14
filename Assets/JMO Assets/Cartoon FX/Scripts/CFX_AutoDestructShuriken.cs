using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
    public bool OnlyDeactivate;
    private ParticleSystem particles;

    void OnEnable()
    {
        particles = GetComponent<ParticleSystem>();
        StartCoroutine(CheckIfAlive());
    }

    IEnumerator CheckIfAlive()
    {
        while (particles != null)
        {
            yield return new WaitForSeconds(0.5f);
            if (!particles.IsAlive(true))
            {
                if (OnlyDeactivate)
                {
                    gameObject.SetActive(false);
                }
                else
                    Destroy(gameObject);
                break;
            }
        }
    }
}
