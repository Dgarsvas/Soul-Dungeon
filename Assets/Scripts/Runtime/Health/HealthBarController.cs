using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public static HealthBarController Instance
    {
        get;
        private set;
    }

    [SerializeField]
    private GameObject healthBarPrefab;

    private Dictionary<HealthController, HealthBar> healthDict;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        healthDict = new Dictionary<HealthController, HealthBar>();
    }

    public void StopTrackingHealth(HealthController entity)
    {
        Destroy(healthDict[entity].gameObject);
        healthDict.Remove(entity);
    }

    public void TrackHealth(HealthController entity)
    {
        var healthBar = Instantiate(healthBarPrefab, transform).GetComponent<HealthBar>();
        healthBar.Initialize(entity);
        healthDict.Add(entity, healthBar);
    }
}
